using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LapPhieunhap.xaml
    /// </summary>
    public partial class LapPhieunhap : Window
    {
        internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        int cx;
        public LapPhieunhap()
        {
            InitializeComponent();
            dpngaydat.SelectedDate = DateTime.Now;
            cmbnv.ItemsSource = dc.NHANVIENs.ToList();
            cmbpm.ItemsSource = dc.PHIEUDATMUAs.ToList();
        }
        private DONGXE FindDONGXE(string n)
        {
            DONGXE x = new DONGXE();
            foreach (var a in dc.DONGXEs)
            {
                if (a.maDong == n)
                {
                    x = a;
                    return x;
                }
            }
            return x;
        }
        public static string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;
                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.PHIEUNHAPXEs.Count();
            c++;
            string s = "0" + c;
            return s;
        }
        private bool isEmpty(string n)
        {
            foreach (var a in dc.PHIEUDATMUAs)
            {
                if(a.maPhieudat == n && a.PHIEUNHAPXEs.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddXe(string madong)
        {
            var c = cx;
            cx++;
            c++;
            string s = "0" + c;
            DONGXE d = FindDONGXE(madong);
            XEMAY x = new XEMAY();
            x.maxe = s;
            x.maDong = madong;
            x.mansx = d.mansx;
            x.matl = d.matl;
            x.mota = "";
            x.tenxe = d.tenDong;
            x.hinh = null;
            x.soKhung = GetUniqueKey(12);
            x.soMay = GetUniqueKey(12);
            foreach(var a in dc.DONGXEs)
            {
                if(a.maDong == d.maDong)
                {
                    a.soluongConlai++;
                }
            }
            x.baohanh = d.baohanh;
            x.baohanhcayso = d.baohanhcayso;
            x.trangthai = "CB";
            dc.XEMAYs.AddObject(x);
        }
        private void btnLap_Click(object sender, RoutedEventArgs e)
        {
            if(cmbnv.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhân viên.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbnv.Focus();
                return;
            }
            else if(cmbpm.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn phiếu đặt mua.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbpm.Focus();
                return;
            }
            else if(isEmpty(cmbpm.SelectedValue.ToString()) == false)
            {
                MessageBox.Show("Phiếu đặt mua này đã có phiếu nhập.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbpm.Focus();
                return;
            }
            else
            {
                cx = dc.XEMAYs.Count();
                PHIEUNHAPXE p = new PHIEUNHAPXE();
                p.maPhieunhap = CodeGen(dc);
                p.ngaynhap = dpngaydat.SelectedDate.Value;
                p.maPhieudat = cmbpm.SelectedValue.ToString();
                p.manv = cmbnv.SelectedValue.ToString();
                foreach (var a in dc.CHITIETPHIEUDATs)
                {
                    if(a.maPhieudat == p.maPhieudat)
                    {
                        CHITIETPHIEUNHAP c = new CHITIETPHIEUNHAP();
                        c.maPhieunhap = p.maPhieunhap;
                        c.maDong = a.maDong;
                        c.soluongnhap = a.soluongdat;
                        for(int i = 1 ; i <= c.soluongnhap ; i++)
                        {
                            AddXe(c.maDong);
                        }
                        c.dongianhapxe = a.DONGXE.giaban * a.soluongdat * 0.8;
                        dc.CHITIETPHIEUNHAPs.AddObject(c);
                    }
                }
                dc.PHIEUNHAPXEs.AddObject(p);
                dc.SaveChanges();
                Close();
            }
        }
    }
}
