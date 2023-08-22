using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class Profile
{
    public Guid Uid { get; set; }

    public DateTime CreatedAt { get; set; }

    public long AccountId { get; set; }

    public long? UniboxId { get; set; }

    public long? ProfilesTypesId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<AccountsWithdrawalLog> AccountsWithdrawalLogs { get; set; } = new List<AccountsWithdrawalLog>();

    public virtual ICollection<PantLog> PantLogs { get; set; } = new List<PantLog>();

    public virtual ProfilesType? ProfilesTypes { get; set; }

    public virtual ProfilesTypesEnterprise? ProfilesTypesEnterprise { get; set; }

    public virtual ProfilesTypesPrivate? ProfilesTypesPrivate { get; set; }

    public virtual Unibox? Unibox { get; set; }
}
