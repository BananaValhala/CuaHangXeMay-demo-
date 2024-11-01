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

namespace LVTN_V0._1._1.Them
{
    /// <summary>
    /// Interaction logic for ThemNhanvien.xaml
    /// </summary>
    public partial class ThemNhanvien : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        NHANVIEN nv = new NHANVIEN();
        public ThemNhanvien()
        {
            InitializeComponent();
        }
        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.NHANVIENs.Count();
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
        private bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }
        private void clear()
        {
            txtten.Text = "";
            txtsdt.Text = "";
            txtcmnd.Text = "";
            dpngaylam.Text = "";
            rdonam.IsChecked = true;
            txtten.Focus();
        }
        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("CMND phải đủ 9 số.");
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
                NHANVIEN nv = new NHANVIEN();
                nv.manv = CodeGen(dc);
                nv.maChucvu = "02";
                nv.tennv = txtten.Text;
                nv.tendn = txtten.Text;
                nv.sdt = int.Parse(txtsdt.Text);
                if(rdonam.IsChecked == true)
                {
                    nv.gioitinh = true;
                }
                else
                    nv.gioitinh = false;
                nv.ngayVaoLam = dpngaylam.SelectedDate.Value;
                nv.matkhau = "123456";
                nv.CMND = txtcmnd.Text;
                nv.trangthai = true;
                dc.NHANVIENs.AddObject(nv);
                dc.SaveChanges();
                clear();
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
    }
}
