using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace client
{
    internal delegate void ChangeTextDelegateHandler(RichTextBox _rtb, string text);

    public partial class Form1 : Form
    {

        private ChangeTextDelegateHandler ChangeTextDelegate;

        #region objects

        Socket s_listen;
        IPAddress my_ip;
        int my_port;
        byte[] msg;

        Socket s_send;
        IPAddress dest_ip;
        int dest_port;

        Socket currentClient;
        Thread waitClientThread;
        ArrayList listOfConnected;
        bool waitForNewClient;

        #endregion

        public Form1()
        {
            InitializeComponent();
            this.ChangeTextDelegate = new ChangeTextDelegateHandler(ChangeText);
        }

        private void init_listen()
        {
            my_port = int.Parse(tb_listen_port.Text);
            IPHostEntry ipHostEntry = Dns.Resolve(Dns.GetHostName());
            my_ip = ipHostEntry.AddressList[0];//Mon Ip
            listOfConnected = new ArrayList();
            waitForNewClient = true;
        }

        private void init_send()
        {
            dest_port = int.Parse(tb_send_port.Text);
            dest_ip = IPAddress.Parse(tb_send_ip.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init_listen();

            s_listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s_listen.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);

            s_listen.Bind(new IPEndPoint(my_ip, my_port));
            s_listen.Listen(10);

            currentClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            waitClientThread = new Thread(new ThreadStart(runNewClientThread));
            waitClientThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (s_listen != null)
            {
                closeConnected();
                waitForNewClient = false;

                if (s_listen.IsBound == true)
                {
                    try
                    {
                        s_listen.Close();
                    }
                    catch {}
                }

                currentClient.Close();
                waitClientThread.Abort();

                s_listen = null;
                currentClient = null;
                cwrite("Listener coupé");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            init_send();

            s_send = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                s_send.Connect(dest_ip, dest_port);
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                {
                    cwrite("Impossible de joindre : " + dest_ip + ":" + dest_port);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (s_send != null)
            {
                s_send.Close();
                s_send = null;
            }
        }

        private void runNewClientThread()
        {
            try
            {
                while (waitForNewClient)
                {
                    cwrite("Attente d'une nouvelle connexion...");
                    this.Invoke(ChangeTextDelegate, richTextBox1, "test message"); 
                    currentClient = s_listen.Accept();
                    listOfConnected.Add(currentClient);
                    cwrite("Nouveau client !");
                }
            }
            catch(Exception ex)
            {
                cwrite("Boucle d'attente coupée");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (s_listen != null)
            {
                s_listen.Close();
            }

            closeConnected();

            e.Cancel = false;
        }

        private void closeConnected()
        {
            if (listOfConnected != null && listOfConnected.Count != 0)
            {
                foreach (Socket s in listOfConnected)
                {
                    s.Close();
                }
            } 
        }

        private void cwrite(string _s)
        {
            Console.WriteLine(_s);
        }

        private void ChangeText(RichTextBox _rtb, string text)
        {
            _rtb.Text += text + '\n' ;
        }
    }
}
