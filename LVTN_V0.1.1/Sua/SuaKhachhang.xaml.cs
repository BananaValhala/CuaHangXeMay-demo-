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
    /// Interaction logic for SuaKhachhang.xaml
    /// </summary>
    public partial class SuaKhachhang : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public SuaKhachhang()
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

        private KHACHHANG Find(string n)
        {
            KHACHHANG kh = new KHACHHANG();
            foreach (var a in dc.KHACHHANGs)
            {
                if (a.makh == n)
                {
                    kh = a;
                    return kh;
                }
            }
            return null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
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
            else 
            {
                KHACHHANG kh = Find(txtma.Text);
                if (kh != null)
                {
                    kh.tenkh = txtten.Text;
                    kh.diachi = txtdiachi.Text;
                    kh.CMND = txtcmnd.Text;
                    kh.sdt = int.Parse(txtsdt.Text);
                    foreach (var a in dc.KHACHHANGs)
                    {
                        if (a.makh == txtma.Text)
                        {
                            a.tenkh = kh.tenkh;
                            a.diachi = kh.diachi;
                            a.CMND = kh.CMND;
                            a.sdt = kh.sdt;
                            break;
                        }
                    }
                    dc.SaveChanges();
                    Close();
                }
            }
        }
    }
}
