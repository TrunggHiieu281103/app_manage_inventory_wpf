using manage_inventory.ViewModel;
using System;
using System.Collections.Generic;

namespace manage_inventory.Models;

public partial class Object : BaseViewModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Object()
    {
        this.InputInfoes = new HashSet<InputInfo>();
        this.OutputInfoes = new HashSet<OutputInfo>();
    }

    private string _Id;
    public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

    private int _IdUnit;
    public int IdUnit { get => _IdUnit; set { _IdUnit = value; OnPropertyChanged(); } }

    private int _IdSuplier;
    public int IdSuplier { get => _IdSuplier; set { _IdSuplier = value; OnPropertyChanged(); } }

    private string _QRCode;
    public string QRCode { get => _QRCode; set { _QRCode = value; OnPropertyChanged(); } }

    private string _BarCode;
    public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }

    public virtual Suplier Suplier { get; set; }
    public virtual Unit Unit { get; set; }

    public virtual ICollection<InputInfo> InputInfoes { get; set; }
    public virtual ICollection<OutputInfo> OutputInfoes { get; set; }
}