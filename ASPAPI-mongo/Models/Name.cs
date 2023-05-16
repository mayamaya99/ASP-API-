using System;
using System.Collections.Generic;

namespace ASPAPI_mongo.Models;

public partial class Name
{
    public int NameId { get; set; }

    public string Name1 { get; set; } = null!;

    public int GenderId { get; set; }

    public int OriginId { get; set; }

    public int YearMost { get; set; }

    public int YearLeast { get; set; }
    public virtual Gender Gender { get; set; } = null!;
    public virtual Origin Origin { get; set; } = null!;
}
