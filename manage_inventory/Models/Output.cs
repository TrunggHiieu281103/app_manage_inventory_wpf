﻿using System;
using System.Collections.Generic;

namespace manage_inventory.Models;

public partial class Output
{
    public string Id { get; set; } = null!;

    public DateTime? DateOutput { get; set; }

    public virtual OutputInfo? OutputInfo { get; set; }
}