using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace darkmessenger
{
    /// <summary>
    /// Liste des trames qui peuvent être envoyées à partir du client.
    /// </summary>
    public static class TrameClient
    {
        public static string getConnectionTrame(string _from)
        {
            return "<trame><type>"+TrameType.Connection+"</type><from>" + TrameType.chToASCII(_from) + "</from></trame>";
        }

        public static string getMsgTrame(string _from, string _msg, string _to)
        {
            return "<trame><type>"+TrameType.Message+"</type><from>" + TrameType.chToASCII(_from) + "</from><msg>" + TrameType.chToASCII(_msg) + "</msg><to>" + TrameType.chToASCII(_to) + "</to></trame>";
        }

        public static string getMsgToAllTrame(string _from, string _msg)
        {
            return "<trame><type>"+TrameType.MessageToAll+"</type><from>" + TrameType.chToASCII(_from) + "</from><msg>" +TrameType.chToASCII( _msg) + "</msg></trame>";
        }

        public static string getDisconnectionTrame(string _from)
        {
            return "<trame><type>"+TrameType.Disconnection+"</type><from>" + TrameType.chToASCII(_from) + "</from></trame>";
        }
    }
}
