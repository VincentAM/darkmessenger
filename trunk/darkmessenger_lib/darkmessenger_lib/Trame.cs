using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

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

        public string ipask;
        public string ipwait;
        public string portwait;
        public string answerfromwait;

        public string blocksize;
        public string lastblocksize;
        public string blockcount;
        public string filename;

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
            data = data.Split(new string[]{"</trame>"}, StringSplitOptions.None)[0];
            data += "</trame>";

            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(this.data);
                XmlNode racine = xdoc.GetElementsByTagName("trame")[0];
                this.from = TrameType.ASCIIToCh(racine.SelectSingleNode("from").FirstChild.Value.Trim());
                this.type = racine.SelectSingleNode("type").FirstChild.Value.Trim();

                if (this.type == TrameType.Message)
                {
                    this.to = TrameType.ASCIIToCh(racine.SelectSingleNode("to").FirstChild.Value.Trim());
                    this.msg = TrameType.ASCIIToCh(racine.SelectSingleNode("msg").FirstChild.Value.Trim());
                }
                else if (this.type == TrameType.ListOfClient)
                {
                    listClients = new ArrayList();

                    for (int i = 0; i < racine.SelectSingleNode("clients").SelectNodes("client").Count; i++)
                    {
                        listClients.Add(TrameType.ASCIIToCh(((XmlNode)racine.SelectSingleNode("clients").SelectNodes("client")[i]).FirstChild.Value.Trim()));
                    }
                }
                else if (this.type == TrameType.MessageToAll)
                {
                    this.msg = TrameType.ASCIIToCh(racine.SelectSingleNode("msg").FirstChild.Value.Trim());
                }
                else if (this.type == TrameType.AskForFile)
                {
                    this.to = TrameType.ASCIIToCh(racine.SelectSingleNode("to").FirstChild.Value.Trim());
                    this.ipask = TrameType.ASCIIToCh(racine.SelectSingleNode("ipask").FirstChild.Value.Trim());
                }
                else if (this.type == TrameType.WaitForFile)
                {
                    this.to = TrameType.ASCIIToCh(racine.SelectSingleNode("to").FirstChild.Value.Trim());
                    this.ipwait = TrameType.ASCIIToCh(racine.SelectSingleNode("ipwait").FirstChild.Value.Trim());
                    this.portwait = TrameType.ASCIIToCh(racine.SelectSingleNode("portwait").FirstChild.Value.Trim());
                    this.answerfromwait = TrameType.ASCIIToCh(racine.SelectSingleNode("answer").FirstChild.Value.Trim());
                }
                else if (this.type == TrameType.FileTransmitHeader)
                {
                    this.to = TrameType.ASCIIToCh(racine.SelectSingleNode("to").FirstChild.Value.Trim());
                    this.blocksize = TrameType.ASCIIToCh(racine.SelectSingleNode("blocksize").FirstChild.Value.Trim());
                    this.lastblocksize = TrameType.ASCIIToCh(racine.SelectSingleNode("lastblocksize").FirstChild.Value.Trim());
                    this.blockcount = TrameType.ASCIIToCh(racine.SelectSingleNode("blockcount").FirstChild.Value.Trim());
                    this.filename = TrameType.ASCIIToCh(racine.SelectSingleNode("filename").FirstChild.Value.Trim());
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
