using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SuaNhanvien.xaml
    /// </summary>
    public partial class SuaNhanvien : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public SuaNhanvien()
        {
            InitializeComponent();
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
        private bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        private NHANVIEN Find(string n)
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

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            Regex trimmer = new Regex(@"\s\s+");
            txtten.Text = trimmer.Replace(txtten.Text, " ");
            if (String.IsNullOrWhiteSpace(txtten.Text) == true)
            {
                MessageBox.Show("Chưa điền tên.");
                txtten.Focus();
                return;
            }
            else if (String.IsNullOrWhiteSpace(txtcmnd.Text) == true)
            {
                MessageBox.Show("Chưa điền CMND.");
                txtcmnd.Focus();
                return;
            }
            else if (String.IsNullOrWhiteSpace(txtsdt.Text) == true)
            {
                MessageBox.Show("Chưa điền số điện thoại.");
                txtsdt.Focus();
                txtsdt.Select(txtsdt.Text.Length, 0);
                return;
            }
            else if (txtsdt.Text.Length != 10 || txtsdt.Text.Length < 10 || txtsdt.Text.Length > 10)
            {
                MessageBox.Show("Số Điện Thoại phải có đủ 10 số.");
                txtsdt.Focus();
                txtsdt.Select(txtsdt.Text.Length, 0);
                return;
            }
            else if (HasSpecialChars(txtsdt.Text.ToString()) == true)
            {
                MessageBox.Show("Số điện thoại không thể có khoảng trắng hoặc kí tự đặc biệt.");
                txtsdt.Focus();
                txtsdt.Select(txtsdt.Text.Length, 0);
                return;
            }
            else if (IsInt(txtsdt.Text) == false)
            {
                MessageBox.Show("Số điện thoại không được có chữ.");
                return;
            }
            else if (HasSpecialChars(txtcmnd.Text.ToString()) == true)
            {
                MessageBox.Show("CMND không thể có khoảng trắng hoặc kí tự đặc biệt.");
                txtcmnd.Focus();
                txtcmnd.Select(txtcmnd.Text.Length, 0);
                return;
            }
            else if (IsInt(txtcmnd.Text) == false)
            {
                MessageBox.Show("CMND phải là số.");
                return;
            }
            else if (txtcmnd.Text.Length != 9)
            {
                MessageBox.Show("CMND phải đủ 9 số");
                txtcmnd.Focus();
                txtcmnd.Select(txtcmnd.Text.Length, 0);
                return;
            }
            else if (dpngaylam.SelectedDate == null)
            {
                MessageBox.Show("Chưa chọn ngày vào làm.");
                dpngaylam.Focus();
                return;
            }
            else if (dpngaylam.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Ngày vào làm không thể là ngày tương lai, chưa đi làm mà vào làm sao được. :P");
                dpngaylam.Focus();
                return;
            }
            else if (dpngaylam.SelectedDate.Value.Year < DateTime.Now.Year - 30)
            {
                MessageBox.Show("Ngày vào làm không được xa hơn 30 năm từ bây giờ.");
                dpngaylam.Focus();
                return;
            }
            else
            {
                NHANVIEN nv = Find(txtma.Text);
                if(nv != null)
                {
                    nv.tennv = txtten.Text;
                    nv.sdt = int.Parse(txtsdt.Text);
                    nv.ngayVaoLam = dpngaylam.SelectedDate.Value;
                    if(rdonam.IsChecked == true)
                    {
                        nv.gioitinh = true;
                    }
                    else
                        nv.gioitinh = false;
                    nv.CMND = txtcmnd.Text;
                    if(rdoactive.IsChecked == true)
                    {
                        nv.trangthai = true;
                    }
                    else
                        nv.trangthai= false;
                    nv.tendn = txttendn.Text;
                    nv.matkhau = txtpasswd.Text;
                    foreach(var a in dc.NHANVIENs)
                    {
                        if (a.manv == txtma.Text)
                        {
                            a.tennv = nv.tennv;
                            a.sdt = nv.sdt;
                            a.gioitinh = nv.gioitinh;
                            a.CMND = nv.CMND;
                            a.ngayVaoLam = nv.ngayVaoLam;
                            a.trangthai = nv.trangthai;
                            a.matkhau = nv.matkhau;
                            a.tendn = nv.tendn;
                            break;
                        }
                    }
                    dc.SaveChanges();
                    Close();
                }
            }
        }

        private void rdonam_Click(object sender, RoutedEventArgs e)
        {
            rdonu.IsChecked = false;
        }

        private void rdonu_Click(object sender, RoutedEventArgs e)
        {
            rdonam.IsChecked = false;
        }

        private void rdoactive_Click(object sender, RoutedEventArgs e)
        {
            rdoinactive.IsChecked = false;
        }

        private void rdoinactive_Click(object sender, RoutedEventArgs e)
        {
            rdoactive.IsChecked = false;
        }
    }
}
