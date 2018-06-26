using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyActiveX
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true), Guid("D857B4F5-8684-453e-82C8-7F493CBE5592")]
    public partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hola Mundo");
        }
    }
}

