using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace manage_inventory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public ICommand LoadedWindowCommand { get; set; }

        // handle logic for MainWindow.xaml
        public bool IsLoaded = false;
        public MainViewModel()
        {

            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                IsLoaded = true;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                }
            );
            
        }
    }
}
