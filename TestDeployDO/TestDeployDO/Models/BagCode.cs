using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class BagCode
{
    public Guid Uid { get; set; }

    public string Code { get; set; } = null!;
}
