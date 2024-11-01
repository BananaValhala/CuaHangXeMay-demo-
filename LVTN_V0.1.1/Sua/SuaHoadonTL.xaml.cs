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
    /// Interaction logic for SuaHoadonTL.xaml
    /// </summary>
    public partial class SuaHoadonTL : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public string maKH = "";
        public SuaHoadonTL()
        {
            InitializeComponent();
            //cmbkh.ItemsSource = dc.KHACHHANGs.ToList();
            //cmbnv.ItemsSource = dc.NHANVIENs.ToList();
            cmbxe.ItemsSource = dc.XEMAYs.ToList();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            //if (cmbkh.SelectedValue == null)
            //{
            //    cmbkh.Focus();
            //    MessageBox.Show("Chưa chọn khách hàng mua xe.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //else if (cmbnv.SelectedValue == null)
            //{
            //    cmbnv.Focus();
            //    MessageBox.Show("Chưa chọn nhân viên lập hóa đơn.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
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
            else
            {
                XEMAY x = Findxe(cmbxe.SelectedValue.ToString());
                HOADONTRALIEN h = new HOADONTRALIEN();
                h.tenhoadon = txtten.Text;
                h.makh = maKH;
                h.manv = tblmanv.Text;
                //h.makh = cmbkh.SelectedValue.ToString();
                //h.manv = cmbnv.SelectedValue.ToString();
                h.maxe = cmbxe.SelectedValue.ToString();
                h.dongia = x.DONGXE.giaban;
                foreach(var a in dc.HOADONTRALIENs)
                {
                    if(a.mahdtl == txtma.Text)
                    {
                        a.makh = h.makh;
                        a.manv = h.manv;
                        //a.makh = cmbkh.SelectedValue.ToString();
                        //a.manv = cmbnv.SelectedValue.ToString();
                        a.maxe = cmbxe.SelectedValue.ToString();
                        a.dongia = h.dongia;
                    }
                }
                dc.SaveChanges();
                Close();
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

    }
}
