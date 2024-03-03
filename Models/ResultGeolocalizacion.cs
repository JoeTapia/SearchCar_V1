using SearchCar_V1.Models.Geolocation;

namespace SearchCar_V1.Models
{
    public class ResultGeolocalizacion
    {
        public List<Resultado> results { get; set; }
        public string status { get; set; }
    }
}
