using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class Address
{
    public short AddressId { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PostCode { get; set; } = null!;

    public short? HouseholdCount { get; set; }
}
