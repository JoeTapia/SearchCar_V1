namespace SearchCar_V1.Models.Geolocation
{
    public class Resultado
    {
        public List<ComponentesDireccion> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometria geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }
}
