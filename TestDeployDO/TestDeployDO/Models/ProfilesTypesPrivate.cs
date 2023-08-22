using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

/// <summary>
/// Private profile parameters
/// </summary>
public partial class ProfilesTypesPrivate
{
    public Guid Uid { get; set; }

    public string? NameGiven { get; set; }

    public string? NameFamily { get; set; }

    public string? BirthYear { get; set; }

    public string? Gender { get; set; }

    public string? PhonePrivate { get; set; }

    public string? Email { get; set; }

    public short? HouseholdCount { get; set; }

    public virtual Profile UidNavigation { get; set; } = null!;
}
