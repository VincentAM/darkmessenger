using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace darkmessenger
{
    delegate void WriteMessageDelegateHandler(string text,Color c_color);

    public partial class main : Form
    {

        #region Objets

        String s_ip;
        int i_port;
        String pseudo;
        TcpClient tcp_client;
        Thread waitMessage;
        UTF8Encoding utf8 = new UTF8Encoding();
        private WriteMessageDelegateHandler WriteMessageDelegate;

        #endregion


        public main()
        {
            InitializeComponent();
            this.WriteMessageDelegate = new WriteMessageDelegateHandler(AddMessage);
        }

        #region Event Méthodes

        private void bt_connexion_Click(object sender, EventArgs e)
        {
            try
            {
                String[] adress_parse = tb_adressip.Text.Split((":").ToCharArray());
                s_ip = adress_parse[0];
                i_port = int.Parse(adress_parse[1]);
                pseudo = tb_pseudo.Text;
                if( pseudo != "")
                {
                    StartConnexion();
                }
                else
                    MessageBox.Show("Entrez un pseudo !");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Adresse de connexion incorrecte !");
            }
        }

        private void tb_message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendMessage();
        }

        #endregion



        public void StartConnexion()
        {
            AddMessage("Connexion en cours...",Color.Red);
            try
            {
                tcp_client = new TcpClient(s_ip, i_port);
                tb_message.Enabled = true;
                AddMessage("Connexion avec succes",Color.Green);
                waitMessage = new Thread(new ThreadStart(runWaitMessage));
                waitMessage.Start();
            }
            catch (Exception ex)
            {
                AddMessage("Echec de connexion",Color.Red);
            }
        }

        public void SendMessage()
        {
            if (tb_message.Text != "" && tcp_client!=null)
            {
                Stream stm = tcp_client.GetStream();
                byte[] ba = utf8.GetBytes(tb_message.Text);
                stm.Write(ba, 0, ba.Length);
                AddMessage("Moi : " + tb_message.Text, Color.Blue);
                tb_message.Text = "";
            }
        }

        public void runWaitMessage()
        {
            try
            {
                Stream stm = tcp_client.GetStream();
                byte[] ba = new byte[100];
                while (stm.Read(ba, 0, 100) != 0)
                {
                    this.Invoke(WriteMessageDelegate, "L'autre : " + utf8.GetString(ba),Color.YellowGreen);
                }
            }
            catch (IOException ex)
            {
                this.Invoke(WriteMessageDelegate, "Server arrêté" ,Color.Red);
            }

        }

        public void AddMessage(string s_mess,Color c_color)
        {
            rtb_allmessage.SelectionColor = c_color;
            rtb_allmessage.AppendText(System.DateTime.Now.ToLongTimeString() + " : " + s_mess);
            rtb_allmessage.AppendText("\n");
        }

       


        
    }
}
