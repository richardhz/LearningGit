using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCoreStuff
{
    public class GateKeeper
    {
        public DateTime currentTime { get; private set; }
        public CancellationToken Token { get; }

        private CancellationTokenSource _cancellationTokenSource;
        private UdpClient server = null;
        private IPEndPoint remoteEndPoint = null;
        public GateKeeper()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Token = _cancellationTokenSource.Token;    
        }

        public Task Listner()
        {
            server = new UdpClient(11000);
            remoteEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            Task task = Task.Factory.StartNew(
                () => Listen(Token),TaskCreationOptions.LongRunning
                );
            return task;
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task Listen(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var buffer = await server.ReceiveAsync();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                currentTime = DateTime.UtcNow;
                Thread.Sleep(500);
            }
        }
    }
}
