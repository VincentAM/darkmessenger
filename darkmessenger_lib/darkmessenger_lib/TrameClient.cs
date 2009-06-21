using System;
using System.Collections;
using System.Text;

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
            return "<trame><type>connection</type><from>" + _from + "</from></trame>";
        }

        public static string getMsgTrame(string _from, string _msg, string _to)
        {
            return "<trame><type>msg</type><from>" + _from + "</from><msg>" + utf8.GetBytes(_msg) + "<msg><to>" + utf8.GetBytes(_to) + "<to></trame>";
        }

        public static string getMsgToAllTrame(string _from, string _msg)
        {
            return "<trame><type>msgall</type><from>" + _from + "</from><msg>" + utf8.GetBytes(_msg) + "<msg></trame>";
        }

        public static string getDisconnectionTrame(string _from)
        {
            return "<trame><type>disconnection</type><from>" + _from + "</from></trame>";
        }

        public static string getListOfConnected(string _from)
        {
            return "<trame><type>list_of_connected</type><from>" + _from + "></from></trame>";
        }
    }
}
