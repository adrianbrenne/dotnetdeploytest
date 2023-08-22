using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

/// <summary>
/// Enterprise profiles parameters
/// </summary>
public partial class ProfilesTypesEnterprise
{
    public Guid Uid { get; set; }

    public string? OrgNr { get; set; }

    public string? OrgName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactEmail { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? ContactPhone { get; set; }

    public virtual Profile UidNavigation { get; set; } = null!;
}
