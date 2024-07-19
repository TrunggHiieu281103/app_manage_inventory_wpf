using manage_inventory.ViewModel;
using System;
using System.Collections.Generic;

namespace manage_inventory.Models
{
    public partial class InputInfo : BaseViewModel
    {
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _IdObject;
        public string IdObject { get => _IdObject; set { _IdObject = value; OnPropertyChanged(); } }

        private string _IdInput;
        public string IdInput { get => _IdInput; set { _IdInput = value; OnPropertyChanged(); } }

        private int? _Count;
        public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private double? _InputPrice;
        public double? InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }

        private double? _OutputPrice;
        public double? OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }

        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        public virtual Input IdInputNavigation { get; set; } = null!;

        public virtual Object IdObjectNavigation { get; set; } = null!;
    }
}
