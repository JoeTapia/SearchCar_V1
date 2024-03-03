using System;
using System.Collections.Generic;

namespace SearchCar_V1.Data;

public partial class TblCliente
{
    public int IdCliente { get; set; }

    public string? DocIdentidadCliente { get; set; }

    public string? NombreCliente { get; set; }

    public string? ApellidoCliente { get; set; }

    public string? TelefonoCliente { get; set; }

    public string? DireccionCliente { get; set; }

    public int? IdLocalidad { get; set; }
}
