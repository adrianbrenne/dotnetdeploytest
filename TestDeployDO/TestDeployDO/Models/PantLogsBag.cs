using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class PantLogsBag
{
    public long BagId { get; set; }

    public long LogId { get; set; }

    public short? UnitsOk { get; set; }

    public short? UnitsNegligible { get; set; }

    public short? Kr { get; set; }

    public string BagCode { get; set; } = null!;

    public virtual PantLog Log { get; set; } = null!;
}
