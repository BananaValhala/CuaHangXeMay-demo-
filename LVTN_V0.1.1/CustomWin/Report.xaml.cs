using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace LVTN_V0._1._1.CustomWin
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        CUAHANGXEMAYEntities dc = new();
        List<XEMAY>? Listxemay = new();
        List<PHIEUDATMUA>? Listdatmua = new();
        List<PHIEUNHAPXE>? Listnhapxe = new();
        List<HOADONTRALIEN>? Listhoadon = new();
        List<HOPDONGTRAGOP>? Listhopdong = new();
        int Cmonth = DateTime.Now.Month;
        int Cyear = DateTime.Now.Year;
        //int Cmenu = 0;
        public Report()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hienthi();
            mnHDvaHD_Click(sender, e);
        }
        private void hienthi()
        {
            tblThang.Text = Cmonth.ToString();
            tblNam.Text = Cyear.ToString();

            Listxemay = new();
            Listdatmua = new();
            Listhoadon = new();
            Listhopdong = new();
            Listnhapxe = new();

            var listdatmua = dc.PHIEUDATMUAs.Where(x => x.ngaydat.Month == Cmonth && x.ngaydat.Year == Cyear);
            var listnhapxe = dc.PHIEUNHAPXEs.Where(x => x.ngaynhap.Month == Cmonth && x.ngaynhap.Year == Cyear);
            var listhoadon = dc.HOADONTRALIENs.Where(x => x.ngaylap.Month == Cmonth && x.ngaylap.Year == Cyear);
            var listhopdong = dc.HOPDONGTRAGOPs.Where(x => x.ngaylap.Month == Cmonth && x.ngaylap.Year == Cyear);
            var listbienlai = dc.BIENLAIs;
            //var listxemay = dc.XEMAYs.Where(x => x.trangthai == "DB" && x.HOADONTRALIENs.Any() || x.HOPDONGTRAGOPs.Any());

            foreach (var a in listhoadon)
            {
                Listxemay.Add(a.XEMAY);
                Listhoadon.Add(a);
            }
            foreach (var a in listhopdong)
            {
                Listxemay.Add(a.XEMAY);
                Listhopdong.Add(a);
            }
            foreach(var a in listdatmua)
            {
                Listdatmua.Add(a);
            }
            foreach(var a in listnhapxe)
            {
                Listnhapxe.Add(a);
            }
            var listxemay = Listxemay;

            dgdatmuaxe.ItemsSource = listdatmua;
            dgnhapxe.ItemsSource = listnhapxe;
            dgHoadon.ItemsSource = listhoadon;
            dgHopdong.ItemsSource = listhopdong;
            dgXemay.ItemsSource = listxemay;

            double sumtien = 0;
            foreach (var a in Listxemay)
            {
                if (a.HOPDONGTRAGOPs.Any())
                {
                    foreach (var b in a.HOPDONGTRAGOPs)
                    {
                        foreach (var c in b.BIENLAIs)
                        {
                            sumtien += c.tienDong;
                        }
                    }
                }
                else
                    sumtien += a.DONGXE.giaban;
            }

            tblTDT.Text = sumtien.ToString("N0");

            sumtien = 0;
            foreach(var a in dc.NHANVIENs)
            {
                if(a.maChucvu == "01")
                {
                    sumtien += 15000000;
                }
                else
                {
                    sumtien += 10000000;
                }
            }
            foreach(var a in dc.CHITIETPHIEUNHAPs)
            {
                if(a.PHIEUNHAPXE.ngaynhap.Month == Cmonth && a.PHIEUNHAPXE.ngaynhap.Year == Cyear)
                {
                    sumtien += a.dongianhapxe;
                }
            }

            tblTCT.Text = sumtien.ToString("N0");
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnXemCTPDM_Click(object sender, RoutedEventArgs e)
        {
            Chitiet.ChitietPhieuDatMua c = new Chitiet.ChitietPhieuDatMua();
            c.ma = dgdatmuaxe.SelectedValue.ToString();
            c.ShowDialog();
        }

        private void btnXemCTPNX_Click(object sender, RoutedEventArgs e)
        {
            Chitiet.ChitietPhieuNhapXe c = new Chitiet.ChitietPhieuNhapXe();
            c.ma = dgnhapxe.SelectedValue.ToString();
            c.ShowDialog();
        }

        private void mnkhachhang_Click(object sender, RoutedEventArgs e)
        {
            //grid_khachhang.Visibility = Visibility.Visible;
            grid_datmuavanhapxe.Visibility = Visibility.Collapsed;
            grid_doanhthu.Visibility = Visibility.Collapsed;
            grid_hoadonvahopdong.Visibility = Visibility.Collapsed;
        }

        private void mnHDvaHD_Click(object sender, RoutedEventArgs e)
        {
            //Cmenu = 1;
            //grid_khachhang.Visibility = Visibility.Collapsed;
            grid_datmuavanhapxe.Visibility = Visibility.Collapsed;
            grid_doanhthu.Visibility = Visibility.Collapsed;
            grid_hoadonvahopdong.Visibility = Visibility.Visible;
        }

        private void mndoanhthu_Click(object sender, RoutedEventArgs e)
        {
            //Cmenu = 2;
            //grid_khachhang.Visibility = Visibility.Collapsed;
            grid_datmuavanhapxe.Visibility = Visibility.Collapsed;
            grid_doanhthu.Visibility = Visibility.Visible;
            grid_hoadonvahopdong.Visibility = Visibility.Collapsed;
        }

        private void mnxeDatvaNhap_Click(object sender, RoutedEventArgs e)
        {
            //Cmenu = 3;
            //grid_khachhang.Visibility = Visibility.Collapsed;
            grid_datmuavanhapxe.Visibility = Visibility.Visible;
            grid_doanhthu.Visibility = Visibility.Collapsed;
            grid_hoadonvahopdong.Visibility = Visibility.Collapsed;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //PrintDialog print = new();
            //print.PrintVisual(grid_toPrint, "In báo cáo");
            //if(Cmenu == 1)
            //{

            //}
            //else if(Cmenu == 2)
            //{

            //}
            //else if(Cmenu == 3)
            //{

            //}
            //else
            //{
            //    MessageBox.Show("Hãy chọn menu.");
            //}
            FlowDocument flowdoc = new();
            flowdoc.Name = "FD";
            if (File.Exists("printPreview.xps"))
            {
                File.Delete("printPreview.xps");
            }
            flowdoc.ColumnWidth = 999999;
            Paragraph p = new Paragraph(new Run("Báo cáo tháng "+Cmonth+" Năm "+Cyear));
            p.FontSize = 40;
            p.FontWeight = FontWeights.Bold;
            flowdoc.Blocks.Add(p);
            p = new Paragraph(new Run("Ngày lập báo cáo này: " + DateTime.Today.ToShortDateString()));
            flowdoc.Blocks.Add(p);

            // Hóa đơn trả liền
            Table table1 = new();
            flowdoc.Blocks.Add(table1);
            table1.CellSpacing = 10;
            table1.Background = Brushes.White;
            int numberOfColumns = 7;
            for (int x = 0; x < numberOfColumns; x++)
            {
                table1.Columns.Add(new TableColumn());
                table1.Columns[x].Width = new GridLength(70);
                // Set alternating background colors for the middle colums.
                //if (x % 2 == 0)
                //    table1.Columns[x].Background = Brushes.Beige;
                //else
                //    table1.Columns[x].Background = Brushes.LightSteelBlue;
            }
            table1.RowGroups.Add(new TableRowGroup());
            int rowck = 0;
            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[rowck];
            //currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 30;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Hóa đơn trả liền trong tháng"))));
            currentRow.Cells[0].ColumnSpan = 7;
            rowck++;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[rowck];
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Mã"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Nhân viên lập"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Khách hàng mua"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Xe máy bán"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Ngày lập hóa đơn"))));

            foreach (var a in Listhoadon)
            {
                rowck++;
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[rowck];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.mahdtl))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.tenhoadon))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.dongia.ToString("N0") + " VND"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.NHANVIEN.tennv))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.KHACHHANG.tenkh))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.XEMAY.tenxe))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.ngaylap.ToShortDateString()))));
            }

            // Hợp đồng trả góp
            Table table2 = new();
            flowdoc.Blocks.Add(table2);
            table2.CellSpacing = 10;
            table2.Background = Brushes.White;
            numberOfColumns = 9;
            for (int x = 0; x < numberOfColumns; x++)
            {
                table2.Columns.Add(new TableColumn());
                table2.Columns[x].Width = new GridLength(70);
                // Set alternating background colors for the middle colums.
                //if (x % 2 == 0)
                //    table1.Columns[x].Background = Brushes.Beige;
                //else
                //    table1.Columns[x].Background = Brushes.LightSteelBlue;
            }
            table2.RowGroups.Add(new TableRowGroup());
            rowck = 0;

            table2.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table2.RowGroups[0].Rows[rowck];
            //currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 30;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Hợp đồng trả góp trong tháng"))));
            currentRow.Cells[0].ColumnSpan = 9;
            rowck++;

            table2.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table2.RowGroups[0].Rows[rowck];
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Mã"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá hàng tháng"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Số tháng trả"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Trạng thái hợp đồng"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Nhân viên lập"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Khách hàng mua"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Xe máy bán"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Ngày lập hóa đơn"))));

            foreach (var a in Listhopdong)
            {
                rowck++;
                table2.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table2.RowGroups[0].Rows[rowck];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.mahdtg))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.tenhopdong))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.dongiaHT.ToString("N0") + " VND"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.soThang.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.trangthai))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.NHANVIEN.tennv))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.KHACHHANG.tenkh))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.XEMAY.tenxe))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.ngaylap.ToShortDateString()))));
            }

            // Xe đặt
            Table table3 = new();
            flowdoc.Blocks.Add(table3);
            table3.CellSpacing = 10;
            table3.Background = Brushes.White;
            numberOfColumns = 5;
            for (int x = 0; x < numberOfColumns; x++)
            {
                table3.Columns.Add(new TableColumn());
                table3.Columns[x].Width = new GridLength(70);
                // Set alternating background colors for the middle colums.
                //if (x % 2 == 0)
                //    table1.Columns[x].Background = Brushes.Beige;
                //else
                //    table1.Columns[x].Background = Brushes.LightSteelBlue;
            }
            table3.RowGroups.Add(new TableRowGroup());
            rowck = 0;
            table3.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table3.RowGroups[0].Rows[rowck];
            //currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 30;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Xe máy đặt trong tháng"))));
            currentRow.Cells[0].ColumnSpan = 5;
            rowck++;

            table3.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table3.RowGroups[0].Rows[rowck];
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Hãng xe"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên xe"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Ngày đặt"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Người đặt"))));

            foreach (var a in Listdatmua)
            {
                foreach(var b in a.CHITIETPHIEUDATs)
                {
                    rowck++;
                    table3.RowGroups[0].Rows.Add(new TableRow());
                    currentRow = table3.RowGroups[0].Rows[rowck];
                    currentRow.FontSize = 12;
                    currentRow.FontWeight = FontWeights.Normal;
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.NHASANXUAT.tennsx))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(b.DONGXE.tenDong))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(b.soluongdat.ToString("N0")))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.ngaydat.ToShortDateString()))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.NHANVIEN.tennv))));
                }
            }

            // Xe nhập
            Table table4 = new();
            flowdoc.Blocks.Add(table4);
            table4.CellSpacing = 10;
            table4.Background = Brushes.White;
            numberOfColumns = 6;
            for (int x = 0; x < numberOfColumns; x++)
            {
                table4.Columns.Add(new TableColumn());
                table4.Columns[x].Width = new GridLength(70);
                // Set alternating background colors for the middle colums.
                //if (x % 2 == 0)
                //    table1.Columns[x].Background = Brushes.Beige;
                //else
                //    table1.Columns[x].Background = Brushes.LightSteelBlue;
            }
            table4.RowGroups.Add(new TableRowGroup());
            rowck = 0;
            table4.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table4.RowGroups[0].Rows[rowck];
            //currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 30;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Xe máy nhập trong tháng"))));
            currentRow.Cells[0].ColumnSpan = 6;
            rowck++;

            table4.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table4.RowGroups[0].Rows[rowck];
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Hãng xe"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên xe"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Ngày nhập"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Người nhập"))));

            foreach (var a in Listnhapxe)
            {
                foreach (var b in a.CHITIETPHIEUNHAPs)
                {
                    rowck++;
                    table4.RowGroups[0].Rows.Add(new TableRow());
                    currentRow = table4.RowGroups[0].Rows[rowck];
                    currentRow.FontSize = 12;
                    currentRow.FontWeight = FontWeights.Normal;
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(b.DONGXE.NHASANXUAT.tennsx))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(b.DONGXE.tenDong))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(b.soluongnhap.ToString("N0")))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.ngaynhap.ToShortDateString()))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(a.NHANVIEN.tennv))));
                }
            }

            // Chi tiêu
            p = new Paragraph(new Run("Tổng chi tiêu tháng: " + tblTCT.Text + " VND"));
            flowdoc.Blocks.Add(p);

            // Doanh thu
            p = new Paragraph(new Run("Tổng doanh thu tháng: " + tblTDT.Text + " VND"));
            flowdoc.Blocks.Add(p);

            // IN
            var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)flowdoc).DocumentPaginator);
            Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
            ReportPreview reP = new(Document);
            reP.ShowDialog();
        }
        public FixedDocumentSequence? Document { get; set; }

        private void btnThangT_Click(object sender, RoutedEventArgs e)
        {
            if(Cmonth == 1)
            {
                Cmonth = 12;
            }
            else
            {
                Cmonth--;
            }
            hienthi();
        }

        private void btnThangS_Click(object sender, RoutedEventArgs e)
        {
            if (Cmonth == 12)
            {
                Cmonth = 1;
            }
            else
            {
                Cmonth++;
            }
            hienthi();
        }

        private void btnNamT_Click(object sender, RoutedEventArgs e)
        {
            Cyear--;
            hienthi();
        }

        private void btnNamS_Click(object sender, RoutedEventArgs e)
        {
            Cyear++;
            hienthi();
        }
    }
}
