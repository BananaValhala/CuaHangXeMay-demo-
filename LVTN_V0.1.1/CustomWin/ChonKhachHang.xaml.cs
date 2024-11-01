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

namespace LVTN_V0._1._1.CustomWin
{
    /// <summary>
    /// Interaction logic for ChonKhachHang.xaml
    /// </summary>
    public partial class ChonKhachHang : Window
    {
        public event Action<string> CheckMA;
        CUAHANGXEMAYEntities dc = new();
        public ChonKhachHang()
        {
            InitializeComponent();
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            dgKhachhang.ItemsSource = ndc.KHACHHANGs.ToList();
            dc = ndc;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }

        private void btnThemKhachhang_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemKhachhang tkh = new Them.ThemKhachhang();
            if (tkh.IsVisible) tkh.Focus();
            else
                tkh.ShowDialog();
            Hienthi();
        }

        private void dgKhachhang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgKhachhang.SelectedValue != null)
            {
                CheckMA(dgKhachhang.SelectedValue.ToString());
            }
        }
    }
}
