using System.Windows.Forms;

namespace OmitObservation
{
    internal partial class SettingsForm_OmitObservation : Form
    {


        #region Get and Set Options

        public int FilterMin;

       #endregion



        public SettingsForm_OmitObservation(int FilterMinInput)
        {
            InitializeComponent();


            FilterMinUpDown.Value = FilterMinInput;


        }



       

        private void OKButton_Click(object sender, System.EventArgs e)
        {
            this.FilterMin = (int)FilterMinUpDown.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
