using System;
using System.Collections;
using System.Text;

namespace darkmessenger
{
    static class TrameClient
    {
        public static UTF8Encoding utf8 = new UTF8Encoding();

        public static string getConnectionTrame(string _from)
        {
            return "<trame><type>connection</type><from>" + utf8.GetBytes(_from) + "</from></trame>";
        }

        public static string getMsgTrame(string _from, string _msg, string _to)
        {
            return "<trame><type>msg</type><from>" + utf8.GetBytes(_from) + "</from><msg>" + utf8.GetBytes(_msg) + "<msg><to>" + utf8.GetBytes(_to) + "<to></trame>";
        }

        public static string getMsgToAllTrame(string _from, string _msg)
        {
            return "<trame><type>msgall</type><from>" + utf8.GetBytes(_from) + "</from><msg>" + utf8.GetBytes(_msg) + "<msg></trame>";
        }

        public static string getDisconnectionTrame(string _from)
        {
            return "<trame><type>disconnection</type><from>" + utf8.GetBytes(_from) + "</from></trame>";
        }
    }
}
