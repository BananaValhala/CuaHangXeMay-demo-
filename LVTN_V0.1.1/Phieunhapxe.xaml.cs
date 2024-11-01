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
    /// Interaction logic for Phieunhapxe.xaml
    /// </summary>
    public partial class Phieunhapxe : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public Phieunhapxe()
        {
            InitializeComponent();
        }
        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            dgPNX.ItemsSource = ndc.PHIEUNHAPXEs.ToList();
            dc = ndc;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLapPN_Click(object sender, RoutedEventArgs e)
        {
            Them.LapPhieunhap l = new Them.LapPhieunhap();
            if(l.IsVisible)l.Focus();
            else
                l.ShowDialog();
            Hienthi();
        }

        //private void btnxemPD_Click(object sender, RoutedEventArgs e)
        //{
        //    Phieumuaxe p = new Phieumuaxe();
        //    p.ShowDialog();
        //    Close();
        //}

        private void btnXemCT_Click(object sender, RoutedEventArgs e)
        {
            Chitiet.ChitietPhieuNhapXe c = new Chitiet.ChitietPhieuNhapXe();
            if(c.IsVisible)c.Focus();
            else
            {
                c.ma = dgPNX.SelectedValue.ToString();
                c.ShowDialog();
            }
        }
    }
}
