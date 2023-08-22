using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class Unibox
{
    public long Id { get; set; }

    public string IdentifierName { get; set; } = null!;

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public short? BagLimit { get; set; }

    public virtual ICollection<PantLog> PantLogs { get; set; } = new List<PantLog>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
