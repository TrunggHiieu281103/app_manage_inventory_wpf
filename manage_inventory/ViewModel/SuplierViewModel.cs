using manage_inventory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace manage_inventory.ViewModel
{
    public class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Suplier _SelectedItem;
        public Suplier SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.ins.DB.Supliers);

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Suplier = new Suplier() { DisplayName = DisplayName, Phone = Phone, Address = Address, Email = Email, ContractDate = ContractDate, MoreInfo = MoreInfo };

                DataProvider.ins.DB.Supliers.Add(Suplier);
                DataProvider.ins.DB.SaveChanges();

                List.Add(Suplier);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Suplier = DataProvider.ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Suplier.DisplayName = DisplayName;
                Suplier.Phone = Phone;
                Suplier.Address = Address;
                Suplier.Email = Email;
                Suplier.ContractDate = ContractDate;
                Suplier.MoreInfo = MoreInfo;
                DataProvider.ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, (p) =>
            {
                var Suplier = DataProvider.ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                if (Suplier != null)
                {
                    DataProvider.ins.DB.Supliers.Remove(Suplier);
                    DataProvider.ins.DB.SaveChanges();
                    List.Remove(Suplier);
                }
            });
        }
    }
}
