using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace client
{
    class Trame
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string from;
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        private string to;
        public string To
        {
            get { return to; }
            set { to = value; }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Trame(string _data)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.LoadXml(_data);
            }
            catch (XmlException ex)
            {
                Console.WriteLine("La srting recue n'est pas un xml.");
            }

            //xdoc.GetElementsByTagName("id")[0].FirstChild.InnerText;
        }
    }
}
