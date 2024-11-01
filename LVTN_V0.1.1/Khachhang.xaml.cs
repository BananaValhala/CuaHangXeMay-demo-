using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
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
    /// Interaction logic for Khachhang.xaml
    /// </summary>
    public partial class Khachhang : Window
    {
        private CUAHANGXEMAYEntities dc = new CUAHANGXEMAYEntities();
        //private ObservableCollection<KHACHHANG> collection;
        public Khachhang()
        {
            InitializeComponent();
            //collection = new ObservableCollection<KHACHHANG>();
        }

        private void Hienthi()
        {
            //dgKhachhang.ItemsSource = null;
            //collection.Clear();
            CUAHANGXEMAYEntities ndc = new CUAHANGXEMAYEntities();
            //foreach (var a in ndc.KHACHHANGs)
            //{
            //    collection.Add(a);
            //}
            dgKhachhang.ItemsSource = ndc.KHACHHANGs.ToList();
            dc = ndc;
        }
        private KHACHHANG Find(string n)
        {
            KHACHHANG kh = new KHACHHANG();
            foreach (var a in dc.KHACHHANGs)
            {
                if (a.makh == dgKhachhang.SelectedValue.ToString())
                {
                    kh = a;
                    return kh;
                }
            }
            return null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hienthi();
        }
        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnThemKhachhang_Click(object sender, RoutedEventArgs e)
        {
            Them.ThemKhachhang tkh = new Them.ThemKhachhang();
            if (tkh.IsVisible) tkh.Focus();
            else
                tkh.ShowDialog();
            Hienthi();
        }

        private void btnEditKhachhang_Click(object sender, RoutedEventArgs e)
        {
            Sua.SuaKhachhang skh = new Sua.SuaKhachhang();
            if (skh.IsVisible)skh.Focus();
            else
            {
                KHACHHANG kh = Find(dgKhachhang.SelectedValue.ToString());
                skh.txtma.Text = kh.makh;
                skh.txtten.Text = kh.tenkh;
                skh.txtsdt.Text = kh.sdt.ToString();
                skh.txtdiachi.Text = kh.diachi;
                skh.txtcmnd.Text = kh.CMND;
                skh.ShowDialog();
                Hienthi();
            }
        }

        private void btnDeleteKhachhang_Click(object sender, RoutedEventArgs e)
        {
            KHACHHANG kh = Find(dgKhachhang.SelectedValue.ToString());
            MessageBoxResult result = MessageBox.Show("Bạn thật sự muốn xóa khách hàng này ?", "Xóa khách hàng", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (kh != null && kh.HOADONTRALIENs.Count == 0 && kh.HOPDONGTRAGOPs.Count == 0) 
                    { 
                        dc.KHACHHANGs.DeleteObject(kh); 
                        dc.SaveChanges();
                        Hienthi();
                    }
                    else
                    {
                        MessageBox.Show("Khách hàng có khóa ngoại, không được xóa.");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
