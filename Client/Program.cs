using System;

namespace Client
{
    class Program
    {
        static ClientData clientData = new ClientData();
        static void Main(string[] args)
        {
            try
            {
                clientData.socket.Connect(clientData.iPEndPoint);

                Console.WriteLine(clientData.GetMsg());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}