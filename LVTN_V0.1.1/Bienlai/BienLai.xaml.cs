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

namespace LVTN_V0._1._1.Bienlai
{
    /// <summary>
    /// Interaction logic for BienLai.xaml
    /// </summary>
    public partial class BienLai : Window
    {
        CUAHANGXEMAYEntities dc = new();
        public BienLai()
        {
            InitializeComponent();
        }

        private void btnthoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private HOPDONGTRAGOP FindHDTG(string s)
        {
            HOPDONGTRAGOP h = new();
            foreach(var a in dc.HOPDONGTRAGOPs)
            {
                if(a.mahdtg == s)
                {
                    h = a;
                    return h;
                }
            }
            return h;
        }
        private void HienThi()
        {
            CUAHANGXEMAYEntities ndc = new();
            dgBienlai.ItemsSource = ndc.BIENLAIs.Where(x => x.mahdtg == tblmaHD.Text);
            dc = ndc;
        }
        private void btnThemBienlai_Click(object sender, RoutedEventArgs e)
        {
            Bienlai.ThemBienlai b = new();
            HOPDONGTRAGOP h = FindHDTG(tblmaHD.Text);
            if (b.IsVisible) b.Focus();
            else
            {
                int tien = (int)h.dongiaHT;
                b.txttien.Text = tien.ToString();
                b.tblmaHD.Text = tblmaHD.Text;
                b.txtthang.Text = (h.BIENLAIs.Count + 1).ToString();
                b.ShowDialog();
                HienThi();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThi();
        }
    }
}
