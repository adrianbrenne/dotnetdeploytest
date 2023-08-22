using System;
using System.Collections.Generic;

namespace TestDeployDO.Models;

public partial class ProfilesType
{
    public long ProfilesTypesId { get; set; }

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
