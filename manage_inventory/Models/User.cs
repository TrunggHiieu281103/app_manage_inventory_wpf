using manage_inventory.ViewModel;
using System;
using System.Collections.Generic;

namespace manage_inventory.Models;

public partial class User : BaseViewModel
{
    public int Id { get; set; }

    private string _DisplayName;
    public string DisplayName { get=> _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

    private string _UserName;
    public string  UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

    private string _Password;
    public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

    private int _IdRole;
    public int IdRole { get => _IdRole; set { _IdRole = value; OnPropertyChanged(); } }

    public virtual UserRole IdRoleNavigation { get; set; } = null!;
}
