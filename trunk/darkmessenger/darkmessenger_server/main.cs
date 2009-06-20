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
    #region delegate declaration

    delegate void WriteConsoleDelegateHandler(string text);
    delegate void ChangeStateServerHandler(bool isWaiting);

    #endregion

    public partial class main : Form
    {
        #region objects

        #region thread objects

        Thread waitForNewConnection;
        Thread waitMessageFromClient;

        #endregion

        #region delegate objects

        private WriteConsoleDelegateHandler WriteConsoleDelegate;
        private ChangeStateServerHandler ChangeStateServer;

        #endregion

        TcpListener serverListener;
        ArrayList listOfClient;

        #endregion

        public main()
        {
            InitializeComponent();
            this.WriteConsoleDelegate = new WriteConsoleDelegateHandler(write_on_console);
            this.ChangeStateServer = new ChangeStateServerHandler(change_state_server);
        }

        #region thread methods

        private void runWaitForNewConnection()
        {
            try
            {
                IPAddress ipAd = Dns.Resolve(Dns.GetHostName()).AddressList[0];
                int port = int.Parse(tb_port.Text);

                serverListener = new TcpListener(ipAd, port);
                serverListener.Start();

                this.Invoke(WriteConsoleDelegate, "Le server attend les connexions sur le port : " + port.ToString());
                this.Invoke(ChangeStateServer, true);

                while (true)
                {
                    this.Invoke(WriteConsoleDelegate, "Attente ...");

                    try
                    {
                        Socket s = serverListener.AcceptSocket();
                        listOfClient.Add(s);
                        this.Invoke(WriteConsoleDelegate, "Connexion accpectée pour : " + s.RemoteEndPoint.ToString());
                    }
                    catch (SocketException ex)
                    {
                        //Console.WriteLine(ex.Message);
                        foreach (Socket so in listOfClient)
                        {
                            so.Close();
                        }
                        break;
                    }
                }

                this.Invoke(WriteConsoleDelegate, "Le server est coupé.");
                this.Invoke(ChangeStateServer, false);
                serverListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur ..." + e.StackTrace);
                Console.Read();
            }
        }

        #endregion

        #region delegate methods

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

        #endregion

        #region event interface

        private void bt_stop_listen_Click(object sender, EventArgs e)
        {
            serverListener.Stop();
        }

        private void bt_start_listen_Click(object sender, EventArgs e)
        {
            listOfClient = new ArrayList();

            waitForNewConnection = new Thread(new ThreadStart(runWaitForNewConnection));
            waitForNewConnection.Start();
        }

        #endregion
    }
}
