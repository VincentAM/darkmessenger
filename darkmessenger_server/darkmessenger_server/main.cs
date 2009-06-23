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
    delegate void RefreshListOfClientHandler();

    #endregion

    public partial class main : Form
    {
        #region objects

        #region thread objects

        Thread waitForNewConnection;
        Thread waitMessageFromClient;
        Thread sendMessage;

        #endregion

        #region delegate objects

        private WriteConsoleDelegateHandler WriteConsoleDelegate;
        private ChangeStateServerHandler ChangeStateServer;
        private RefreshListOfClientHandler RefreshListOfClient;

        #endregion

        TcpListener serverListener;
        ArrayList listOfClient;
        ArrayList toSend;
        UTF8Encoding utf8 = new UTF8Encoding();

        #endregion

        public main()
        {
            InitializeComponent();
            this.WriteConsoleDelegate = new WriteConsoleDelegateHandler(write_on_console);
            this.ChangeStateServer = new ChangeStateServerHandler(change_state_server);
            this.RefreshListOfClient = new RefreshListOfClientHandler(refresh_list_of_client);
            toSend = new ArrayList();
        }

        #region other methods

        private void send_list_of_client()
        {
            ArrayList listOfNames = new ArrayList();
            foreach (Client c in listOfClient)
            {
                listOfNames.Add(c.Name);
            }

            foreach (Client c in listOfClient)
            {
                send_msg(c, TrameServer.getListOfClientTrame(listOfNames));
            }          
        }

        private void send_msg(Client _c, string _s)
        {
            toSend.Add(_c);
            toSend.Add(_s);
            sendMessage = new Thread(new ThreadStart(runSendMessage));
            sendMessage.Start();
        }

        private void send_msg_to_all(string _from, string _msg)
        {
            foreach (Client c in listOfClient)
            {
                if (c.Name != _from)
                {
                    send_msg(c, TrameClient.getMsgTrame(_from, _msg, c.Name));
                    this.Invoke(WriteConsoleDelegate, "Redirection du message de [" + _from + "] à "+c.Name);
                }
            }
        }

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
            this.Invoke(RefreshListOfClient);
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

        private int getIndexClientInList(string _name)
        {
            int i = -1;
            for (int j=0;j<listOfClient.Count;j++)
            {
                if (((Client)listOfClient[j]).Name == _name)
                {
                    i = j;
                }
            }
            return i;
        }

        private bool isClientNameExist(string _name)
        {
            bool exist = false;

            foreach (Client c in listOfClient)
            {
                if (c.Name.ToLower() == _name.ToLower())
                {
                    exist = true;
                    break;
                }
            }

            return exist;
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

                this.Invoke(WriteConsoleDelegate, "Lancement de la boucle des connexions.");

                while (true)
                {
                    try
                    {
                        bool isconnecting = true;
                        while (isconnecting)
                        {
                            //Connexion avec l'utilisateur - Le code est bloqué sur cette ligne tant qu'il n'y a pas de demande de connexion
                            Socket s = serverListener.AcceptSocket();
                            this.Invoke(WriteConsoleDelegate, "Tentative de connexion.");

                            // Reception du premier message, qui doit être une demande de connexion
                            byte[] b;
                            int k;
                            b = new byte[1024];
                            k = s.Receive(b);
                            string temp = utf8.GetString(b);
                            Trame t = new Trame(temp);
                            if (t.isValidTrame)//Si la trame est valide
                            {
                                if (t.type == TrameType.Connection)//Si c'est une demande de connexion
                                {
                                    Client c = new Client(t.from, s);

                                    if (!isClientNameExist(t.from))
                                    {

                                        this.Invoke(WriteConsoleDelegate, "Connexion accpectée pour : " + t.from);

                                        
                                        listOfClient.Add(c);

                                        this.Invoke(RefreshListOfClient);
                                        send_list_of_client();
                                        waitMessageFromClient = new Thread(new ThreadStart(runWaitForMessage));
                                        waitMessageFromClient.Start();
                                        isconnecting = false;
                                    }
                                    else
                                    {
                                        this.Invoke(WriteConsoleDelegate, "Connexion refusée pour [" + t.from + "] : nom déjà connecté");
                                        send_msg(c, TrameServer.getWrongNameTrame());
                                    }
                                }
                                else
                                {
                                    s.Close();
                                    //serverListener.Stop();
                                }
                            }
                            else
                            {
                                s.Close();
                                serverListener.Stop();
                            }
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
                    b = new byte[1024];
                    try
                    {
                        k = myClient.Receive(b);
                    }
                    catch (ObjectDisposedException ex3)
                    { }

                    Trame t = new Trame(utf8.GetString(b));
                    if (t.isValidTrame)//Si la trame est valide
                    {
                        if (t.type == TrameType.Disconnection)//Si c'est une demande de connexion
                        {
                            this.Invoke(WriteConsoleDelegate, "Déconnexion demandée par : " + t.from);

                            disconnect_user(t.from);
                            this.Invoke(RefreshListOfClient);
                            this.Invoke(WriteConsoleDelegate, "Déconnexion de " + t.from + " ok.");
                            send_list_of_client();
                        }
                        else if (t.type == TrameType.Message)
                        {
                            this.Invoke(WriteConsoleDelegate, "Message reçu ["+t.from+"] pour ["+t.to+"] : " + t.msg);
                            if (t.to != "server")
                            {
                                int index = getIndexClientInList(t.to);
                                if (index != -1)
                                {
                                    send_msg(((Client)listOfClient[index]), t.data);
                                    this.Invoke(WriteConsoleDelegate, "Redirection du message à [" + t.to + "]");
                                }
                                else
                                {
                                    this.Invoke(WriteConsoleDelegate, "Client [" + t.to + "] inconnu");
                                }
                            }
                            else
                            {
                                this.Invoke(WriteConsoleDelegate, "Message pour le server de ["+t.from+"] : "+t.msg);
                            }
                        }
                        else if (t.type == TrameType.MessageToAll)
                        {
                            this.Invoke(WriteConsoleDelegate, "Message reçu [" + t.from + "] pour tout le monde : " + t.msg);
                            send_msg_to_all(t.from, t.msg);
                        }
                        else
                        {
                            this.Invoke(WriteConsoleDelegate, "string reçue : " + utf8.GetString(b));
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (SocketException ex)
                {
                    try
                    {
                        myClient.Close();
                        this.Invoke(WriteConsoleDelegate, "Utilisateur déconnecté.");
                        this.Invoke(RefreshListOfClient);
                    }
                    catch (ObjectDisposedException ex2)
                    {
                        break;
                    }
                    
                    break;
                }
            }
        }

        private void runSendMessage()
        {
            Client c = ((Client)toSend[0]);
            string s = toSend[1].ToString();
            toSend.RemoveAt(0);
            toSend.RemoveAt(0);

            byte[] b;
            b = utf8.GetBytes(s);
            Console.WriteLine("Transmission ...");
            c.Socket.BeginSend(b, 0, b.Length, SocketFlags.None, null, null);
            Console.WriteLine("Transmission terminée.");
        }

        #endregion

        #region delegate methods

        private void write_on_console(string _s)
        {
            DateTime.Today.ToString();
            rtb_console.AppendText("[" + DateTime.Now.ToLongTimeString()+ "] " + _s);
            rtb_console.AppendText("\n");
            rtb_console.Select(rtb_console.Text.Length, 0);
            rtb_console.ScrollToCaret();
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

        private void refresh_list_of_client()
        {
            lb_clients.Items.Clear();

            foreach (Client c in listOfClient)
            {
                lb_clients.Items.Add(c.Name);
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

        #endregion
    }
}
