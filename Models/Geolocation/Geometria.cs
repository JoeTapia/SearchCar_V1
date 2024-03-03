namespace SearchCar_V1.Models.Geolocation
{
    public class Geometria
    {
        public Limites bounds { get; set; }
        public Limite location { get; set; }
        public string location_type { get; set; }
        public VentanaGrafica viewport { get; set; }
    }
}
