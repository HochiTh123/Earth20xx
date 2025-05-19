using System.Net.Sockets;
using System.Net;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;

namespace NetWork
{
    public class Client
    {
        public UdpClient UdpClient;
        public IPEndPoint Server;
        public void Init(IPAddress server, int port)
        {
            Server = new IPEndPoint(server, port);
            UdpClient = new UdpClient();
            UdpClient.ExclusiveAddressUse = false;
        }
        public void Init(IPEndPoint server)
        {
            this.Server = server;
            UdpClient = new UdpClient();
            UdpClient.ExclusiveAddressUse = false;
        }
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }
        public void SendMessage(string message)
        {
            var buffer = System.Text.Encoding.ASCII.GetBytes(message);
            SendMessage(buffer);
        }

        public void SendMessage(byte[] buffer)
        {
            UdpClient.Send(buffer, buffer.Length, Server);
        }

        public void Update()
        {
            if (UdpClient.Available > 0)
            {
                IPEndPoint sender = null;
                var message = UdpClient.Receive(ref sender);
                ReceivedMessage re = new ReceivedMessage(sender, message);
                MessageReceivedEventArgs m = new MessageReceivedEventArgs(re);
                OnMessageReceived(m);
            }
        }
    }
    public class MessageReceivedEventArgs
    {
        public MessageReceivedEventArgs(ReceivedMessage message)
        {
            this.Message = message;
        }
        public ReceivedMessage Message { get; private set; }
    }
    public class ReceivedMessage
    {
        public ReceivedMessage(IPEndPoint sender, byte[] data)
        {
            this.Sender = sender;
            this.Buffer = data;
            this.Message = System.Text.Encoding.ASCII.GetString(this.Buffer);
        }
        public IPEndPoint Sender;
        public Byte[] Buffer;
        public string Message;
    }

   
}
