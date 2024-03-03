using System;
using System.Collections.Generic;

namespace SearchCar_V1.Data;

public partial class TblSede
{
    public int IdSede { get; set; }

    public string? NombreSede { get; set; }

    public string? DireccionSede { get; set; }

    public int? IdLocalidad { get; set; }

    public int? CantMaxima { get; set; }
}
