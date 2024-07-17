using manage_inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace manage_inventory.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool Islogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }


        // handle logic for MainWindow.xaml
        public LoginViewModel()
        {
            Password = "";
            UserName = "";
            Islogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        void Login(Window p)
        {
            if( p  == null )
                return;

            var accCount = DataProvider.ins.DB.Users.Where(p => p.UserName == UserName && p.Password == Password).Count();
            if (accCount > 0)
            {
                Islogin = true;
                p.Close();
            }
            else 
            {
                Islogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
            
        }
    }
}
