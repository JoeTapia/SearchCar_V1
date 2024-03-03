using SearchCar_V1.Models.Geolocation;

namespace SearchCar_V1.Models
{
    public class ResultDistancia
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public List<Registro> rows { get; set; }
        public string status { get; set; }
    }
}
