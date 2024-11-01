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

namespace LVTN_V0._1._1.Chitiet
{
    /// <summary>
    /// Interaction logic for ChitietPhieuNhapXe.xaml
    /// </summary>
    public partial class ChitietPhieuNhapXe : Window
    {
        public string ma = "";
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public ChitietPhieuNhapXe()
        {
            InitializeComponent();
        }
        private List<CHITIETPHIEUNHAP> listCTPN (string n)
        {
            List<CHITIETPHIEUNHAP> c = new List<CHITIETPHIEUNHAP>();
            foreach(var a in dc.CHITIETPHIEUNHAPs)
            {
                if(a.maPhieunhap == ma)
                {
                    c.Add(a);
                }
            }
            return c;
        }
        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgCTPDM.ItemsSource = listCTPN(ma);
        }
    }
}
