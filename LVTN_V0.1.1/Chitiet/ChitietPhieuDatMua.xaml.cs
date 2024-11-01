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
    /// Interaction logic for ChitietPhieuDatMua.xaml
    /// </summary>
    public partial class ChitietPhieuDatMua : Window
    {
        public string ma = "";
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public ChitietPhieuDatMua()
        {
            InitializeComponent();
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private List<CHITIETPHIEUDAT> listCTPD(string n)
        {
            List<CHITIETPHIEUDAT> c = new List<CHITIETPHIEUDAT>();
            foreach (var a in dc.CHITIETPHIEUDATs)
            {
                if (a.maPhieudat == ma)
                {
                    c.Add(a);
                }
            }
            return c;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgCTPDM.ItemsSource = listCTPD(ma);
        }
    }
}
