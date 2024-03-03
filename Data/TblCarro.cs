using System;
using System.Collections.Generic;

namespace SearchCar_V1.Data;

public partial class TblCarro
{
    public int IdCarro { get; set; }

    public string? CodigoCarro { get; set; }

    public int? IdMarca { get; set; }

    public string? Modelo { get; set; }

    public decimal? Precio { get; set; }

    public string? Color { get; set; }

    public string? PotenciaMotor { get; set; }

    public string? Version { get; set; }

    public int? IdTipoCarro { get; set; }
}
