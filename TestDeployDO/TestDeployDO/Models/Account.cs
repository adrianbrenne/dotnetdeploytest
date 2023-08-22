using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class Account
{
    public long AccountId { get; set; }

    public long Balance { get; set; }

    public string? PantCode { get; set; }

    public virtual ICollection<AccountsWithdrawalLog> AccountsWithdrawalLogs { get; set; } = new List<AccountsWithdrawalLog>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
