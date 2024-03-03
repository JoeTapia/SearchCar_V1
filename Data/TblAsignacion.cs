using System;
using System.Collections.Generic;

namespace SearchCar_V1.Data;

public partial class TblAsignacion
{
    public int IdAsignacion { get; set; }

    public int? IdCarro { get; set; }

    public int? IdSede { get; set; }

    public int? Cant { get; set; }
}
