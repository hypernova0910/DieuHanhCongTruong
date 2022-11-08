using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.CustomControl
{
    public partial class MachineBombLabel : Label
    {
        public Color _ColorRun = new Color();
        public string _Code = string.Empty;

        private string _machineId = "";
        private string _staff_name = "";
        private string _GPS = "";

        public string staff_name
        {
            get { return _staff_name; }
            set
            {
                _staff_name = value;
                this.Text = _machineId + (_staff_name.Trim() != "" ? " - " + _staff_name : "") + (_GPS.Trim() != "" ? " - " + _GPS : "");
            }
        }

        public string machineId
        {
            get { return _machineId; }
            set
            {
                _machineId = value;
                this.Text = _machineId + (_staff_name.Trim() != "" ? " - " + _staff_name : "") + (_GPS.Trim() != "" ? " - " + _GPS : "");
            }
        }

        public string GPS 
        { 
            get { return GPS; } 
            set
            {
                _GPS = value;
                this.Text = _machineId + (_staff_name.Trim() != "" ? " - " + _staff_name : "") + (_GPS.Trim() != "" ? " - " + _GPS : "");
            }
        }

        public MachineBombLabel()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}