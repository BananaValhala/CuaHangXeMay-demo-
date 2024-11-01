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
    /// Interaction logic for Dongxe.xaml
    /// </summary>
    public class TheLoaiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "01":
                    return "Động cơ đốt trong";
                case "02":
                    return "Động cơ điện";
            }
            return "Động cơ đốt trong";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
    public partial class Dongxe : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public Dongxe()
        {
            InitializeComponent();
        }

        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            dgDongxe.ItemsSource = ndc.DONGXEs.ToList();
            dc = ndc;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }
        private DONGXE Find(string n)
        {
            DONGXE dx = new DONGXE();
            foreach (var a in dc.DONGXEs)
            {
                if (a.maDong == dgDongxe.SelectedValue.ToString())
                {
                    dx = a;
                    return dx;
                }
            }
            return null;
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnThemdongxe_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemDongxe tdx = new Them.ThemDongxe();
            if (tdx.IsVisible) tdx.Focus();
            else
                tdx.ShowDialog();
            Hienthi();
        }

        private void btnEditDongxe_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaDongxe sdx = new Sua.SuaDongxe();
            if(sdx.IsVisible) sdx.Focus();
            else
            {
                DONGXE dx = Find(dgDongxe.SelectedValue.ToString());
                sdx.txtma.Text = dx.maDong;
                sdx.txtten.Text = dx.tenDong;
                sdx.txtmau.Text = dx.mau;
                sdx.txtgia.Text = dx.giaban.ToString();
                sdx.cmbnsx.SelectedValue = dx.mansx;
                sdx.cmbtl.SelectedValue = dx.matl;
                sdx.txtkilometbaohanh.Text = dx.baohanhcayso.ToString();
                sdx.txtbaohanh.Text = dx.baohanh.ToString();
                sdx.ShowDialog();
                Hienthi();
            }
        }

        private void btnDeleteDongxe_Click(object sender, RoutedEventArgs e)
        {
            DONGXE dx = Find(dgDongxe.SelectedValue.ToString());
            MessageBoxResult result = MessageBox.Show("Bạn thật sự muốn xóa dòng xe này ?", "Xóa Dòng Xe", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (dx != null && dx.XEMAYs.Count == 0 && dx.CHITIETPHIEUDATs.Count == 0 && dx.CHITIETPHIEUNHAPs.Count == 0)
                    {
                        dc.DONGXEs.DeleteObject(dx);
                        dc.SaveChanges();
                        Hienthi();
                    }
                    else
                    {
                        MessageBox.Show("Dòng xe có khóa ngoại, không được xóa.");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
