namespace SearchCar_V1.Models
{
    public class ResultConsulta
    {
        public ResultSede recogida { get; set; }
        public List<ResultVehiculo> carros { get; set; }
        public List<ResultSede> devolucion { get; set; }
       
        
    }
}
