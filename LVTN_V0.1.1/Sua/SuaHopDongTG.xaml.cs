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

namespace LVTN_V0._1._1.Sua
{
    /// <summary>
    /// Interaction logic for SuaHopDongTG.xaml
    /// </summary>
    public partial class SuaHopDongTG : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public string maKH = "";

        public SuaHopDongTG()
        {
            InitializeComponent();
            cmbxe.ItemsSource = dc.XEMAYs.ToList();
        }

        private XEMAY Findxe(string n)
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
        private string trangThai(string n)
        {
            string s = "";
            if (n == "Đang Trả")
            {
                s = "DT";
            }
            else if (n == "Trả Xong")
            {
                s = "TX";
            }
            else
                s = "VP";
            return s;
        }
        private KHACHHANG FindKH(string n)
        {
            KHACHHANG x = new();
            foreach (var a in dc.KHACHHANGs)
            {
                if (a.makh == n)
                {
                    x = a;
                    return x;
                }
            }
            return x;

        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnchonKH_Click(object sender, RoutedEventArgs e)
        {
            CustomWin.ChonKhachHang c = new();
            if (c.IsVisible) c.Focus();
            else
            {
                c.CheckMA += value => maKH = value;
                c.ShowDialog();
                KHACHHANG k = FindKH(maKH);
                btnchonKH.Content = k.tenkh;
            }

        }

        private void cmbxe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbxe.SelectedValue != null) 
            {
                XEMAY x = Findxe(cmbxe.SelectedValue.ToString());
                Uri uri = new("/Hinh/null.bmp", UriKind.Relative);
                if (x != null)
                {
                    if (x.hinh != null && x.hinh != "")
                    {
                        uri = new(x.hinh);
                    }
                    txtmota.Text = x.mota;
                    txtbaohanh.Text = x.baohanh.ToString() + " tháng" + " / " + x.baohanhcayso.ToString() + " Kilomét";
                    txtdongia.Text = x.DONGXE.giaban.ToString();
                }
                imgxe.Source = new BitmapImage(uri);
            }
        }
        private void txtsothangtra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtsothangtra.Text) || IsInt(txtsothangtra.Text) == false)
            {
                MessageBox.Show("Số tháng trả góp không hợp lệ.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                txtdongiaHT.Text = (int.Parse(txtdongia.Text) / int.Parse(txtsothangtra.Text)).ToString();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (cmbxe.SelectedValue == null)
            {
                cmbxe.Focus();
                MessageBox.Show("Chưa chọn xe máy.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Findxe(cmbxe.SelectedValue.ToString()).HOADONTRALIENs.Any())
            {
                MessageBox.Show("Xe máy đã bán.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (maKH == "")
            {
                MessageBox.Show("Chưa chọn khách hàng.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (String.IsNullOrWhiteSpace(txtsothangtra.Text))
            {
                MessageBox.Show("Chưa điền số tháng trả góp.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (IsInt(txtsothangtra.Text) == false)
            {
                MessageBox.Show("Số tháng trả góp phải là số.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                XEMAY x = Findxe(cmbxe.SelectedValue.ToString());
                HOPDONGTRAGOP h = new();
                h.mahdtg = txtma.Text;
                h.tenhopdong = "HDTG" + h.mahdtg;
                h.makh = maKH;
                h.manv = tblmanv.Text;
                h.maxe = cmbxe.SelectedValue.ToString();
                h.dongiaHT = x.DONGXE.giaban / int.Parse(txtsothangtra.Text);
                h.soThang = int.Parse(txtsothangtra.Text);
                h.trangthai = trangThai(cmbtrangthai.Text);
                foreach (var a in dc.HOPDONGTRAGOPs)
                {
                    if(a.mahdtg == h.mahdtg)
                    {
                        a.makh = h.makh;
                        a.manv = h.manv;
                        a.maxe = h.maxe;
                        a.trangthai = h.trangthai;
                        a.soThang = h.soThang;
                        a.dongiaHT = h.dongiaHT;
                        break;
                    }
                }
                dc.SaveChanges();
                Close();
            }
        }

    }
}
