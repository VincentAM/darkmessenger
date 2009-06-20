﻿using System;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace server
{
    class Program
    {
        public static void Main()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("192.168.0.2");
                int port = 9999;
                TcpListener myList = new TcpListener(ipAd, port);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The server is running at port "+port.ToString()+"...");
                Console.WriteLine("The local End point is  :" +
                                  myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                byte[] b;
                int k;
                string str="";

                while (true)
                {
                    if (str == "q")
                    {
                        break;
                    }
                    else { str = ""; }

                    b = new byte[100];
                    k = s.Receive(b);
                    Console.WriteLine("Recieved...");
                    for (int i = 0; i < k; i++)
                        str += Convert.ToChar(b[i]);

                    Console.WriteLine(str);
                }

                s.Close();
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                Console.Read();
            }

            
        }
    }
}
