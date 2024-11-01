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
    /// Interaction logic for HoadonTL.xaml
    /// </summary>
    public partial class HoadonTL : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public HoadonTL()
        {
            InitializeComponent();
        }
        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new();
            dgHDTL.ItemsSource = ndc.HOADONTRALIENs.ToList();
            if(tbljob.Text == "NV")
            {
                foreach(DataGridColumn c in dgHDTL.Columns)
                {
                    if(c.Header.ToString() == "Edit" || c.Header.ToString() == "Delete")
                    {
                        c.Visibility = Visibility.Collapsed;
                    }
                }
            }
            dc = ndc;
        }
        private HOADONTRALIEN Find(string n)
        {
            HOADONTRALIEN h = new HOADONTRALIEN();
            foreach (var a in dc.HOADONTRALIENs)
            {
                if (a.mahdtl == n)
                {
                    h = a;
                    return h;
                }
            }
            return h;
        }
        private NHANVIEN FindNV(string n)
        {
            NHANVIEN nv = new NHANVIEN();
            foreach (var a in dc.NHANVIENs)
            {
                if (a.manv == n)
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

        private void btnThemHDTL_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemHoadonTL t = new Them.ThemHoadonTL();
            if (t.IsVisible) t.Focus();
            else
            {
                NHANVIEN nv = FindNV(tblma.Text);
                t.tblmanv.Text = tblma.Text;
                t.txttennv.Text = nv.tennv;
                t.ShowDialog();
            }
            Hienthi();
        }
        private void btnEdithoadon_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaHoadonTL s = new Sua.SuaHoadonTL();
            if (s.IsVisible) s.Focus();
            else
            {
                HOADONTRALIEN h = Find(dgHDTL.SelectedValue.ToString());
                s.txtma.Text = h.mahdtl;
                s.txtten.Text = h.tenhoadon;
                s.txtmota.Text = h.XEMAY.mota;
                Uri uri = new("/Hinh/null.bmp", UriKind.Relative);
                if (h.XEMAY.hinh != null && h.XEMAY.hinh != "")
                {
                    uri = new(h.XEMAY.hinh);
                }
                s.imgxe.Source = new BitmapImage(uri);
                s.maKH = h.KHACHHANG.makh;
                s.tblmanv.Text = h.NHANVIEN.manv;
                s.txttennv.Text = h.NHANVIEN.tennv;
                //s.cmbkh.SelectedValue = h.KHACHHANG.makh;
                //s.cmbnv.SelectedValue = h.NHANVIEN.manv;
                s.cmbxe.SelectedValue = h.maxe;
                s.btnchonKH.Content = h.KHACHHANG.tenkh;
                s.txtmota.Text = h.XEMAY.mota;
                s.txtbaohanh.Text = h.XEMAY.baohanh.ToString() + " tháng" + " / " + h.XEMAY.baohanhcayso.ToString() + " Kilomét";
                s.txtdongia.Text = h.XEMAY.DONGXE.giaban.ToString();
                s.ShowDialog();
            }
            Hienthi();
        }

        private void btnDeletehoadon_Click(object sender, RoutedEventArgs e)
        {
            HOADONTRALIEN h = Find(dgHDTL.SelectedValue.ToString());
            MessageBoxResult result = MessageBox.Show("Bạn thật sự muốn xóa hóa đơn này ?", "Xóa Hóa Đơn", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    h.XEMAY.trangthai = "CB";
                    dc.HOADONTRALIENs.DeleteObject(h);
                    dc.SaveChanges();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
