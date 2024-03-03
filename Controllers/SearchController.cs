using Microsoft.AspNetCore.Mvc;
using SearchCar_V1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchCar_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        I_Servicios _servicio = new Imp_Servicios();


        [HttpGet]
        [Route("ConsultarCarrosDisponibles")]
        public ResultConsulta ConsultarCarrosDisponibles(string direccionCliente)
        {
            var result = _servicio.GetConsulta(direccionCliente);
            return result;
        }
    }
}
