using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for ThemXemay.xaml
    /// </summary>
    public partial class ThemXemay : Window
    {
        internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        string img = "";
        public ThemXemay()
        {
            InitializeComponent();
            cmbnsx.ItemsSource = dc.NHASANXUATs.ToList();
            cmbtl.ItemsSource = dc.THELOAIs.ToList();
            cmbdong.ItemsSource = dc.DONGXEs.ToList();
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
            var c = dc.XEMAYs.Count();
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
            cmbnsx.Text = "";
            cmbnsx.SelectedValue = null;
            cmbtl.Text = "";
            cmbtl.SelectedValue = null;
            cmbdong.Text = "";
            cmbdong.SelectedValue = null;
            txtmota.Text = "";
            img = "";
            txtsothangbh.Text = "";
            txtsokilometbh.Text = "";
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
            else if (cmbnsx.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhà sản xuất.");
                cmbnsx.Focus();
                return;
            }
            else if (cmbtl.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn thể loại xe.");
                cmbtl.Focus();
                return;
            }
            else if (cmbdong.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn thể loại xe.");
                cmbtl.Focus();
                return;
            }
            else if(String.IsNullOrWhiteSpace(txtsothangbh.Text))
            {
                MessageBox.Show("Chưa điền số tháng bảo hành.");
                txtsothangbh.Focus();
                return;
            }
            else if(IsInt(txtsothangbh.Text) == false)
            {
                MessageBox.Show("Số tháng bảo hành phải là số.");
                txtsothangbh.Focus();
                return;
            }
            else if (String.IsNullOrWhiteSpace(txtsokilometbh.Text))
            {
                MessageBox.Show("Chưa điền số kilomet bảo hành.");
                txtsokilometbh.Focus();
                return;
            }
            else if (IsInt(txtsokilometbh.Text) == false)
            {
                MessageBox.Show("Số kilomet bảo hành phải là số.");
                txtsokilometbh.Focus();
                return;
            }

            else
            {
                XEMAY x = new XEMAY();
                x.maxe = CodeGen(dc);
                x.tenxe = txtten.Text;
                x.mota = txtmota.Text;
                x.hinh = img;
                x.mansx = cmbnsx.SelectedValue.ToString();
                x.matl = cmbtl.SelectedValue.ToString();
                x.maDong = cmbdong.SelectedValue.ToString();
                x.soKhung = GetUniqueKey(12);
                x.soMay = GetUniqueKey(12);
                x.trangthai = "CB";
                x.baohanh = int.Parse(txtsothangbh.Text);
                x.baohanhcayso = int.Parse(txtsokilometbh.Text);
                dc.XEMAYs.AddObject(x);
                foreach(var a in dc.DONGXEs)
                {
                    if(a.maDong == x.maDong)
                    {
                        a.soluongConlai++;
                        break;
                    }
                }
                dc.SaveChanges();
                clear();
            }
        }

        private void btnPickhinh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image files (*.png;*.jpeg;*.bmp;*.jpg)|*.PNG;*.JPEG;*.BMP;*.JPG|All files (*.*)|*.*";
            if(o.ShowDialog() == true)
            {
                img = o.FileName;
                Uri uri = new Uri(img);
                imgxe.Source = new BitmapImage(uri);
            }
        }

        private void cmbdong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbdong.SelectedValue != null)
            {
                DONGXE xe = FindDONGXE(cmbdong.SelectedValue.ToString());
                txtten.Text = xe.tenDong;
                cmbtl.SelectedValue = xe.matl;
            }
        }
    }
}
