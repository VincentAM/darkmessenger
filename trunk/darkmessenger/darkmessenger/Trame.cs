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

        public bool isValid;

        public string data;
        public string action;
        public string message;

        public trame(string _s)
        {
            this.load();
            this.isValid = true;
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
                    this.id = racine.SelectSingleNode("id").FirstChild.Value.Trim(toTrim);
                    this.name = racine.SelectSingleNode("name").FirstChild.Value.Trim(toTrim);
                    this.from = racine.SelectSingleNode("from").FirstChild.Value.Trim(toTrim);
                    this.to = racine.SelectSingleNode("to").FirstChild.Value.Trim(toTrim);
                    this.action = racine.SelectSingleNode("action").FirstChild.Value.Trim(toTrim);
                }
                catch { this.isValid = false; }
            }
        }
    }
}

