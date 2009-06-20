using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace darkmessenger_server
{
    delegate void WriteConsoleDelegateHandler(string text);
    delegate void ChangeStateServerHandler(bool isWaiting);

    public partial class main : Form
    {
        Thread waitForNewConnection;
        Thread waitMessageFromClient;

        private WriteConsoleDelegateHandler WriteConsoleDelegate;
        private ChangeStateServerHandler ChangeStateServer;

        public main()
        {
            InitializeComponent();
            this.WriteConsoleDelegate = new WriteConsoleDelegateHandler(write_on_console);
            this.ChangeStateServer = new ChangeStateServerHandler(change_state_server);
        }

        private void bt_start_listen_Click(object sender, EventArgs e)
        {
            waitForNewConnection = new Thread(new ThreadStart(runWaitForNewConnection));
            waitForNewConnection.Start();
        }

        private void runWaitForNewConnection()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("192.168.0.2");//IPAddress.Parse("192.168.0.2");
                int port = 12609;
                TcpListener serverListener = new TcpListener(ipAd, port);
                //this.Invoke(WriteConsoleDelegate, "mon ip : " + ipAd.Address.ToString());

                /* Start Listeneting at the specified port */

                serverListener.Start();

                this.Invoke(WriteConsoleDelegate, "Le server attend les connexions sur le port : " + port.ToString());
                this.Invoke(ChangeStateServer, true);

                while (true)
                {
                    this.Invoke(WriteConsoleDelegate, "Attente ...");
                    Socket s = serverListener.AcceptSocket();
                    this.Invoke(WriteConsoleDelegate, "Connexion accpectée pour : " + s.RemoteEndPoint.ToString());
                }

                //byte[] b;
                //int k;
                //string str = "";

                //while (true)
                //{
                //    if (str == "q")
                //    {
                //        break;
                //    }
                //    else { str = ""; }

                //    b = new byte[100];
                //    k = s.Receive(b);
                //    Console.WriteLine("Reception ...");
                //    for (int i = 0; i < k; i++)
                //        str += Convert.ToChar(b[i]);

                //    Console.WriteLine(str);
                //}

                //s.Close();
                serverListener.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur ..." + e.StackTrace);
                Console.Read();
            }
        }

        private void write_on_console(string _s)
        {
            rtb_console.Text += _s + "\n";
        }

        private void change_state_server(bool _b)
        {
            if (_b == true)
            {
                p_etat_server.BackColor = Color.Green;
            }
            else
            {
                p_etat_server.BackColor = Color.Red;
            }
        }
    }
}
