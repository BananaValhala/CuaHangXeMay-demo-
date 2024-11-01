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
    /// Interaction logic for ThemKhachhang.xaml
    /// </summary>
    public partial class ThemKhachhang : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        KHACHHANG kh = new KHACHHANG();
        public ThemKhachhang()
        {
            InitializeComponent();
        }
        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.KHACHHANGs.Count();
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
            txtdiachi.Text = "";
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
            txtdiachi.Text = trimmer.Replace(txtdiachi.Text, " ");
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
            else if (String.IsNullOrWhiteSpace(txtdiachi.Text) == true)
            {
                MessageBox.Show("Chưa điền địa chỉ.");
                txtdiachi.Focus();
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
            else if(IsInt(txtcmnd.Text) == false)
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
            else
            {
                kh.makh = CodeGen(dc);
                kh.tenkh = txtten.Text.Trim();
                kh.diachi = txtdiachi.Text;
                kh.CMND = txtcmnd.Text;
                kh.sdt = int.Parse(txtsdt.Text.Trim());
                dc.KHACHHANGs.AddObject(kh);
                dc.SaveChanges();
                clear();
            }
        }
    }
}
