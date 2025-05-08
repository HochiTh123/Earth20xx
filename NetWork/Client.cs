using Lidgren.Network;
using System.Net;

namespace NetWork
{
    public class Client
    {
        public Lidgren.Network.NetClient ThisClient;
        public IPEndPoint Server;
        public NetConnection Connection;
        private NetConnectionStatus _status;
        public NetConnectionStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    NetStatusEventArgs e = new NetStatusEventArgs(_status);
                    OnStatusChanged(e);
                }
            }
        }
        public event EventHandler<NetStatusEventArgs> StatusChanged;
        private void OnStatusChanged( NetStatusEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }
        public void Init()
        {
            ThisClient = new Lidgren.Network.NetClient(new Lidgren.Network.NetPeerConfiguration("EARTH"));
          
        }

        public void Connect(IPEndPoint server)
        {
            this.Server = server;
            Connection = this.ThisClient.Connect(server);
           
        }
    }

    public class NetStatusEventArgs : EventArgs
    {
        public NetStatusEventArgs(NetConnectionStatus status)
        {
            this.Status = status;
        }
        public NetConnectionStatus Status { get; private set; }
    }
}
