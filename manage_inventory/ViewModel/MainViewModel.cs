using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace manage_inventory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        // handle logic for MainWindow.xaml
        public bool IsLoaded = false;
        public MainViewModel()
        {
            if(!IsLoaded)
            {
                IsLoaded = true;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
            
        }
    }
}
