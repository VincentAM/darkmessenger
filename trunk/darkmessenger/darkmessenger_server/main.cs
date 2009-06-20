using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace darkmessenger_server
{
    public partial class main : Form
    {
        Thread waitForNewConnection;

        public main()
        {
            InitializeComponent();
        }

        private void bt_start_listen_Click(object sender, EventArgs e)
        {
            waitForNewConnection = new Thread(new ThreadStart(runForNewConnection));
        }
    }
}
