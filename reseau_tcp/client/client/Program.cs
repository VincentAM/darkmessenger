﻿using System;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace client
{
    class Program
    {
        static void Main()
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecion.....");

                tcpclnt.Connect("192.168.0.2", 9999);
                // use the ipaddress as in the server program
                Console.WriteLine("Connecté");

                byte[] ba;
                String str="";
                Stream stm;
                ASCIIEncoding asen = new ASCIIEncoding();

                while (true)
                {
                    if (str == "q")
                    {
                        break;
                    }

                    Console.Write("Taper qqchose : ");
                    str = Console.ReadLine();
                    stm = tcpclnt.GetStream();

                    ba = asen.GetBytes(str);
                    Console.WriteLine("Transmission ...");
                    stm.Write(ba, 0, ba.Length);
                }

                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Erreur ..." + e.StackTrace);
                Console.Read();
            }
        }
    }
}
