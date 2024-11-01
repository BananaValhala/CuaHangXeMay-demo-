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
    /// Interaction logic for ThemDongxe.xaml
    /// </summary>
    public partial class ThemDongxe : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        //string nsx = "";
        //string tl = "";
        public ThemDongxe()
        {
            InitializeComponent();
            cmbnsx.ItemsSource = dc.NHASANXUATs.ToList();
            cmbtl.ItemsSource = dc.THELOAIs.ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        //private string FindNSX(string s)
        //{
        //    NHASANXUAT d = new NHASANXUAT();
        //    foreach (var a in dc.NHASANXUATs)
        //    {
        //        if (a.tennsx == s)
        //        {
        //            d = a;
        //            break;
        //        }
        //    }
        //    return d.mansx;
        //}
        //private string FindTL(string s)
        //{
        //    THELOAI t = new THELOAI();
        //    foreach (var a in dc.THELOAIs)
        //    {
        //        if (a.tentl == s)
        //        {
        //            t = a;
        //            break;
        //        }
        //    }
        //    return t.matl;
        //}
        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.DONGXEs.Count();
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
            txtgia.Text = "";
            txtmau.Text = "";
            cmbnsx.Text = "";
            cmbnsx.SelectedValue = null;
            cmbtl.Text = "";
            cmbtl.SelectedValue = null;
            txtbaohanh.Text = "";
            txtkilometbaohanh.Text = "";
            txtten.Focus();
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
            else if(String.IsNullOrWhiteSpace(txtgia.Text) == true)
            {
                MessageBox.Show("Chưa điền giá.");
                txtgia.Focus();
                return;
            }
            else if (IsInt(txtgia.Text) == false)
            {
                MessageBox.Show("Giá không được có chữ.");
                txtgia.Focus();
                return;
            }
            else if (String.IsNullOrWhiteSpace(txtmau.Text) == true)
            {
                MessageBox.Show("Chưa điền màu.");
                txtmau.Focus();
                return;
            }
            else if(IsInt(txtmau.Text) == true)
            {
                MessageBox.Show("Màu không có số.");
                txtmau.Focus();
                return;
            }
            else if(HasSpecialChars(txtmau.Text))
            {
                MessageBox.Show("Màu không có ký hiệu đặc biệt.");
                txtmau.Focus();
                return;
            }
            else if(cmbnsx.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhà sản xuất.");
                cmbnsx.Focus();
                return;
            }
            else if(cmbtl.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn thể loại xe.");
                cmbtl.Focus();
                return;
            }
            else if(String.IsNullOrWhiteSpace(txtbaohanh.Text))
            {
                MessageBox.Show("Chưa điền số tháng bảo hành.");
                txtbaohanh.Focus();
                return ;
            }
            else if(IsInt(txtbaohanh.Text) == false)
            {
                MessageBox.Show("Số tháng bảo hành phải là số.");
                txtbaohanh.Focus();
                return;
            }
            else if (String.IsNullOrWhiteSpace(txtkilometbaohanh.Text))
            {
                MessageBox.Show("Chưa điền số kilomet bảo hành.");
                txtkilometbaohanh.Focus();
                return;
            }
            else if (IsInt(txtkilometbaohanh.Text) == false)
            {
                MessageBox.Show("Số kilomet bảo hành phải là số.");
                txtkilometbaohanh.Focus();
                return;
            }
            else
            {
                DONGXE dx = new DONGXE();
                dx.maDong = CodeGen(dc);
                dx.tenDong = txtten.Text;
                dx.giaban = int.Parse(txtgia.Text);
                dx.mau = txtmau.Text;
                dx.mansx = cmbnsx.SelectedValue.ToString();
                dx.matl = cmbtl.SelectedValue.ToString();
                dx.baohanh = int.Parse( txtbaohanh.Text);
                dx.baohanhcayso = int.Parse(txtkilometbaohanh.Text);
                dc.DONGXEs.AddObject(dx);
                dc.SaveChanges();
                clear();
            }
        }
        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmbnsx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //nsx = FindNSX(cmbnsx.Text);
        }

        private void cmbtl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //tl = FindTL(cmbtl.Text);
        }
    }
}
