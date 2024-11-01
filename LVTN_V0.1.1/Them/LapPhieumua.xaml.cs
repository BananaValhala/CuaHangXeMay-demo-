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

namespace LVTN_V0._1._1.Them
{
    /// <summary>
    /// Interaction logic for LapPhieumua.xaml
    /// </summary>
    public partial class LapPhieumua : Window
    {
        CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        List<CHITIETPHIEUDAT> lc = new();
        List<DONGXE> ldx = new();
        public LapPhieumua()
        {
            InitializeComponent();
            dpngaydat.SelectedDate = DateTime.Now;
            //cmbnv.ItemsSource = dc.NHANVIENs.ToList();
            cmbnsx.ItemsSource = dc.NHASANXUATs.ToList();
            //cmbdong.ItemsSource = dc.DONGXEs.ToList();
        }
        private void Hienthi()
        {
            dgCTPDM.ItemsSource = lc;
        }
        private string CodeGen(CUAHANGXEMAYEntities dc)
        {
            var c = dc.PHIEUDATMUAs.Count();
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

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLap_Click(object sender, RoutedEventArgs e)
        {
            if(dpngaydat.SelectedDate == null)
            {
                MessageBox.Show("Chưa chọn ngày đặt mua.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                dpngaydat.Focus();
                return;
            }
            //else if(dpngaydat.SelectedDate < DateTime.Now)
            //{
            //    MessageBox.Show("Không được chọn ngày đặt mua trong quá khứ.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    dpngaydat.Focus();
            //    return;
            //}
            else if(cmbnsx.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhà sản xuất.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbnsx.Focus();
                return;
            }
            else if(cmbdong.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn dòng xe.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbdong.Focus();
                return;
            }
            //else if(cmbnv.SelectedValue == null)
            //{
            //    MessageBox.Show("Chưa chọn nhân viên.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    cmbnv.Focus();
            //    return;
            //}
            else if (dgCTPDM.Items.Count == 0)
            {
                MessageBox.Show("Chưa đặt mua.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                btnthemvaolist.Focus();
                return;
            }
            else
            {
                PHIEUDATMUA p = new PHIEUDATMUA();
                p.maPhieudat = CodeGen(dc);
                p.mansx = cmbnsx.SelectedValue.ToString();
                p.manv = tblmanv.Text;
                p.ngaydat = dpngaydat.SelectedDate.Value;
                dc.PHIEUDATMUAs.AddObject(p);
                foreach (var a in lc)
                {
                    dc.CHITIETPHIEUDATs.AddObject(a);
                }
                dc.SaveChanges();
                Close();
            }
        }

        private void btnthemvaolist_Click(object sender, RoutedEventArgs e)
        {
            if (txtsoluong.Text != "")
            {
                if (IsInt(txtsoluong.Text) == true && HasSpecialChars(txtsoluong.Text) == false)
                {
                    if (cmbdong.SelectedValue != null && int.Parse(txtsoluong.Text) > 0)
                    {
                        CHITIETPHIEUDAT C = new CHITIETPHIEUDAT();
                        C.maPhieudat = CodeGen(dc);
                        C.maDong = cmbdong.SelectedValue.ToString();
                        C.soluongdat = int.Parse(txtsoluong.Text);
                        string flag = "";
                        foreach(var a in lc)
                        {
                            if(a.maDong == C.maDong)
                            {
                                a.soluongdat += C.soluongdat;
                                flag = a.maDong;
                            }
                        }
                        if(flag != C.maDong)
                        {
                            lc.Add(C);
                        }
                        Hienthi();
                    }
                    else
                        MessageBox.Show("Chưa chọn dòng xe.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Số lượng không được là chữ hoặc ký tự đặc biệt.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Chưa điền số lượng.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cmbnsx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbdong.ItemsSource = null;
            ldx.Clear();
            foreach(var a in dc.DONGXEs)
            {
                if(a.mansx == cmbnsx.SelectedValue.ToString())
                {
                    ldx.Add(a);
                }
            }
            cmbdong.ItemsSource = ldx;
        }
    }
}
