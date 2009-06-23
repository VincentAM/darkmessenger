using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace darkmessenger
{
    public class Trame
    {
        public string data;

        public string from;
        public string to;
        public string type;

        public string msg;
        public string value;

        public bool isValidTrame;

        public ArrayList listClients;

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

                if (this.type == TrameType.Message)
                {
                    this.to = racine.SelectSingleNode("to").FirstChild.Value.Trim();
                    this.msg = racine.SelectSingleNode("msg").FirstChild.Value.Trim();
                }
                else if (this.type == TrameType.ListOfClient)
                {
                    listClients = new ArrayList();

                    for (int i = 0; i < racine.SelectSingleNode("clients").SelectNodes("client").Count; i++)
                    {
                        listClients.Add(((XmlNode)racine.SelectSingleNode("clients").SelectNodes("client")[i]).FirstChild.Value.Trim());
                    }
                }
                else
                {
                    XmlNode i = racine.SelectSingleNode("qqzfrqsqsdf");
                }
                
            }
            catch (XmlException ex)
            {
                this.isValidTrame = false;
            }
        }
    }
}
