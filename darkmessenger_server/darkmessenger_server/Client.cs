using System;
using System.Collections;
using System.Text;
using System.Net.Sockets;

namespace darkmessenger_server
{
    class Client
    {
        private string name;
        public string Name
        {
            get { return name; }
            //set { name = value; }
        }
        private Socket socket;
        public Socket Socket
        {
            get { return socket; }
            //set { socket = value; }
        }

        private string ip;
        public string Ip
        {
            get { return ip; }
            //set { ip = value; }
        }

        public Client(string _n, Socket _s, string _i)
        {
            this.name = _n;
            this.socket = _s;
            this.ip = _i;
        }
    }
}
