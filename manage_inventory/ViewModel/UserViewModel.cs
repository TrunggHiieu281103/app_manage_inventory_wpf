using manage_inventory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace manage_inventory.ViewModel
{
    public class UserViewModel: BaseViewModel
    {
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List {  get => _List; set { _List = value; OnPropertyChanged(); }  }

        private ObservableCollection<UserRole> _Role;
        public ObservableCollection<UserRole> Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        private User _SelectedItem;
        public User SelectedItem { get => _SelectedItem;
            set 
            { 
                _SelectedItem = value; OnPropertyChanged(); 
                if (SelectedItem != null) 
                { 
                    DisplayName = SelectedItem.DisplayName;
                    UserName = SelectedItem.UserName;
                    Password = SelectedItem.Password;
                    IdRole = SelectedItem.IdRole;
                } 
            } 
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private int _IdRole;
        public int IdRole { get => _IdRole; set { _IdRole = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public UserViewModel() 
        {
            List = new ObservableCollection<User>(DataProvider.ins.DB.Users);
            Role = new ObservableCollection<UserRole>(DataProvider.ins.DB.UserRoles);
            AddCommand = new RelayCommand<object>((p) => 
            { 
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                var displayList = DataProvider.ins.DB.Users.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0) 
                    return false;
                return true;

            }, (p) => 
            {
                var user = new User() { 
                    DisplayName = DisplayName,
                    UserName = UserName,
                    Password= Password,
                    IdRole = SelectedItem.IdRole

                };
                DataProvider.ins.DB.Users.Add(user);
                DataProvider.ins.DB.SaveChanges();

                List.Add(user);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.ins.DB.Users.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var user = DataProvider.ins.DB.Users.Where(x=>x.Id == SelectedItem.Id).SingleOrDefault();

                user.DisplayName = DisplayName;
                user.UserName = UserName;
                user.Password = Password;
                user.IdRole = SelectedItem.IdRole;

                DataProvider.ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;

            });
        }
    }
}
