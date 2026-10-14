namespace WinForms.Forms
{
    public partial class ConfirmForm : Form
    {
        public ConfirmForm(string message, string title)
        {
            InitializeComponent();

            lblMessage.Text = message;
            Text = title;
        }
    }
}
