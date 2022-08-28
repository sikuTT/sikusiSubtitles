﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.OCR {
    public partial class TesseractOcrPage : SettingPage {
        TesseractOcrService service;

        public TesseractOcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new TesseractOcrService(serviceManager);

            InitializeComponent();
        }
    }
}