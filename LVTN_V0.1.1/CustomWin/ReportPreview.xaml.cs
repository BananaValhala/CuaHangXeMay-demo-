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

namespace LVTN_V0._1._1.CustomWin
{
    /// <summary>
    /// Interaction logic for ReportPreview.xaml
    /// </summary>
    public partial class ReportPreview : Window
    {
        private FixedDocumentSequence _document;
        public ReportPreview(FixedDocumentSequence document)
        {
            InitializeComponent();
            _document = document;
            PreviewD.Document = document;
        }
    }
}
