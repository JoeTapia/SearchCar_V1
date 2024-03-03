using System;
using System.Collections.Generic;

namespace SearchCar_V1.Data;

public partial class TblLocalidad
{
    public int IdLocalidad { get; set; }

    public string? NombreLocalidad { get; set; }

    public int? IdCiudad { get; set; }
}
