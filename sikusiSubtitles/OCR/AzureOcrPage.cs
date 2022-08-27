using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.OCR {
    public partial class AzureOcrPage : SettingPage {
        public Ocr Ocr { get { return ocr; } }

        public AzureOcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            InitializeComponent();
        }

        readonly AzureOcr ocr = new AzureOcr();
    }
}
