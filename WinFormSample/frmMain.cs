using System;
using System.Windows.Forms;

namespace WinFormSample
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            var datePicker = (WpfPersianDatePicker.Views.PDatePicker)elementHost1.Child;            
            lblDate.Text = datePicker.SelectedPersianDate;
        }
    }
}
