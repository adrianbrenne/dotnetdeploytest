using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class AccountsWithdrawalLog
{
    public long AccountId { get; set; }

    public Guid Uid { get; set; }

    public DateTime TimeRequested { get; set; }

    public DateTime? TimeTransferred { get; set; }

    public int MoneyRequested { get; set; }

    public int? MoneyTransferred { get; set; }

    public bool Transferred { get; set; }

    public long WithdrawalId { get; set; }

    public string? PhoneNr { get; set; }

    public string? BankAccountNumber { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Profile UidNavigation { get; set; } = null!;
}
