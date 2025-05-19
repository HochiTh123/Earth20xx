using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using System.Net.Security;

namespace NetWork
{
    public class Server
    {

        public Server()
        {
            ServerUdp = new UdpClient();
            string hostname = "localhost";
            hostname = System.Net.Dns.GetHostName();
            var ips = System.Net.Dns.GetHostAddresses(hostname);
            IPAddress ipa = null;
            for (int i = 0; i <= ips.GetUpperBound(0); i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipa = ips[i].MapToIPv4();
                    break;
                }
            }
            ServerUdp.ExclusiveAddressUse = false;

            IpEndport = new IPEndPoint(ipa, 2000);
            ServerUdp = new UdpClient(IpEndport);
            WriteInConsole("Server started");
            WriteInConsole("Server ip: " + IpEndport.Address.ToString());
            WriteInConsole("Port: " + 2000.ToString());


        }
        public UdpClient ServerUdp;
        public Queue<Serverrecmessage> MessagesToWorkWith = new Queue<Serverrecmessage>();
        public Queue<Serversendmessage> MessagestoSend = new Queue<Serversendmessage>();
        public List<ServerClient> Clients = new List<ServerClient>();
        public IPEndPoint IpEndport;
        private void WriteInConsole(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " [ Server ] > " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Update(GameTime gameTime)
        {
            while (ServerUdp.Available > 0)
            {
                IPEndPoint sender = null;
                var buffer = ServerUdp.Receive(ref sender);
                Serverrecmessage rm = new Serverrecmessage(buffer, sender);
                MessagesToWorkWith.Enqueue(rm);
            }

            while (MessagesToWorkWith.Count > 0)
            {
                WorkOnMessage(MessagesToWorkWith.Dequeue());
            }

            while (MessagestoSend.Count > 0)
            {
                var message = MessagestoSend.Dequeue();
                if (message.Receiver == null)
                {
                    // Send TO All
                    for (int i = 0; i < Clients.Count;i++)
                    {
                        ServerUdp.Send(message.Buffer, message.Buffer.Length, Clients[i].IPE);
                    }
                }
                else
                {
                    ServerUdp.Send(message.Buffer, message.Buffer.Length, message.Receiver);
                }
            }


        }

        private void WorkOnMessage(ReceivedMessage message)
        {
            WriteInConsole("Message Received: " + message.Message);
            // First byte is the MessageByte 
            byte first = message.Buffer[0];
            switch(first)
            {
                case 0: // Connect
                    string username = Encoding.ASCII.GetString(message.Buffer, 1, message.Buffer.Length - 1);
                    // Checking if already connected
                    for (int i = 0; i < Clients.Count;i++)
                    {
                        if (Clients[i].Username == username)  // Username already in use
                        {
                          
                        }
                        else
                        {
                            ServerClient c = new ServerClient(message.Sender, username);
                            Clients.Add(c);
                            int index = Clients.Count - 1; // Clients Index;
                            // Message for OK + Index of the User;


                            // Message to all, that a new User is online
                        }
                    }
                    break;
                case 1: // Status
                    break;
                case 2: // Disconnect
                    break;
            }
        }

       
    }

    public class ServerClient
    {
        public ServerClient(IPEndPoint ipe)
        {
            this.IPE = ipe;
        }
        public ServerClient(IPEndPoint ipe, string username)
        {
            this.IPE = ipe;
            this.Username = username;
        }
        public IPEndPoint IPE;
        public string Username;
        public bool Online;
    }


    public class Serverrecmessage
    {
        public Serverrecmessage(byte[] buffer, IPEndPoint sender)
        {
            this.Buffer = buffer;
            this.Sender = sender;
        }
        public byte[] Buffer;
        public IPEndPoint Sender;
        

       

    }
    public class Serversendmessage
    {
        public Serversendmessage(byte[] buffer, IPEndPoint? receiver)
        {
            this.Buffer = buffer;
            this.Receiver = receiver;
        }
        public byte [] Buffer { get; set; }
        public IPEndPoint? Receiver { get; set; }

    }
}
