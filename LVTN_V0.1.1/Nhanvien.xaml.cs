using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LVTN_V0._1._1
{
    /// <summary>
    /// Interaction logic for Nhanvien.xaml
    /// </summary>
    public class GenderToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case true:
                    return "Nam";
                case false:
                    return "Nữ";
            }
            return "Nữ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
    public class StatusToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case true:
                    return "Active";
                case false:
                    return "Inactive";
            }
            return "Inactive";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
    public partial class Nhanvien : Window
    {
        private CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public Nhanvien()
        {
            InitializeComponent();
        }
        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            dgNhanvien.ItemsSource = ndc.NHANVIENs.ToList();
            dc = ndc;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }
        private NHANVIEN Find(string n)
        {
            NHANVIEN nv = new NHANVIEN();
            foreach (var a in dc.NHANVIENs)
            {
                if (a.manv == n)
                {
                    nv = a;
                    return nv;
                }
            }
            return nv;
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnThemNhanvien_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemNhanvien tnv = new Them.ThemNhanvien();
            if (tnv.IsVisible) tnv.Focus();
            else
                tnv.ShowDialog();
            Hienthi();
        }

        private void btnEditNhanvien_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaNhanvien snv = new Sua.SuaNhanvien();
            if (snv.IsVisible) snv.Focus();
            else
            {
                NHANVIEN nv = Find(dgNhanvien.SelectedValue.ToString());
                snv.txtma.Text = nv.manv;
                snv.txtten.Text = nv.tennv;
                snv.txtsdt.Text = nv.sdt.ToString();
                snv.txtcmnd.Text = nv.CMND;
                if (nv.gioitinh == true)
                {
                    snv.rdonam.IsChecked = true;
                }
                else
                    snv.rdonu.IsChecked = false;
                if (nv.trangthai == true)
                {
                    snv.rdoactive.IsChecked = true;
                }
                else
                    snv.rdoinactive.IsChecked = true;
                snv.dpngaylam.Text = nv.ngayVaoLam.ToString();
                snv.txtpasswd.Text = nv.matkhau;
                snv.txttendn.Text = nv.tendn;
                if(nv.maChucvu == "01")
                {
                    snv.rdoactive.IsEnabled = false;
                    snv.rdoinactive.IsEnabled = false;
                }
                snv.ShowDialog();
                Hienthi();
            }
        }

        private void btnDeleteNhanvien_Click(object sender, RoutedEventArgs e)
        {
            NHANVIEN nv = Find(dgNhanvien.SelectedValue.ToString());
            MessageBoxResult result = MessageBox.Show("Bạn thật sự muốn xóa nhân viên này ?", "Xóa Nhân Viên", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if(nv.trangthai == true)
                    {
                        MessageBox.Show("Nhân viên vẫn còn làm, không được xóa.");
                    }
                    else if (nv.tennv == tbluser.Text)
                    {
                        MessageBox.Show("Nhân viên này là bạn, sao mà xóa được. :D");
                    }
                    else if (nv != null && nv.trangthai == false && nv.HOADONTRALIENs.Count == 0 && nv.HOPDONGTRAGOPs.Count == 0 && nv.PHIEUDATMUAs.Count == 0 && nv.PHIEUNHAPXEs.Count == 0)
                    {
                        dc.NHANVIENs.DeleteObject(nv);
                        dc.SaveChanges();
                        Hienthi();
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên có khóa ngoại, không được xóa.");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
