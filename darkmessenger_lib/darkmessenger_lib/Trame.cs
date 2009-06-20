using System;
using System.Collections;
using System.Text;
using System.Xml;

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

        public bool isValidTrame;

        public Trame(string _data)
        {
            this.data = _data;
            this.isValidTrame = true;
            this.load();
        }

        private void load()
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(this.data);
                XmlNode racine = xdoc.GetElementsByTagName("trame")[0];
                this.from = racine.SelectSingleNode("from").FirstChild.Value.Trim();
                this.type = racine.SelectSingleNode("type").FirstChild.Value.Trim();
            }
            catch (XmlException ex)
            {
                this.isValidTrame = false;
            }
        }
    }
}
