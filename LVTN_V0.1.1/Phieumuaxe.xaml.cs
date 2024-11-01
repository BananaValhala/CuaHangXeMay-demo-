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
    /// Interaction logic for Phieumuaxe.xaml
    /// </summary>
    public partial class Phieumuaxe : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public Phieumuaxe()
        {
            InitializeComponent();
        }
        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            dgPDM.ItemsSource = ndc.PHIEUDATMUAs.ToList();
            dc = ndc;
        }
        private PHIEUDATMUA Find(string n)
        {
            PHIEUDATMUA d = new PHIEUDATMUA();
            foreach (var a in dc.PHIEUDATMUAs)
            {
                if (a.maPhieudat == n)
                {
                    d = a;
                    return d;
                }
            }
            return d;
        }
        private NHANVIEN FindNV(string n)
        {
            NHANVIEN nv = new();
            foreach(var a in dc.NHANVIENs)
            {
                if(a.manv == n)
                {
                    nv = a;
                    return nv;
                }
            }
            return nv;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLapPN_Click(object sender, RoutedEventArgs e)
        {
            PHIEUDATMUA d = Find(dgPDM.SelectedValue.ToString());
            if(d.PHIEUNHAPXEs.Any() == true)
            {
                MessageBox.Show("Phiếu đặt mua xe đã có phiếu nhập.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Them.LapPhieunhap l = new Them.LapPhieunhap();
                l.cmbpm.SelectedValue = d.maPhieudat;
                l.ShowDialog();
            }
        }

        private void btnXemCT_Click(object sender, RoutedEventArgs e)
        {
            Chitiet.ChitietPhieuDatMua c = new Chitiet.ChitietPhieuDatMua();
            c.ma = dgPDM.SelectedValue.ToString();
            c.ShowDialog();
        }

        private void btnLapPM_Click(object sender, RoutedEventArgs e)
        {
            Them.LapPhieumua l = new Them.LapPhieumua();
            if (l.IsVisible) l.Focus();
            else
            {
                NHANVIEN n = FindNV(tblma.Text);
                l.txttennv.Text = n.tennv;
                l.tblmanv.Text = n.manv;
                l.ShowDialog();
            }
            Hienthi();
        }
    }
}
