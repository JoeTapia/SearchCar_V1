namespace SearchCar_V1.Models
{
    public interface I_Servicios
    {
        ResultGeolocalizacion GetLocalizacion(string address);
        ResultDistancia GetDistancia(string address1, string address2);
        string ReplaceDireccion(string address);
        string GetLlave(string llave);
        ResultConsulta GetConsulta(string address);
        string CorregirDireccion(string address);
    }
}
