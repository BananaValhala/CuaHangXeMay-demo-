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

namespace LVTN_V0._1._1.Bienlai
{
    /// <summary>
    /// Interaction logic for ThemBienlai.xaml
    /// </summary>
    public partial class ThemBienlai : Window
    {
        CUAHANGXEMAYEntities dc = new();
        public ThemBienlai()
        {
            InitializeComponent();
            dpngaydong.SelectedDate = DateTime.Now;
        }
        private HOPDONGTRAGOP FindHDTG(string n)
        {
            HOPDONGTRAGOP h = new();
            foreach(var a in dc.HOPDONGTRAGOPs)
            {
                if(a.mahdtg == n)
                {
                    h = a;
                    return h;
                }
            }
            return h;
        }
        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.BIENLAIs.Count();
            c++;
            string s = "0" + c;
            return s;
        }
        private bool IsInt(string sVal)
        {
            foreach (char c in sVal)
            {
                int iN = (int)c;
                if ((iN > 57) || (iN < 48))
                    return false;
            }
            return true;
        }

        private int CountTien(HOPDONGTRAGOP h)
        {
            int sumtien = 0;
            foreach(var a in h.BIENLAIs)
            {
                sumtien += a.tienDong;
            }
            return sumtien;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {           
            HOPDONGTRAGOP h = FindHDTG(tblmaHD.Text);
            DateTime ngayhethan = h.ngaylap.AddMonths(h.soThang);
            BIENLAI b = new();
            if(CountTien(h) >= h.XEMAY.DONGXE.giaban)
            {
                MessageBox.Show("Đã trả hết, không thể tạo thêm biên lai.", "",MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //else if(h.BIENLAIs.Count >= h.soThang)
            //{
            //    MessageBox.Show("Đã quá hạn số tháng trả, không thể tạo.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            else if(String.IsNullOrWhiteSpace(txttien.Text))
            {
                MessageBox.Show("Chưa điền tiền đóng.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txttien.Focus();
                return;
            }

            else if (IsInt(txttien.Text)==false)
            {
                MessageBox.Show("Tiền đóng phải là số.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txttien.Focus();
                return;
            }

            //else if (int.Parse(txttien.Text) < h.dongiaHT)
            //{
            //    MessageBox.Show("Tiền đóng không đủ.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txttien.Focus();
            //    return;
            //}
            else if(DateTime.Now > ngayhethan)
            {
                MessageBox.Show("Đã quá hạn trả nợ, không thể tạo.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                b.maBienlai = CodeGen(dc);
                b.mahdtg = tblmaHD.Text;
                b.tienDong = (int)h.dongiaHT;
                //if (txttienthua.Text == "")
                //{
                //    b.tienDong = int.Parse(txttien.Text);
                //}
                //else
                //    b.tienDong = (int)h.dongiaHT;
                b.thangDong = h.BIENLAIs.Count + 1;
                b.ngayDongthuc = DateTime.Now;
                dc.BIENLAIs.AddObject(b);
                dc.SaveChanges();
                Close();
            }
        }

        private void txttien_TextChanged(object sender, TextChangedEventArgs e)
        {
            //HOPDONGTRAGOP h = FindHDTG(tblmaHD.Text);
            //if(txttien.Text != "")
            //{
            //    if (int.Parse(txttien.Text) > h.dongiaHT)
            //    {
            //        txttienthua.Text = (int.Parse(txttien.Text) - h.dongiaHT).ToString();
            //    }
            //    else
            //        txttienthua.Text = "";
            //}
        }
    }
}
