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
    /// Interaction logic for HopdongTG.xaml
    /// </summary>
    public partial class HopdongTG : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public HopdongTG()
        {
            InitializeComponent();
        }

        private void Hienthi()
        {
            CUAHANGXEMAYEntities ndc = new();
            dgHDTG.ItemsSource = ndc.HOPDONGTRAGOPs.ToList();
            if (tbljob.Text == "NV")
            {
                foreach (DataGridColumn c in dgHDTG.Columns)
                {
                    if (c.Header.ToString() == "Edit" || c.Header.ToString() == "Delete")
                    {
                        c.Visibility = Visibility.Collapsed;
                    }
                }
            }
            dc = ndc;
        }
        private HOPDONGTRAGOP Find(string n)
        {
            HOPDONGTRAGOP h = new();
            foreach (var a in dc.HOPDONGTRAGOPs)
            {
                if (a.mahdtg == n)
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

        private void btnThemHDTG_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemHopDongTG t = new();
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

        private void btnEdithopdong_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaHopDongTG s = new();
            if(s.IsVisible) s.Focus();
            else
            {
                HOPDONGTRAGOP h = Find(dgHDTG.SelectedValue.ToString());
                NHANVIEN nv = FindNV(tblma.Text);
                s.tblmanv.Text = tblma.Text;
                s.txttennv.Text = nv.tennv;
                s.maKH = h.makh;
                if(h.trangthai == "DT")
                {
                    s.cmbtrangthai.SelectedItem = "Đang Trả";
                    //s.cmbtrangthai.SelectedValue = "Đang Trả";
                    s.cmbtrangthai.Text = "Đang Trả";
                }
                else if(h.trangthai == "TX")
                {
                    s.cmbtrangthai.SelectedItem = "Trả Xong";
                    //s.cmbtrangthai.SelectedValue = "Trả Xong";
                    s.cmbtrangthai.Text = "Trả Xong";
                }
                else
                {
                    s.cmbtrangthai.SelectedItem = "Vi Phạm";
                    //s.cmbtrangthai.SelectedValue = "Vi Phạm";
                    s.cmbtrangthai.Text = "Vi Phạm";
                }
                s.cmbxe.SelectedValue = h.maxe;
                s.txtdongia.Text = h.XEMAY.DONGXE.giaban.ToString();
                s.txtten.Text = h.tenhopdong;
                s.txtma.Text = h.mahdtg;
                s.txtsothangtra.Text = h.soThang.ToString();
                s.txtmota.Text = h.XEMAY.mota;
                s.btnchonKH.Content = h.KHACHHANG.tenkh;
                s.txtmota.Text = h.XEMAY.mota;
                s.txtbaohanh.Text = h.XEMAY.baohanh.ToString() + " tháng" + " / " + h.XEMAY.baohanhcayso.ToString() + " Kilomét";
                Uri uri = new("/Hinh/null.bmp", UriKind.Relative);
                if (h.XEMAY.hinh != null && h.XEMAY.hinh != "")
                {
                    uri = new(h.XEMAY.hinh);
                }
                s.imgxe.Source = new BitmapImage(uri);
                s.txtdongiaHT.Text = (h.XEMAY.DONGXE.giaban / h.soThang).ToString();
                s.ShowDialog();
            }
            Hienthi();
        }

        private void btnDeletehopdong_Click(object sender, RoutedEventArgs e)
        {
            HOPDONGTRAGOP h = Find(dgHDTG.SelectedValue.ToString());
            if(h.BIENLAIs.Any())
            {
                MessageBox.Show("Không thể xóa, đã có biên lai.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Bạn thật sự muốn xóa hợp đồng này ?", "Xóa Hợp Đồng", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    h.XEMAY.trangthai = "CB";
                    dc.HOPDONGTRAGOPs.DeleteObject(h);
                    dc.SaveChanges();
                    Hienthi();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void btnxembienlai_Click(object sender, RoutedEventArgs e)
        {
            Bienlai.BienLai b = new();
            if (b.IsVisible) b.Focus();
            else
            {
                b.tblmaHD.Text = dgHDTG.SelectedValue.ToString();
                b.ShowDialog();
            }
        }
    }
}
