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

namespace LVTN_V0._1._1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        public static string user="";
        public Login()
        {
            InitializeComponent();
        }
        //private bool tim(string n, string m)
        //{
        //    foreach (var a in dc.NHANVIENs)
        //    {
        //        if (a.tendn == n && a.matkhau == m)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        private NHANVIEN timNV(string n, string m)
        {
            foreach (var a in dc.NHANVIENs)
            {
                if (a.tendn == n && a.matkhau == m)
                {
                    return a;
                }
            }
            return null;
        }
        private void signin_manager(string n, string m)
        {
            NHANVIEN nv = timNV(n, m);
            if (nv.trangthai == false)
            {
                MessageBox.Show("USER LOCKED");
                return;
            }
            else
            {
                user = nv.tennv;
                MainWindow main = new MainWindow();
                this.Close();
                main.tbljob.Text = "QL";
                main.tbluser.Text = user.Substring(user.LastIndexOf(" ") + 1) + " ";
                main.tblma.Text = nv.manv;
                main.ShowDialog();

            }

        }
        private void signin_employee(string n, string m)
        {
            NHANVIEN nv = timNV(n, m);
            if (nv.trangthai == false)
            {
                MessageBox.Show("USER LOCKED");
                return;
            }
            else
            {
                user = nv.tennv;
                MainWindow main = new MainWindow();
                this.Close();
                main.tbljob.Text = "NV";
                main.tbluser.Text = user.Substring(user.LastIndexOf(" ") + 1) + " ";
                main.tblma.Text = nv.manv;
                main.mndongxe.Visibility = Visibility.Collapsed;
                main.mnnhanvien.Visibility = Visibility.Collapsed;
                main.mnphieudatmua.Visibility = Visibility.Collapsed;
                main.mnbaocao.Visibility = Visibility.Collapsed;
                //main.mnphieunhapxe.Visibility = Visibility.Collapsed;
                main.ShowDialog();

            }

        }
        private void btnsignin_Click(object sender, RoutedEventArgs e)
        {
            NHANVIEN nv = timNV(txtuser.Text,txtpass.Password);
            if (nv != null && nv.maChucvu == "01")
            {
                signin_manager(txtuser.Text, txtpass.Password);
            }
            else if (nv != null && nv.maChucvu == "02")
            {
                signin_employee(txtuser.Text, txtpass.Password);
            }
            else
            {
                MessageBox.Show("Sai User name hoặc password");
                return;
            }
        }
    }
}
