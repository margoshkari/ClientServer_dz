using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static ServerData serverData = new ServerData();
        static bool isStartNewTask = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Start server...");
            try
            {
                serverData.socket.Bind(serverData.iPEndPoint);
                serverData.socket.Listen(10);

                Task.Factory.StartNew(() => Connect());

                while(true)
                {
                    if(isStartNewTask)
                    {
                        Task.Factory.StartNew(() => SendMsg());
                        isStartNewTask = false;
                    }
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Connect()
        {
            while (true)
            {
                serverData.socketClient = serverData.socket.Accept();
                serverData.socketClientList.Add(serverData.socketClient);

                serverData.socketClient.Send(Encoding.Unicode.GetBytes("Welcome on server!"));
            }
        }
        static void SendMsg()
        {
            Console.WriteLine("Enter message or path:");
            string msg = Console.ReadLine();
            foreach (var item in serverData.socketClientList)
            {
                if (File.Exists(msg) && Path.GetFileName(msg).Contains(".txt") || Path.GetFileName(msg).Contains(".rtf"))
                {
                    item.Send(Encoding.Unicode.GetBytes(File.ReadAllText(msg)));
                }
                else
                {
                    item.Send(Encoding.Unicode.GetBytes(msg));
                }
            }
            isStartNewTask = true;
        }
    }
}