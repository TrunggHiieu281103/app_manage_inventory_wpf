using manage_inventory.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;

namespace manage_inventory.ViewModel
{
    public class ObjectViewModel : BaseViewModel
{
    private ObservableCollection<Models.Object> _List;
    public ObservableCollection<Models.Object> List { get => _List; set { _List = value; OnPropertyChanged(); } }

    private ObservableCollection<Models.Unit> _Unit;
    public ObservableCollection<Models.Unit> Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

    private ObservableCollection<Models.Suplier> _Suplier;
    public ObservableCollection<Models.Suplier> Suplier { get => _Suplier; set { _Suplier = value; OnPropertyChanged(); } }

    private Models.Object _SelectedItem;
    public Models.Object SelectedItem
    {
        get => _SelectedItem;
        set
        {
            _SelectedItem = value;
            OnPropertyChanged();
            if (SelectedItem != null)
            {
                DisplayName = SelectedItem.DisplayName;
                QRCode = SelectedItem.QRCode;
                BarCode = SelectedItem.BarCode;
                SelectedUnit = Unit.FirstOrDefault(u => u.Id == SelectedItem.IdUnit);
                SelectedSuplier = Suplier.FirstOrDefault(s => s.Id == SelectedItem.IdSuplier);
            }
        }
    }

    private Models.Unit _SelectedUnit;
    public Models.Unit SelectedUnit
    {
        get => _SelectedUnit;
        set
        {
            _SelectedUnit = value;
            OnPropertyChanged();               
        }
    }

    private Models.Suplier _SelectedSuplier;
    public Models.Suplier SelectedSuplier
    {
        get => _SelectedSuplier;
        set
        {
            _SelectedSuplier = value;
            OnPropertyChanged();
        }
    }

    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

    private string _QRCode;
    public string QRCode { get => _QRCode; set { _QRCode = value; OnPropertyChanged(); } }

    private string _BarCode;
    public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }

    public ICommand AddCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public ObjectViewModel()
    {
        List = new ObservableCollection<Models.Object>(DataProvider.ins.DB.Objects);
        Unit = new ObservableCollection<Models.Unit>(DataProvider.ins.DB.Units);
        Suplier = new ObservableCollection<Models.Suplier>(DataProvider.ins.DB.Supliers);
        
        AddCommand = new RelayCommand<object>((p) =>
        {
            if (SelectedSuplier == null || SelectedUnit == null)
                return false;
            return true;

        }, (p) =>
        {
            try
            {
                if (!DisplayName.IsNullOrEmpty() && !BarCode.IsNullOrEmpty() && !QRCode.IsNullOrEmpty())
                {
                    var Object = new Models.Object()
                    {
                        DisplayName = DisplayName,
                        BarCode = BarCode,
                        QRCode = QRCode,
                        IdSuplier = SelectedSuplier.Id,
                        IdUnit = SelectedUnit.Id,
                        Id = Guid.NewGuid().ToString()
                    };

                    DataProvider.ins.DB.Objects.Add(Object);
                    DataProvider.ins.DB.SaveChanges();

                    List.Add(Object);
                } else
                {
                    MessageBox.Show("Please fill all");
                }
                
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
            
        });

        EditCommand = new RelayCommand<object>((p) =>
        {
            if (SelectedItem == null || SelectedSuplier == null || SelectedUnit == null)
                return false;

            var displayList = DataProvider.ins.DB.Objects.Where(x => x.Id == SelectedItem.Id);
            if (displayList != null && displayList.Count() != 0)
                return true;

            return false;

        }, (p) =>
        {
            try
            {   if (!DisplayName.IsNullOrEmpty() && !BarCode.IsNullOrEmpty() && !QRCode.IsNullOrEmpty())
                {
                    var Object = DataProvider.ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    Object.DisplayName = DisplayName;
                    Object.BarCode = BarCode;
                    Object.QRCode = QRCode;
                    Object.IdSuplier = SelectedSuplier.Id;
                    Object.IdUnit = SelectedUnit.Id;
                    DataProvider.ins.DB.SaveChanges();

                    SelectedItem.DisplayName = DisplayName;
                    List = new ObservableCollection<Models.Object>(DataProvider.ins.DB.Objects);
                } else
                {
                    MessageBox.Show("Please fill all");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error");

            }
           
        });

        DeleteCommand = new RelayCommand<object>((p) =>
        {
            if (SelectedItem == null)
                return false;

            return true;

        }, (p) =>
        {
            try
            {
                var Object = DataProvider.ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.ins.DB.Objects.Remove(Object);
                DataProvider.ins.DB.SaveChanges();

                List.Remove(Object);
            }
            catch (Exception)
            {
                MessageBox.Show("Can not delete this object");
            }
            
        });
    }
}


}
