using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace darkmessenger
{
    public class trame
    {
        public string id;
        public string name;
        public string from;
        public string to;
        public string action;

        public bool isValid;

        public string data;
        public string message;

        public trame(string _s)
        {
            this.data = _s;
            this.isValid = true;
            this.load();
        }

        private void load()
        {
            if (data != "")
            {
                try
                {
                    XmlDocument xdoc = new XmlDocument();

                    xdoc.LoadXml(data);
                    XmlNode racine = xdoc.GetElementsByTagName("trame")[0];
                    this.id = racine.SelectSingleNode("id").FirstChild.Value.Trim();
                    this.name = racine.SelectSingleNode("name").FirstChild.Value.Trim();
                    this.from = racine.SelectSingleNode("from").FirstChild.Value.Trim();
                    this.to = racine.SelectSingleNode("to").FirstChild.Value.Trim();
                    this.action = racine.SelectSingleNode("action").FirstChild.Value.Trim();
                }
                catch { this.isValid = false; }
            }
        }
    }
}

