using System;
using System.Collections;
using System.Text;

namespace darkmessenger
{
    public class Trame
    {
        public string data;

        public string name;
        public string from;
        public string to;
        public string type;

        public string msg;
        public string value;

        public Trame(string _data)
        {
            this.data = _data;
        }
    }
}
