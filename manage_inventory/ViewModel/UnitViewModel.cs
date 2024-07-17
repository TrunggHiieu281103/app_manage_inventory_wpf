using manage_inventory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace manage_inventory.ViewModel
{
    class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Unit _SelectedItem;
        public Unit SelectedItem { get=> _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                }
            }
        }


        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.ins.DB.Units);

            AddCommand = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = DataProvider.ins.DB.Units.Where(x => x.DisplayName == DisplayName);

                if(displayList == null || displayList.Count() != 0) 
                    return false;

                return true;
            }, (p) => 
            {
                var unit = new Unit() { DisplayName = DisplayName };

                DataProvider.ins.DB.Units.Add(unit);
                DataProvider.ins.DB.SaveChanges();

                List.Add(unit);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;

                var displayList = DataProvider.ins.DB.Units.Where(x => x.DisplayName == DisplayName);

                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                var unit = DataProvider.ins.DB.Units.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                unit.DisplayName = DisplayName;
                DataProvider.ins.DB.SaveChanges();

                List = new ObservableCollection<Unit>(DataProvider.ins.DB.Units);

            });

        }
    }
}
