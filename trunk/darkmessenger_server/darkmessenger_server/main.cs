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

namespace darkmessenger
{
    #region delegate declaration

    delegate void WriteConsoleDelegateHandler(string text);
    delegate void ChangeStateServerHandler(bool isWaiting);
    delegate void RefreshListOfConnectedHandler();

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
        private RefreshListOfConnectedHandler RefreshListOfConnected;

        #endregion

        TcpListener serverListener;
        ArrayList listOfClient;
        UTF8Encoding utf8 = new UTF8Encoding();

        #endregion

        public main()
        {
            InitializeComponent();
            this.WriteConsoleDelegate = new WriteConsoleDelegateHandler(write_on_console);
            this.ChangeStateServer = new ChangeStateServerHandler(change_state_server);
            this.RefreshListOfConnected = new RefreshListOfConnectedHandler(refresh_list_of_connected);
        }

        #region other methods

        private void disconnect_user(string _name)
        {
            int i = 0;
            foreach (Client c in listOfClient)
            {
                if (c.Name == _name)
                {
                    listOfClient.RemoveAt(i);
                    try
                    {
                        c.Socket.Close();
                    }
                    catch (SocketException ex)
                    {

                    }
                    break;
                }
                i++;
            }
        }

        private void disconnect_all_user()
        {
            foreach (Client c in listOfClient)
            {
                try
                {
                    c.Socket.Close();
                }
                catch (SocketException ex)
                {

                }
            }

            listOfClient.Clear();
        }

        private void stop_listener()
        {
            if (waitForNewConnection != null)
            {
                disconnect_all_user();

                try
                {
                    serverListener.Stop();
                }
                catch (InvalidCastException ex)
                { }
                catch (SocketException ex)
                { }
                bt_stop_listen.Enabled = false;
                bt_start_listen.Enabled = true;
            }
        }

        #endregion

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

                        byte[] b;
                        int k;
                        b = new byte[100];
                        k = s.Receive(b);
                        Trame t = new Trame(utf8.GetString(b));
                        if (t.isValidTrame)
                        {
                            if (t.type == "connection")
                            {
                                this.Invoke(WriteConsoleDelegate, "Connexion accpectée pour : " + t.from);

                                Client c = new Client(t.from, s);
                                listOfClient.Add(c);

                                waitMessageFromClient = new Thread(new ThreadStart(runWaitForMessage));
                                waitMessageFromClient.Start();
                            }
                        }
                        else
                        { 
                            
                        }
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
            }
        }

        private void runWaitForMessage()
        {
            byte[] b;
            int k;
            string str = "";
            Socket myClient = null;

            try
            {
                myClient = ((Client)listOfClient[listOfClient.Count - 1]).Socket;

            }
            catch (SocketException ex)
            {

            }

            while (true)
            {
                try
                {
                    if (str == "q")
                    {
                        break;
                    }
                    else { str = ""; }

                    b = new byte[100];
                    k = myClient.Receive(b);

                    this.Invoke(WriteConsoleDelegate, "Message reçu : " + utf8.GetString(b));
                }

                catch (SocketException ex)
                {
                    try
                    {
                        myClient.Close();
                        this.Invoke(WriteConsoleDelegate, "Utilisateur déconnecté.");
                    }
                    catch (ObjectDisposedException ex2)
                    {
                        break;
                    }
                    
                    break;
                }
            }
        }

        #endregion

        #region delegate methods

        private void write_on_console(string _s)
        {
            rtb_console.AppendText(_s);
            rtb_console.AppendText("\n");
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

        private void refresh_list_of_connected()
        {
            foreach (Client c in listOfClient)
            { 
            
            }
        }


        #endregion

        #region event interface

        private void bt_stop_listen_Click(object sender, EventArgs e)
        {
            stop_listener();
        }

        private void bt_start_listen_Click(object sender, EventArgs e)
        {
            listOfClient = new ArrayList();
            waitForNewConnection = new Thread(new ThreadStart(runWaitForNewConnection));
            waitForNewConnection.Start();

            bt_stop_listen.Enabled = true;
            bt_start_listen.Enabled = false;
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_listener();
        }

        private void bt_msg_test_Click(object sender, EventArgs e)
        {
            byte[] b;
            foreach (Client c in listOfClient)
            {
                b = utf8.GetBytes("test");
                Console.WriteLine("Transmission ...");
                c.Socket.BeginSend(b, 0, b.Length, SocketFlags.None, null, null);
                Console.WriteLine("Transmission terminée.");
            }
        }

        #endregion
    }
}
