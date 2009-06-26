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
    #region Objet delegate

    delegate void WriteMessageDelegateHandler(string text,Color c_color);
    delegate void ConnexionDelegateHandler(main _main);
    delegate void DeconnexionDelegateHandler(main _main);
    delegate void LoadListClientDelegateHandler(ArrayList _listclient);

    delegate void ProgressBarDelegateHandler(int _value);

    #endregion

    public partial class main : Form
    {

        #region Variables

        String s_ip;
        int i_port;
        String pseudo;
        DialogResult dialog_res;

        #endregion

        #region Variables réseau

        TcpClient tcp_client;
        UTF8Encoding utf8 = new UTF8Encoding();

        #endregion

        #region Variables fichier

        TcpClient tcp_client_fichier;
        string s_ip_client_fichier;
        TcpListener tcp_listener_fichier;
        string s_ip_server_fichier;
        string s_port_server_fichier="9998";
        EnvoiFichier ef_fichier;
        

        #endregion

        #region Thread Delegate

        Thread t_waitMessage;
        Thread t_waitConnexion;

        private WriteMessageDelegateHandler WriteMessageDelegate;
        private ConnexionDelegateHandler ConnexionDelegate;
        private DeconnexionDelegateHandler DeconnexionDelegate;
        private LoadListClientDelegateHandler LoadListClientDelegate;

        #endregion

        #region Thread Fichier

        Thread t_reception_fichier;
        Thread t_envoi_fichier;

        private ProgressBarDelegateHandler ProgressBarDelegate;

        #endregion

        public main()
        {
            InitializeComponent();
            //instanciation des delegués
            this.WriteMessageDelegate = new WriteMessageDelegateHandler(AddMessage);
            this.ConnexionDelegate = new ConnexionDelegateHandler(EnabledConnexion);
            this.DeconnexionDelegate = new DeconnexionDelegateHandler(EnabledDeconnexion);
            this.LoadListClientDelegate = new LoadListClientDelegateHandler(LoadListClient);
            this.ProgressBarDelegate = new ProgressBarDelegateHandler(UpdateProgressBar);
        }

        #region Event Méthodes

        private void bt_connexion_Click(object sender, EventArgs e)
        {
            Connexion();
        }

        private void tb_adressip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Connexion();
        }

        private void tb_pseudo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Connexion();
        }

        private void tb_message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (lb_client.SelectedIndex != -1 && lb_client.SelectedItem.ToString() != pseudo && lb_client.SelectedIndex!=0) //une personne
                {
                    SendMessage(TrameClient.getMsgTrame(pseudo, tb_message.Text, lb_client.SelectedItem.ToString()));
                    AddMessage(TrameClient.getMsgTrame(pseudo, tb_message.Text, lb_client.SelectedItem.ToString()), Color.Blue);
                    tb_message.Text = "";
                }
                else if (lb_client.SelectedIndex == 0) //to all
                {
                    SendMessage(TrameClient.getMsgToAllTrame(pseudo, tb_message.Text));
                    AddMessage(TrameClient.getMsgToAllTrame(pseudo, tb_message.Text), Color.Violet);
                    tb_message.Text = "";
                }
            }
        }

        private void tb_fichier_MouseClick(object sender, MouseEventArgs e)
        {
            dialog_res = openFileDialog.ShowDialog();
            if (dialog_res != DialogResult.Cancel && dialog_res != DialogResult.Abort)
            {
                tb_fichier.Text = openFileDialog.FileName;
            }
        }

        private void bt_send_fichier_Click(object sender, EventArgs e)
        {
            if (tb_fichier.Text != "" && lb_client.SelectedIndex != -1 && lb_client.SelectedItem.ToString() != pseudo && lb_client.SelectedIndex != 0)
            {
                SendMessage(TrameClient.getAskForFileTrame(pseudo, lb_client.SelectedItem.ToString(),
                    ((IPAddress)Dns.Resolve(Dns.GetHostName()).AddressList[0]).ToString()));
                ef_fichier = new EnvoiFichier(tb_fichier.Text, pseudo, lb_client.SelectedItem.ToString());
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SendMessage(TrameClient.getDisconnectionTrame(pseudo));
                CloseTcpClient();
                t_waitMessage.Abort();
                t_waitConnexion.Abort();
                t_envoi_fichier.Abort();
                t_reception_fichier.Abort();
            }
            catch (Exception ex) { }
        }

        private void Connexion()
        {
            try
            {
                String[] adress_parse = tb_adressip.Text.Split((":").ToCharArray());
                s_ip = adress_parse[0];
                i_port = int.Parse(adress_parse[1]);
                pseudo = tb_pseudo.Text;
                if (pseudo != "")
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

        #region Lanceur de Thread fichier

        public void StartReception()
        {
            t_reception_fichier = new Thread(new ThreadStart(runReceptionFichier));
            t_reception_fichier.Start();
        }

        public void StartEnvoi()
        {
            t_envoi_fichier = new Thread(new ThreadStart(runEnvoiFichier));
            t_envoi_fichier.Start();
        }

        #endregion

        #region Méthodes en Thread

        public void runWaitConnexion()
        {
            try
            {
                tcp_client = new TcpClient(s_ip, i_port);
                SendMessage(TrameClient.getConnectionTrame(pseudo));
                WaitMessage();
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
                Boolean first_co = true;
                while (stm.Read(ba, 0, 1024) != 0)
                {
                    //analyse de trame
                    string _s_trame = utf8.GetString(ba);
                    Trame _trame = new Trame(_s_trame);
                    if (_trame.isValidTrame)
                    {
                        if(_trame.type == TrameType.ListOfClient)
                        {
                            this.Invoke(LoadListClientDelegate, _trame.listClients);
                            if (first_co)
                            {
                                this.Invoke(ConnexionDelegate, this);
                                this.Invoke(WriteMessageDelegate, "Connexion avec succes", Color.Green);
                                first_co = false;
                            }
                        }
                        else if (_trame.type == TrameType.AskForFile)
                        {
                            this.Invoke(WriteMessageDelegate, _trame.from+" veut vous envoyer un fichier", Color.Green);
                            dialog_res = MessageBox.Show("Voulez-vous autoriser " + _trame.from + " à vous envoyer un fichier ?", "Question ?", MessageBoxButtons.YesNo);
                            if (dialog_res == DialogResult.Yes)
                            {
                                //lance thread listenner fichier
                                s_ip_client_fichier = _trame.ipask;
                                StartReception();
                                SendMessage(TrameClient.getWaitForFileTrame(pseudo, _trame.from, ((IPAddress)Dns.Resolve(Dns.GetHostName()).AddressList[0]).ToString(), s_port_server_fichier, "1"));
                            }
                            else
                                SendMessage(TrameClient.getWaitForFileTrame(pseudo, _trame.from, ((IPAddress)Dns.Resolve(Dns.GetHostName()).AddressList[0]).ToString(), s_port_server_fichier, "0"));
                        }
                        else if (_trame.type == TrameType.WaitForFile)
                        {
                            if (_trame.answerfromwait == "1")
                            {
                                this.Invoke(WriteMessageDelegate, _trame.from + " accepte le fichier", Color.Green);
                                s_ip_server_fichier = _trame.ipwait;
                                s_port_server_fichier = _trame.portwait;
                                StartEnvoi();
                            }
                            else
                            {
                                this.Invoke(WriteMessageDelegate, _trame.from + " refuse le fichier", Color.Red);
                            }
                        }
                        else if (_trame.type == TrameType.WrongName)
                        {
                            this.Invoke(WriteMessageDelegate, "Changer de pseudo : déja utilisé", Color.Red);
                        }
                        else
                            this.Invoke(WriteMessageDelegate, _s_trame, Color.BlueViolet);
                    }
                    else
                        this.Invoke(WriteMessageDelegate, _s_trame, Color.Red);
                    
                }
                stm.Close();
            }
            catch (IOException ex)
            {
                if(CloseTcpClient()==1)
                    this.Invoke(DeconnexionDelegate, this);
                else
                    this.Invoke(WriteMessageDelegate, "Erreur dans la fermeture du socket", Color.Red);
            }

        }

        #endregion

        #region Méthodes en thread Fichier

        public void runReceptionFichier()
        {
            try
            {
                IPAddress ipAd = Dns.Resolve(Dns.GetHostName()).AddressList[0];
                tcp_listener_fichier = new TcpListener(ipAd, Convert.ToInt32(s_port_server_fichier));
                tcp_listener_fichier.Start();
                Socket s=null;
                Trame t=null;

                while (true)
                {
                    try
                    {
                        s = tcp_listener_fichier.AcceptSocket();
                        string s_ip_distant = s.RemoteEndPoint.ToString().Split(':')[0];
                        if (s_ip_distant == s_ip_client_fichier)//bon client
                        {
                            byte[] b;
                            int k;
                            b = new byte[1024];
                            k = s.Receive(b);

                            string temp = utf8.GetString(b);
                            t = new Trame(temp);
                            if (t.isValidTrame)
                            {
                                if (t.type == TrameType.FileTransmitHeader)
                                {
                                    this.Invoke(WriteMessageDelegate, t.blockcount+" "+t.lastblocksize, Color.Black);
                                    tcp_listener_fichier.Stop();
                                }
                            }
                            break;
                        }
                        else
                        {
                            s.Close();
                        }
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine("Erreur ..." + ex.StackTrace); 
                        break;
                    }
                }
                if (t != null)//pas de p
                {
                    byte[] buffer;
                    FileStream fsi = new FileStream(t.filename, FileMode.OpenOrCreate);
                    for (int i = 0; i < Convert.ToInt32(t.blockcount) - 1; i++)
                    {
                        int i_taille = Convert.ToInt32(t.blocksize);
                        buffer = new byte[i_taille];
                        s.Receive(buffer);
                        fsi.Write(buffer, 0, i_taille);
                        fsi.Flush();
                    }
                    int i_taille_eof = Convert.ToInt32(t.lastblocksize);
                    buffer = new byte[i_taille_eof];
                    s.Receive(buffer);
                    fsi.Write(buffer, 0, i_taille_eof);
                    fsi.Close();
                    fsi.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur ..." + ex.StackTrace);
            }

        }

        public void runEnvoiFichier()
        {
            try
            {
               bool co=false;
               while(!co)
               {
                    try{
                        tcp_client_fichier = new TcpClient(s_ip_server_fichier, Convert.ToInt32(s_port_server_fichier));
                        co = true;
                        //envoi de la première trame
                        Stream stm = tcp_client_fichier.GetStream();
                        byte[] ba = utf8.GetBytes(ef_fichier.getTrame());
                        stm.Write(ba, 0, ba.Length);

                        //envoi contenu
                        byte[] buffer;
                        FileStream fso = new FileStream(ef_fichier.s_nom_fichier, FileMode.Open);
                        for (int i = 0; i < ef_fichier.i_nb_block_fichier - 1; i++)
                        {
                            int i_taille = Convert.ToInt32(ef_fichier.i_taille_block);
                            buffer = new byte[i_taille];
                            fso.Read(buffer, 0, i_taille);
                            stm = tcp_client_fichier.GetStream();
                            stm.Write(buffer, 0, i_taille);
                        }
                        int i_taille_eof = Convert.ToInt32(ef_fichier.i_taille_eof_block);
                        buffer = new byte[i_taille_eof];
                        fso.Read(buffer, 0, i_taille_eof);
                        stm = tcp_client_fichier.GetStream();
                        stm.Write(buffer, 0, i_taille_eof);
                        fso.Close();
                        fso.Dispose();
                        stm.Close();
                    }
                    catch(Exception ex){co=true;}
               }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur ..." + ex.StackTrace);
            }
        }

        #endregion

        #region Méthodes invokées dans les delegués

        public void AddMessage(string s_mess, Color c_color)
        {
            Trame _trame = new Trame(s_mess);
            if (_trame.isValidTrame)
            {
                string _mess="";
                if (_trame.type == TrameType.Message)
                {
                    if (_trame.from == pseudo) //l'envoi un message
                        _mess = pseudo + " => "+_trame.to+" : " + _trame.msg;
                    else if (_trame.to == pseudo) //message pour moi
                        _mess = _trame.from + " : " + _trame.msg;
                    else
                        _mess = "Message perdu " + _trame.data;
                    rtb_allmessage.SelectionColor = c_color;
                    rtb_allmessage.AppendText(System.DateTime.Now.ToLongTimeString() + " : " + _mess);
                    rtb_allmessage.AppendText("\n");
                }
                else if (_trame.type == TrameType.MessageToAll)
                {
                    rtb_allmessage.SelectionColor = c_color;
                    rtb_allmessage.AppendText(System.DateTime.Now.ToLongTimeString() + " : " + pseudo + " => ALL : " + _trame.msg);
                    rtb_allmessage.AppendText("\n");
                }
            }
            else
            {
                rtb_allmessage.SelectionColor = c_color;
                rtb_allmessage.AppendText(System.DateTime.Now.ToLongTimeString() + " : " + s_mess);
                rtb_allmessage.AppendText("\n");
            }

            rtb_allmessage.Select(rtb_allmessage.Text.Length, 0);
            rtb_allmessage.ScrollToCaret();
        }

        public void EnabledConnexion(main _main)
        {
            _main.bt_connexion.Enabled = false;
            _main.tb_adressip.Enabled = false;
            _main.tb_pseudo.Enabled = false;
            _main.tb_message.Enabled = true;
            _main.Text = "DarkMessenger on " + pseudo;
            _main.lb_client.Enabled = true;
            _main.tb_fichier.Enabled = true;
            _main.bt_send_fichier.Enabled = true;
            _main.pb_fichier.Enabled = true;

        }

        public void EnabledDeconnexion(main _main)
        {
            _main.bt_connexion.Enabled = true;
            _main.tb_adressip.Enabled = true;
            _main.tb_pseudo.Enabled = true;
            _main.tb_message.Enabled = false;
            _main.Text = "DarkMessenger";
            _main.lb_client.Enabled = false;
            _main.tb_fichier.Enabled = false;
            _main.tb_fichier.Text = "";
            _main.bt_send_fichier.Enabled = false;
            _main.pb_fichier.Enabled = false;
            _main.pb_fichier.Value = 0;
            _main.lb_client.Items.Clear();
            _main.AddMessage("Server arrêté", Color.Red);
        }

        public void LoadListClient(ArrayList _listclient)
        {
            lb_client.Items.Clear();
            lb_client.Items.Add("--ALL--");
            foreach(string _unclient in _listclient)
                lb_client.Items.Add(_unclient);
        }

        public void UpdateProgressBar(int _value)
        {
            pb_fichier.Value = _value;
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
            }
        }

        #endregion

        #region Fermeture de conexion

        public int CloseTcpClient()
        {
            try
            {
                if (tcp_client != null)
                {
                    tcp_client.Client.Close();
                    tcp_client.Close();
                    tcp_client = null;
                    return 1;
                }
            }
            catch (Exception ex) { return 0; }
            return 0;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            EnvoiFichier ef_test = new EnvoiFichier(tb_fichier.Text, "from", "to");
            byte[] bs;
            FileStream fso = new FileStream(tb_fichier.Text, FileMode.Open);
            FileStream fsi = new FileStream(tb_fichier.Text + ".new", FileMode.OpenOrCreate);
            for (int i = 0; i < ef_test.i_nb_block_fichier-1; i++)
            {
                int i_taille=Convert.ToInt32(ef_test.i_taille_block);
                bs=new byte[i_taille];
                fso.Read(bs, 0, i_taille);
                fsi.Write(bs, 0, i_taille);
            }
            int i_taille_eof = Convert.ToInt32(ef_test.i_taille_eof_block);
            bs = new byte[i_taille_eof];
            fso.Read(bs, 0, i_taille_eof);
            fsi.Write(bs, 0, i_taille_eof);
            fso.Close();
            fsi.Close();
        }

    }
}
