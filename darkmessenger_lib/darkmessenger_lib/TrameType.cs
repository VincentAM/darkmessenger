using System;
using System.Collections;
using System.Text;

namespace darkmessenger
{
    public static class TrameType
    {
        public static string Connection = "connection";
        public static string Disconnection = "disconnection";
        public static string Message = "msg";
        public static string MessageToAll = "msgtoall";
        public static string ListOfClient = "listofclient";
        public static string WrongName = "wrongname";
        public static string Kicked = "kicked";

        public static string chToASCII(string _s)
        {
            string toto = "";
            char[] tab = _s.ToCharArray();

            foreach (char c in tab)
            {
                toto += (int)c + "@";
            }

            return toto;
        }
        public static string ASCIIToCh(string _s)
        {
            string toto = "";
            foreach (string s in _s.Substring(0, _s.Length - 1).Split('@'))
            {
                int i = Convert.ToInt32(s);

                toto += Convert.ToChar(i);
            }
            return toto;
        }

        private static UTF8Encoding utf8 = new UTF8Encoding();
    }
}
