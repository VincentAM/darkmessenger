using System;
using System.Collections;
using System.Text;

namespace darkmessenger
{
    /// <summary>
    /// Liste des trames pouvant être envoyées à partir du server.
    /// </summary>
    public static class TrameServer
    {
        public static string getListOfClientTrame(ArrayList _list)
        {
            string str = "<trame><type>"+TrameType.ListOfClient+"</type><from>server</from><clients>";

            foreach (string s in _list)
            {
                str += "<client>"+s+"</client>";
            }
            
            str+="</clients></trame>";

            return str;
        }

        public static string getMsgTestTrame(string _to)
        { 
            return "<trame><type>"+TrameType.Message+"</type><from>server</from><msg>test</msg><to>" + _to + "</to></trame>";
        }

        public static string getWrongNameTrame()
        {
            return "<trame><type>" + TrameType.WrongName + "</type><from>server</from></trame>";
        }

        public static string getKickedTrame()
        {
            return "<trame><type>" + TrameType.Kicked + "</type><from>server</from></trame>";
        }
    }
}
