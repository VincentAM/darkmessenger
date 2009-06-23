using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace darkmessenger
{
    /// <summary>
    /// Liste des trame qui peuvent partir du client.
    /// </summary>
    public static class TrameClient
    {
        private static UTF8Encoding utf8 = new UTF8Encoding();

        public static string getConnectionTrame(string _from)
        {
            return "<trame><type>"+TrameType.Connection+"</type><from>" + _from + "</from></trame>";
        }

        public static string getMsgTrame(string _from, string _msg, string _to)
        {
            //XmlDocument xdoc = new XmlDocument();
            //XmlNode xracine = xdoc.CreateNode(XmlNodeType.Element, "trame", "");
            //xdoc.AppendChild(xracine);

            //XmlNode xtype = xdoc.CreateNode(XmlNodeType.Element, "type", "");
            //XmlNode xtype_data = xdoc.CreateNode(XmlNodeType.Text, TrameType.Message, "");
            //xtype.AppendChild(xtype_data);
            //xracine.AppendChild(xtype);

            //XmlNode xfrom = xdoc.CreateNode(XmlNodeType.Element, "from", "");
            //XmlNode xfrom_data = xdoc.CreateNode(XmlNodeType.Text, _from, "");
            //xfrom.AppendChild(xfrom_data);
            //xracine.AppendChild(xfrom);

            //XmlNode xto = xdoc.CreateNode(XmlNodeType.Element, "to", "");
            //XmlNode xto_data = xdoc.CreateNode(XmlNodeType.Text, _to, "");
            //xto.AppendChild(xto_data);
            //xracine.AppendChild(xto);

            //XmlNode xmsg = xdoc.CreateNode(XmlNodeType.Element, "msg", "");
            //XmlNode xmsg_data = xdoc.CreateNode(XmlNodeType.Text, _msg, "");
            //xmsg.AppendChild(xmsg_data);
            //xracine.AppendChild(xmsg);

            //return xdoc.InnerXml;

            return "<trame><type>"+TrameType.Message+"</type><from>" + _from + "</from><msg>" + chToASCII(_msg) + "</msg><to>" + _to + "</to></trame>";
        }

        public static string getMsgToAllTrame(string _from, string _msg)
        {
            return "<trame><type>"+TrameType.MessageToAll+"</type><from>" + _from + "</from><msg>" + _msg + "</msg></trame>";
        }

        public static string getDisconnectionTrame(string _from)
        {
            return "<trame><type>"+TrameType.Disconnection+"</type><from>" + _from + "</from></trame>";
        }

        public static string chToASCII(string _s)
        { 
            string toto = "";
            char[] tab = _s.ToCharArray();

            foreach (char c in tab)
            {
                toto += (int)c+"@";
            }

            return toto;
        }

        public static string ASCIIToCh(string _s)
        {
            string toto = "";
            foreach (string s in _s.Substring(0, _s.Length-1).Split('@'))
            {
                int i = Convert.ToInt32(s);

                toto += Convert.ToChar(i);
            }
            return toto;
        }
    }
}
