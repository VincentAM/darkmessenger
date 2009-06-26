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
            string str = "<trame><type>"+TrameType.ListOfClient+"</type><from>"+TrameType.chToASCII("server")+"</from><clients>";

            foreach (string s in _list)
            {
                str += "<client>"+TrameType.chToASCII(s)+"</client>";
            }
            
            str+="</clients></trame>";

            return str;
        }

        public static string getWrongNameTrame()
        {
            return "<trame><type>" + TrameType.WrongName + "</type><from>"+TrameType.chToASCII("server")+"</from></trame>";
        }

        public static string getKickedTrame()
        {
            return "<trame><type>" + TrameType.Kicked + "</type><from>"+TrameType.chToASCII("server")+"</from></trame>";
        }

        public static string getAskForFileTrame(string _from, string _to, string _ipask)
        {
            return "<trame><type>" + TrameType.AskForFile + "</type><from>" + TrameType.chToASCII(_from) + "</from><to>" + TrameType.chToASCII(_to) + "</to><ipask>" + TrameType.chToASCII(_ipask) + "</ipask></trame>";
        }

        public static string getWaitForFileTrame(string _from, string _to, string _ipwait, string _portwait, string _answer)
        {
            return "<trame><type>" + TrameType.WaitForFile + "</type><from>" + TrameType.chToASCII(_from) + "</from><to>" + TrameType.chToASCII(_to) + "</to><ipwait>" + TrameType.chToASCII(_ipwait) + "</ipwait><portwait>" + TrameType.chToASCII(_portwait) + "</portwait><answer>" + TrameType.chToASCII(_answer) + "</answer></trame>";
        }
    }
}
