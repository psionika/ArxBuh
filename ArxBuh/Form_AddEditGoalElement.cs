using System.Windows.Forms;

namespace ArxBuh
{
    public partial class Form_AddEditGoalElement : Form
    {
        public Form_AddEditGoalElement(string title)
        {
            InitializeComponent();
            Text = $"ArxBuh: {title}";
        }

        private void txb_GoalElementAllSum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == ','
                && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
