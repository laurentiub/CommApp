using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace CommApp.Logic
{
    public interface IView
    {
        string GetIpServer();
        string GetPortServer();

        string GetIpClient();
        string GetPortClient();
        void ShowMessage(string newMessage);
    }

    class Client
    {
        public Socket connection;
        public string Name;
    }
    class Communication
    {
        Socket communicationSocket;
        IView interfata;

        public Communication(IView interfata)
        {
            this.interfata = interfata;
        }

        public void StartServer()
        {
            string ipServer = interfata.GetIpServer();
            int portServer = Convert.ToInt32(interfata.GetPortServer());

            IPEndPoint ep =
                new IPEndPoint(IPAddress.Parse(ipServer), portServer);

            communicationSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            communicationSocket.Bind(ep);

            communicationSocket.Listen(10);

            communicationSocket.BeginAccept(ClientAccepted, null);

            interfata.ShowMessage("Server started");
        }

        public void Connect()
        {
            communicationSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(interfata.GetIpClient()),
                Convert.ToInt32(interfata.GetPortClient()));

            communicationSocket.Connect(ep);
        }

        internal void SendMessage()
        {
            throw new NotImplementedException();
        }

        List<Client> connectedClients = new List<Client>();
        private void ClientAccepted(IAsyncResult ar)
        {
            Socket connectedClient = communicationSocket.EndAccept(ar);


            connectedClients.Add(new Client()
            {
                connection = connectedClient,
                Name = "Client1"
            });
            //connectedClient.Send();
            //connectedClient.Receive();

            interfata.ShowMessage("Client connected");

            communicationSocket.BeginAccept(ClientAccepted, null);
        }
    }
}
