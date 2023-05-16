using System;
using System.Collections.Generic;

namespace ASPAPI_mongo.Models;

public partial class Origin
{
    public int OriginId { get; set; }

    public string Usage { get; set; } = null!;
}
