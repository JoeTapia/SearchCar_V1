using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SearchCar_V1.Data;
using System.Collections.Generic;

namespace SearchCar_V1.Models
{
    public class Imp_Servicios : I_Servicios
    {
        DbSearchCarContext db = new DbSearchCarContext();

        public string CorregirDireccion(string address)
        {

            if (address.ToUpper().Contains("DIAGONAL"))
            {
               address =  address.ToUpper().Replace("DIAGONAL", "Dg.");
            }
            else if (address.ToUpper().Contains("CARRERA"))
            {
                address = address.ToUpper().Replace("CARRERA", "Cra.");
            }
            else if (address.ToUpper().Contains("CALLE"))
            {
                address = address.ToUpper().Replace("CALLE", "CL.");
            }
            else if (address.ToUpper().Contains("AVENIDA"))
            {
                address = address.ToUpper().Replace("AVENIDA", "Av.");
            }
            else if (address.ToUpper().Contains("TRANSVERSAL"))
            {
                address = address.ToUpper().Replace("TRANSVERSAL", "Tv.");
            }
            return address;
        }

        public ResultConsulta GetConsulta(string address)
        {
            try
            {
                ResultConsulta consulta = new ResultConsulta();
                consulta.carros = new List<ResultVehiculo>();
                consulta.devolucion = new List<ResultSede>();
                var sedes = db.TblSedes.ToList();
                double distancia = 0;
                foreach(var sede in sedes)
                {
                    var result = GetDistancia(sede.DireccionSede, address);
                    if (result != null)
                    {
                        var distanciaKm = result.rows[0].elements[0].distance.text;
                        distanciaKm = distanciaKm.Replace(" km", "");
                        distanciaKm = distanciaKm.Replace(".", ",");
                        var dist = Convert.ToDouble(distanciaKm);
                        if (dist < distancia || distancia == 0)
                        {
                            distancia = dist;
                            consulta.recogida = new ResultSede()
                            {
                                    direccion = result.origin_addresses[0],
                                    nombre = sede.NombreSede,
                                    disponibilidad = "Sede más cerca para recogerlo",
                                    numeroSede = sede.IdSede
                            };
                        }
                        
                    }
                }
                var listAsignados = db.TblAsignacions.ToList();
                var asignados = listAsignados.Where(x => x.IdSede == consulta.recogida.numeroSede).ToList();
                var carros = db.TblCarros.Where(x => asignados.Select(y => y.IdCarro).Contains(x.IdCarro)).ToList();
                
                foreach (var car in carros) 
                {
                    var marca = db.TblMarcas.Where(x => x.IdMarca == (int)car.IdMarca).Select(x => x.NombreMarca).First();
                    var tipo = db.TblTipoCarros.Where(x => x.IdTipoCarro == (int)car.IdTipoCarro).Select(x => x.TipoCarro).First();
                    var cant = asignados.Where(x => x.IdCarro == car.IdCarro).Select(x => x.Cant).First() + " disponibles";
                    consulta.carros.Add(new ResultVehiculo()
                    {
                        marca = marca,
                        modelo = car.Modelo,
                        version = car.Version,
                        tipo = tipo,
                        color = car.Color,
                        motor = car.PotenciaMotor,
                        cant = cant
                    });
                }

                foreach (var sede in sedes)
                {
                    if (sede.CantMaxima > listAsignados.Where(x => (int)x.IdSede == sede.IdSede).Sum(x => x.Cant))
                    {
                        consulta.devolucion.Add(new ResultSede()
                        {
                            direccion = sede.DireccionSede,
                            nombre = sede.NombreSede,
                            disponibilidad = "Sede disponible para devolución",
                            numeroSede = sede.IdSede
                        });
                    }
                
                }

                return consulta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultDistancia GetDistancia(string address1, string address2)
        {
            try
            {
                ResultDistancia resultDis = new ResultDistancia();
                address1 = ReplaceDireccion(address1);
                address2 = ReplaceDireccion(address2);
                string key = GetLlave("keyApiGoogle");
                string url = GetLlave("urlApiGoogle");
                url = url.Replace("[address1]", address1);
                url = url.Replace("[address2]", address2);
                string requestUri = string.Format(url + key);

                WebRequest request = WebRequest.Create(requestUri);
                request.Method = "GET";
                request.ContentType = "application/json";
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(strReader))
                        {
                            string responseBody = reader.ReadToEnd();
                            resultDis = JsonConvert.DeserializeObject<ResultDistancia>(responseBody);
                        }
                    }
                }
                return resultDis;
            }
            catch (Exception e)
            {
                return new ResultDistancia();
            }
        }

        public string GetLlave(string llave)
        {
            string inicioPath = AppDomain.CurrentDomain.BaseDirectory;
            var builder = new ConfigurationBuilder().SetBasePath(inicioPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange:true);
            IConfiguration configuration = builder.Build();
            string result = configuration.GetSection(llave).Value;
            return result;
        }

        public ResultGeolocalizacion GetLocalizacion(string address)
        {
            try
            {
                ResultGeolocalizacion resultGeo = new ResultGeolocalizacion();
                address = ReplaceDireccion(address);
                string key = "AIzaSyB49AInhQ0ju6nax193OtuOzo2ONittgcU";
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=" + key);

                WebRequest request = WebRequest.Create(requestUri);
                request.Method = "GET";
                request.ContentType = "application/json";
                using(WebResponse response = request.GetResponse())
                {
                    using(Stream strReader = response.GetResponseStream())
                    {
                        using(StreamReader reader = new StreamReader(strReader))
                        {
                            string responseBody = reader.ReadToEnd();
                            resultGeo = JsonConvert.DeserializeObject<ResultGeolocalizacion>(responseBody);
                        }
                    }
                }
                return resultGeo;
            }
            catch (Exception e)
            { 
                return new ResultGeolocalizacion();
            }
        }

        public string ReplaceDireccion(string address)
        {
            address = CorregirDireccion(address);
            address = address.Replace(" ", "+");
            address = address.Contains("#") ? address.Replace("#", "%23") : address.Replace("No", "%23");
            return address;
        }
    }
}
