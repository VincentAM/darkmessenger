﻿using System;
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
    #region Objet delegate

    delegate void WriteMessageDelegateHandler(string text,Color c_color);
    delegate void ConnexionDelegateHandler(main _main);
    delegate void DeconnexionDelegateHandler(main _main);
    delegate void LoadListClientDelegateHandler(ListBox _lb);

    #endregion

    public partial class main : Form
    {

        #region Variables

        String s_ip;
        int i_port;
        String pseudo;

        #endregion

        #region Variables réseau

        TcpClient tcp_client;
        UTF8Encoding utf8 = new UTF8Encoding();

        #endregion

        #region Thread Delegate

        Thread t_waitMessage;
        Thread t_waitConnexion;

        private WriteMessageDelegateHandler WriteMessageDelegate;
        private ConnexionDelegateHandler ConnexionDelegate;
        private DeconnexionDelegateHandler DeconnexionDelegate;
        private LoadListClientDelegateHandler LoadListClientDelegate;

        #endregion

        public main()
        {
            InitializeComponent();
            //instanciation des delegués
            this.WriteMessageDelegate = new WriteMessageDelegateHandler(AddMessage);
            this.ConnexionDelegate = new ConnexionDelegateHandler(EnabledConnexion);
            this.DeconnexionDelegate = new DeconnexionDelegateHandler(EnabledDeconnexion);
            this.LoadListClientDelegate = new LoadListClientDelegateHandler(LoadListClient);
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
                    WaitConnexion();
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
            {
                SendMessage(tb_message.Text);
                tb_message.Text = "";
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendMessage(TrameClient.getDisconnectionTrame(pseudo));
            CloseTcpClient();
        }

        #endregion

        #region Lanceur de thread

        public void WaitMessage()
        {
            t_waitMessage = new Thread(new ThreadStart(runWaitMessage));
            t_waitMessage.Start();
        }

        public void WaitConnexion()
        {
            AddMessage("Connexion en cours...", Color.Red);
            t_waitConnexion = new Thread(new ThreadStart(runWaitConnexion));
            t_waitConnexion.Start();
        }

        #endregion

        #region Méthodes en Thread

        public void runWaitConnexion()
        {
            try
            {
                tcp_client = new TcpClient(s_ip, i_port);
                this.Invoke(ConnexionDelegate, this);
            }
            catch (Exception ex)
            {
                this.Invoke(WriteMessageDelegate, "Echec de connexion", Color.Red);
            }
        }

        public void runWaitMessage()
        {
            try
            {
                Stream stm = tcp_client.GetStream();
                byte[] ba = new byte[1024];
                while (stm.Read(ba, 0, 1024) != 0)
                {
                    //analyse de trame

                    this.Invoke(WriteMessageDelegate, "L'autre : " + utf8.GetString(ba), Color.Violet);

                    //modification de la liste de client
                    ListBox new_lb = new ListBox();
                    new_lb.Items.Add("L'autre");
                    this.Invoke(LoadListClientDelegate, new_lb);
                }
            }
            catch (IOException ex)
            {
                try
                {
                    CloseTcpClient();
                    this.Invoke(DeconnexionDelegate, this);
                }
                catch (InvalidOperationException exx){
                    CloseTcpClient();
                }
            }

        }

        #endregion

        #region Méthodes invokées dans les delegués

        public void AddMessage(string s_mess, Color c_color)
        {
            rtb_allmessage.SelectionColor = c_color;
            rtb_allmessage.AppendText(System.DateTime.Now.ToLongTimeString() + " : " + s_mess);
            rtb_allmessage.AppendText("\n");
        }

        public void EnabledConnexion(main _main)
        {
            _main.bt_connexion.Enabled = false;
            _main.tb_adressip.Enabled = false;
            _main.tb_pseudo.Enabled = false;
            _main.tb_message.Enabled = true;
            _main.AddMessage("Connexion avec succes", Color.Green);
            _main.SendMessage(TrameClient.getConnectionTrame(pseudo));
            _main.WaitMessage();
        }

        public void EnabledDeconnexion(main _main)
        {
            _main.bt_connexion.Enabled = true;
            _main.tb_adressip.Enabled = true;
            _main.tb_pseudo.Enabled = true;
            _main.tb_message.Enabled = false;
            _main.AddMessage("Server arrêté", Color.Red);
        }

        public void LoadListClient(ListBox _lb)
        {
            lb_client = _lb;
        }

        #endregion

        #region Envoi de message

        public void SendMessage(String _mess)
        {
            if (_mess != "" && tcp_client != null)
            {
                Stream stm = tcp_client.GetStream();
                byte[] ba = utf8.GetBytes(_mess);
                stm.Write(ba, 0, ba.Length);
                AddMessage(pseudo + " : " + _mess, Color.Blue);
            }
        }

        #endregion

        public void CloseTcpClient()
        {
            if(tcp_client!=null)
            {
                tcp_client.Close();
                tcp_client=null;
            }
        }
    }
}
