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

namespace LVTN_V0._1._1.Them
{
    /// <summary>
    /// Interaction logic for ThemHoadonTL.xaml
    /// </summary>
    public partial class ThemHoadonTL : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public string maKH = "";
        public ThemHoadonTL()
        {
            InitializeComponent();
            //cmbkh.ItemsSource = dc.KHACHHANGs.ToList();
            //cmbnv.ItemsSource = dc.NHANVIENs.ToList();
            cmbxe.ItemsSource = dc.XEMAYs.ToList();
            txtten.Text = "HDTL" + CodeGen(dc);
        }
        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.HOADONTRALIENs.Count();
            c++;
            string s = "0" + c;
            return s;
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
            KHACHHANG x = new ();
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

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            //if(cmbkh.SelectedValue == null)
            //{
            //    cmbkh.Focus();
            //    MessageBox.Show("Chưa chọn khách hàng mua xe.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //if(cmbnv.SelectedValue == null)
            //{
            //    cmbnv.Focus();
            //    MessageBox.Show("Chưa chọn nhân viên lập hóa đơn.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            if(cmbxe.SelectedValue == null)
            {
                cmbxe.Focus();
                MessageBox.Show("Chưa chọn xe máy.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(Findxe(cmbxe.SelectedValue.ToString()).HOADONTRALIENs.Any()|| (Findxe(cmbxe.SelectedValue.ToString()).HOPDONGTRAGOPs.Any()))
            {
                MessageBox.Show("Xe máy đã bán.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(maKH == "")
            {
                MessageBox.Show("Chưa chọn khách hàng.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                XEMAY x = Findxe(cmbxe.SelectedValue.ToString());
                HOADONTRALIEN h = new HOADONTRALIEN();
                h.mahdtl = CodeGen(dc);
                h.tenhoadon = "HDTL"+h.mahdtl;
                //h.makh = cmbkh.SelectedValue.ToString();
                h.makh = maKH;
                h.manv = tblmanv.Text;
                h.maxe = cmbxe.SelectedValue.ToString();
                h.dongia = x.DONGXE.giaban;
                h.ngaylap = DateTime.Now;
                x.trangthai = "DB";
                dc.HOADONTRALIENs.AddObject(h);
                dc.SaveChanges();
                Close();
            }
        }
        private void cmbxe_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                txtbaohanh.Text = x.baohanh.ToString()+ " tháng" + " / " + x.baohanhcayso.ToString() + " Kilomét";
                txtdongia.Text = x.DONGXE.giaban.ToString();
            }
            imgxe.Source = new BitmapImage(uri);
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
