using Library;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {

           await Install.Load(@"http://192.168.1.210/api/file/jvnvb2gr.wmh");
        }
    }
}
