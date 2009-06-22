using System;
using System.Collections;
using System.Text;

namespace darkmessenger
{
    /// <summary>
    /// Liste des trames pouvant être envoyées depuis le server
    /// </summary>
    public static class TrameServer
    {
        private static UTF8Encoding utf8 = new UTF8Encoding();

        public static string getListOfConnectedTrame(ArrayList _list)
        {
            string str = "<trame><type>list_of_connected</type><from>server</from><clients>";

            foreach (string s in _list)
            {
                str += "<client>"+s+"</client>";
            }
            
            str+="<clients></trame>";

            return str;
        }

        public static string getMsgTestTrame(string _to)
        { 
            return "<trame><type>msg</type><from>server</from><msg>test<msg><to>" + utf8.GetBytes(_to) + "<to></trame>";
        }
    }
}
