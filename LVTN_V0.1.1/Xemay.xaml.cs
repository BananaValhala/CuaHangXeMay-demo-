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
    /// Interaction logic for Xemay.xaml
    /// </summary>
    public class BikeStatusToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "DB":
                    return "Đã bán";
                case "CB":
                    return "Chưa bán";
            }
            return "Chưa bán";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }

    public partial class Xemay : Window
    {
        private CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public Xemay()
        {
            InitializeComponent();
        }

        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            dgXemay.ItemsSource = ndc.XEMAYs.ToList();
            dc = ndc;
            if (tbljob.Text == "NV")
            {
                btnThemXemay.Visibility = Visibility.Collapsed;
                foreach (DataGridColumn c in dgXemay.Columns)
                {
                    if (c.Header.ToString() == "Edit" || c.Header.ToString() == "Delete")
                    {
                        c.Visibility = Visibility.Collapsed;
                    }
                }
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }
        private XEMAY Find(string n)
        {
            XEMAY x = new XEMAY();
            foreach (var a in dc.XEMAYs)
            {
                if (a.maxe == n)
                {
                    x = a;
                    return x;
                }
            }
            return x;
        }


        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnThemXemay_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemXemay x = new Them.ThemXemay();
            if (x.IsVisible) x.Focus();
            else
                x.ShowDialog();
            Hienthi();
        }

        private void btnEditXemay_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaXemay x = new Sua.SuaXemay();
            if (x.IsVisible) x.Focus();
            else
            {
                XEMAY xe = Find(dgXemay.SelectedValue.ToString());
                x.txtma.Text = xe.maxe;
                x.txtten.Text = xe.tenxe;
                x.txtmota.Text = xe.mota;
                x.cmbdong.SelectedValue = xe.maDong;
                x.cmbnsx.SelectedValue = xe.mansx;
                x.cmbtl.SelectedValue = xe.matl;
                x.txtsothangbh.Text = xe.baohanh.ToString();
                x.txtsokilometbh.Text = xe.baohanhcayso.ToString();
                Uri uri = new("/Hinh/null.bmp", UriKind.Relative);
                if (xe.hinh != null && xe.hinh != "")
                {
                    uri = new Uri(xe.hinh);
                    x.img = xe.hinh;
                }
                x.imgxe.Source = new BitmapImage(uri);
                x.ShowDialog();
            }
            Hienthi();
        }

        private void btnDeleteXemay_Click(object sender, RoutedEventArgs e)
        {
            XEMAY x = Find(dgXemay.SelectedValue.ToString());
            MessageBoxResult result = MessageBox.Show("Bạn thật sự muốn xóa xe máy này ?", "Xóa Xe Máy", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (x != null && x.HOADONTRALIENs.Count == 0 && x.HOPDONGTRAGOPs.Count == 0)
                    {
                        foreach(var a in dc.DONGXEs)
                        {
                            if(a.maDong == x.maDong)
                            {
                                a.soluongConlai--;
                            }
                        }
                        dc.XEMAYs.DeleteObject(x);
                        dc.SaveChanges();
                        Hienthi();
                    }
                    else
                    {
                        MessageBox.Show("Xe máy có khóa ngoại, không được xóa.");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void dgXemay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgXemay.SelectedValue != null)
            {
                Uri uri = new("/Hinh/null.bmp", UriKind.Relative);
                XEMAY x = Find(dgXemay.SelectedValue.ToString());
                if (x != null)
                {
                    if (x.hinh != null && x.hinh != "")
                    {
                        uri = new(x.hinh);
                    }
                    tblmota.Text = x.mota;
                    tblbaohanh.Text = x.baohanh.ToString() + " tháng" + " / " + x.baohanhcayso.ToString() + " Kilomét";
                    tblSoKvaSoM.Text = x.soKhung + "    " + x.soMay;
                }
                imgHinhxe.Source = new BitmapImage(uri);
            }
        }
    }
}
