using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class PantLog
{
    public long LogId { get; set; }

    public DateTime LastUpdated { get; set; }

    public Guid Uid { get; set; }

    public long TimestampId { get; set; }

    public long UniboxId { get; set; }

    public short? RatingNumber { get; set; }

    public Guid? Handler { get; set; }

    public string? RatingText { get; set; }

    public virtual ProfilesTypesAdmin? HandlerNavigation { get; set; }

    public virtual ICollection<PantLogsBag> PantLogsBags { get; set; } = new List<PantLogsBag>();

    public virtual PantLogsTimestamp Timestamp { get; set; } = null!;

    public virtual Profile UidNavigation { get; set; } = null!;

    public virtual Unibox Unibox { get; set; } = null!;
}
