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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LVTN_V0._1._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public MainWindow()
        {
            InitializeComponent();
            cmbtim_theloai.ItemsSource = dc.THELOAIs.ToList();
            cmbtim_nhasanxuat.ItemsSource = dc.NHASANXUATs.ToList();
        }
        private NHANVIEN FindNV(string ma)
        {
            NHANVIEN nv = new NHANVIEN();
            foreach (var a in dc.NHANVIENs)
            {
                if (a.manv == ma)
                {
                    nv = a;
                    return nv;
                }
            }
            return nv;
        }
        private void ReloadData()
        {
            CUAHANGXEMAYEntities ndc = new();
            dc = ndc;
            //cmbtim_theloai.ItemsSource = dc.THELOAIs.ToList();
            //cmbtim_nhasanxuat.ItemsSource = dc.NHASANXUATs.ToList();
        }
        private XEMAY FindXe(string n)
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

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Close();
            login.Show();
        }

        private void mnxe_Click(object sender, RoutedEventArgs e)
        {
            Xemay qlxe = new Xemay();
            Visibility = Visibility.Collapsed;
            qlxe.tbljob.Text = tbljob.Text;
            qlxe.tbluser.Text = tbluser.Text;
            qlxe.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mndongxe_Click(object sender, RoutedEventArgs e)
        {
            Dongxe dongxe = new Dongxe();
            Visibility= Visibility.Collapsed;
            dongxe.tbljob.Text = tbljob.Text;
            dongxe.tbluser.Text = tbluser.Text;
            dongxe.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mnQLkhachhang_Click(object sender, RoutedEventArgs e)
        {
            Khachhang qlkh = new Khachhang();
            Visibility = Visibility.Collapsed;
            qlkh.tbluser.Text = tbluser.Text;
            qlkh.tbljob.Text = tbljob.Text;
            qlkh.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mnQLnhanvien_Click(object sender, RoutedEventArgs e)
        {
            Nhanvien qlnv = new Nhanvien();
            Visibility = Visibility.Collapsed;
            qlnv.tbljob.Text = tbljob.Text;
            qlnv.tbluser.Text = tbluser.Text;
            qlnv.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mnhoadonTL_Click(object sender, RoutedEventArgs e)
        {
            HoadonTL hoadonTL = new HoadonTL();
            Visibility = Visibility.Collapsed;
            hoadonTL.tbljob.Text = tbljob.Text;
            hoadonTL.tbluser.Text = tbluser.Text;
            hoadonTL.tblma.Text = tblma.Text;
            hoadonTL.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mnhopdongTG_Click(object sender, RoutedEventArgs e)
        {
            if(tbljob.Text == "NV")
            {
                Them.ThemHopDongTG t = new();
                Visibility = Visibility.Collapsed;
                if (t.IsVisible) t.Focus();
                else
                {
                    NHANVIEN nv = FindNV(tblma.Text);
                    t.tblmanv.Text = tblma.Text;
                    t.txttennv.Text = nv.tennv;
                    t.ShowDialog();
                }
                Visibility = Visibility.Visible;
            }
            else
            {
                HopdongTG h = new();
                Visibility = Visibility.Collapsed;
                h.tbljob.Text = tbljob.Text;
                h.tbluser.Text = tbluser.Text;
                h.tblma.Text = tblma.Text;
                h.ShowDialog();
                Visibility = Visibility.Visible;
            }
            ReloadData();
        }

        private void mnlapPM_Click(object sender, RoutedEventArgs e)
        {
            Phieumuaxe phieumuaxe = new Phieumuaxe();
            Visibility = Visibility.Collapsed;
            phieumuaxe.tbljob.Text = tbljob.Text;
            phieumuaxe.tbluser.Text = tbluser.Text;
            phieumuaxe.tblma.Text = tblma.Text;
            phieumuaxe.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mnlapPN_Click(object sender, RoutedEventArgs e)
        {
            Phieunhapxe phieunhapxe = new Phieunhapxe();
            Visibility = Visibility.Collapsed;
            phieunhapxe.tbluser.Text = tbluser.Text;
            phieunhapxe.tbljob.Text = tbljob.Text;
            phieunhapxe.ShowDialog();
            Visibility = Visibility.Visible;
            ReloadData();
        }

        private void mnsuattcanhan_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaNhanvien snv = new Sua.SuaNhanvien();
            Visibility = Visibility.Collapsed;
            NHANVIEN nv = FindNV(tblma.Text);
            snv.txtma.Text = nv.manv;
            snv.txtten.Text = nv.tennv;
            snv.txtsdt.Text = nv.sdt.ToString();
            snv.txtcmnd.Text = nv.CMND;
            if (nv.gioitinh == true)
            {
                snv.rdonam.IsChecked = true;
            }
            else
                snv.rdonu.IsChecked = false;
            if (nv.trangthai == true)
            {
                snv.rdoactive.IsChecked = true;
            }
            else
                snv.rdoinactive.IsChecked = true;
            snv.dpngaylam.Text = nv.ngayVaoLam.ToString();
            snv.txtpasswd.Text = nv.matkhau;
            snv.txttendn.Text = nv.tendn;
            snv.dpngaylam.IsEnabled = false;
            snv.rdoactive.IsEnabled = false;
            snv.rdoinactive.IsEnabled = false;
            snv.ShowDialog();
            nv = FindNV(tblma.Text);
            tbluser.Text = nv.tennv.Substring(nv.tennv.LastIndexOf(" ") + 1) + " ";
            Visibility = Visibility.Visible;
            ReloadData();
        }
        private void dgXemay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgXemay.SelectedValue != null)
            {
                Uri uri = new("/Hinh/null.bmp", UriKind.Relative);
                XEMAY x = FindXe(dgXemay.SelectedValue.ToString());
                if (x != null)
                {
                    if (x.hinh != null && x.hinh != "")
                    {
                        uri = new(x.hinh);
                    }
                    tblmota.Text = x.mota;
                    tblbaohanh.Text = x.baohanh.ToString() + " tháng" + " / " + x.baohanhcayso.ToString() + " Kilomét";
                    tblSoKvaSoM.Text = x.soKhung + "    " + x.soMay;
                }
                imgHinhxe.Source = new BitmapImage(uri);
            }
        }

        private void cmbtim_nhasanxuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nsx = cmbtim_nhasanxuat.SelectedValue.ToString();
            cmbtim_dongxe.ItemsSource = null;
            var listdongxe = dc.DONGXEs.Where(x => x.mansx == nsx);
            cmbtim_dongxe.ItemsSource = listdongxe.ToList();
        }

        private void btntimkiem_Click(object sender, RoutedEventArgs e)
        {
            string tl = "";
            string nsx = "";
            string dongxe = "";
            if(cmbtim_theloai.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn thể loại.","",MessageBoxButton.OK,MessageBoxImage.Error);
                cmbtim_theloai.Focus();
                return;
            }
            else
            {
                tl = cmbtim_theloai.SelectedValue.ToString();
                if (cmbtim_nhasanxuat.SelectedValue == null)
                {
                    var listxe = dc.XEMAYs.Where(x => x.matl == tl);
                    dgXemay.ItemsSource = listxe.ToList();
                }
                else if(cmbtim_nhasanxuat.SelectedValue != null && cmbtim_dongxe.SelectedValue == null)
                {
                    nsx = cmbtim_nhasanxuat.SelectedValue.ToString();
                    var listxe = dc.XEMAYs.Where(x => x.matl == tl && x.mansx == nsx);
                    dgXemay.ItemsSource = listxe.ToList();
                }
                else if(cmbtim_nhasanxuat.SelectedValue != null && cmbtim_dongxe.SelectedValue != null)
                {
                    nsx = cmbtim_nhasanxuat.SelectedValue.ToString();
                    dongxe = cmbtim_dongxe.SelectedValue.ToString();
                    var listxe = dc.XEMAYs.Where(x => x.matl == tl && x.mansx == nsx && x.maDong == dongxe);
                    dgXemay.ItemsSource = listxe.ToList();
                }
            }
        }

        private void mnbaocao_Click(object sender, RoutedEventArgs e)
        {
            CustomWin.Report r = new();
            Visibility = Visibility.Collapsed;
            r.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
