using Microsoft.Win32;
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
    /// Interaction logic for SuaXemay.xaml
    /// </summary>
    public partial class SuaXemay : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public string img = "";
        public SuaXemay()
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
            return null;
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
        private XEMAY Find(string n)
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
            return null;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPickhinh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image files (*.png;*.jpeg;*.bmp;*.jpg)|*.PNG;*.JPEG;*.BMP;*.JPG|All files (*.*)|*.*";
            if (o.ShowDialog() == true)
            {
                img = o.FileName;
                Uri uri = new Uri(img);
                imgxe.Source = new BitmapImage(uri);
            }
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
            else if (String.IsNullOrWhiteSpace(txtsothangbh.Text))
            {
                MessageBox.Show("Chưa điền số tháng bảo hành.");
                txtsothangbh.Focus();
                return;
            }
            else if (IsInt(txtsothangbh.Text) == false)
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
                foreach(var a in dc.XEMAYs)
                {
                    if(a.maxe == txtma.Text)
                    {
                        a.tenxe = txtten.Text;
                        a.matl = cmbtl.SelectedValue.ToString();
                        a.mansx = cmbnsx.SelectedValue.ToString();
                        string oldmadong = a.maDong;
                        a.maDong = cmbdong.SelectedValue.ToString();
                        a.baohanh = int.Parse(txtsothangbh.Text);
                        a.baohanhcayso = int.Parse(txtsokilometbh.Text);
                        if(a.maDong != oldmadong)
                        {
                            foreach (var s in dc.DONGXEs)
                            {
                                if (s.maDong == a.maDong)
                                {
                                    s.soluongConlai++;
                                }
                                if (s.maDong == oldmadong)
                                {
                                    s.soluongConlai--;
                                }
                            }
                        }
                        a.hinh = img;
                        a.mota = txtmota.Text;
                        break;
                    }
                }
                dc.SaveChanges();
                Close();
            }
        }

        private void cmbdong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbdong.SelectedValue != null)
            {
                DONGXE xe = FindDONGXE(cmbdong.SelectedValue.ToString());
                txtten.Text = xe.tenDong;
                cmbtl.SelectedValue = xe.matl;
            }
        }
    }
}
