using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class ProfilesTypesAdmin
{
    public Guid Uid { get; set; }

    public string NameGiven { get; set; } = null!;

    public string NameFamily { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhonePrivate { get; set; } = null!;

    public virtual ICollection<PantLog> PantLogs { get; set; } = new List<PantLog>();
}
