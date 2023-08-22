using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class PantLogsTimestamp
{
    public long TimestampsId { get; set; }

    public DateTime Start { get; set; }

    public DateTime Delivered { get; set; }

    public DateTime? Pickup { get; set; }

    public DateTime? Completed { get; set; }

    public virtual ICollection<PantLog> PantLogs { get; set; } = new List<PantLog>();
}
