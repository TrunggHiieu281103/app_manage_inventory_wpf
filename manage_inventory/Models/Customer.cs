using System;
using System.Collections.Generic;

namespace manage_inventory.Models;

public partial class Customer
{
    public Customer()
    {
        this.OutputInfoes = new HashSet<OutputInfo>();
    }

    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string MoreInfo { get; set; }
    public Nullable<System.DateTime> ContractDate { get; set; }

    public virtual ICollection<OutputInfo> OutputInfoes { get; set; }
}
