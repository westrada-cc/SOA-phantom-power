using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GIORP_TOTAL
{
    public class RequestReceivedEventArgs : EventArgs
    {
        public string Request { get; private set; }
        public string Response { get; set; }

        public RequestReceivedEventArgs(string request)
        {
            this.Request = request;
        }
    }

    public static class SocketListener
    {
        private static string _ip;

        private static int _port;

        private static string _endOfRequestString;

        private static bool _shouldShutDown = false;

        private static bool _isShuttingDown = false;

        // State object for reading client data asynchronously
        private class StateObject 
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();  
        }

        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public async static void StartListeningAsync(string ip, int port, string endOfRequest) 
        {
            _ip = ip;
            _port = port;
            _endOfRequestString = endOfRequest;
            if (_isShuttingDown)
            {
                throw new InvalidOperationException("Server is shutting down. You have to wait until it is completed before you can start listening again.");
            }

            await Task.Run(() =>
            {
                // Data buffer for incoming data.
                byte[] bytes = new Byte[1024];

                // Establish the local endpoint for the socket.
                // The DNS name of the computer
                // running the listener is "host.contoso.com".
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPHostEntry ipHostInfo = Dns.Resolve(_ip);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, _port);

                // Create a TCP/IP socket.
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    while (_shouldShutDown == false)
                    {
                        // Set the event to nonsignaled state.
                        allDone.Reset();

                        // Start an asynchronous socket to listen for connections.
                        //Console.WriteLine("Waiting for a connection...");
                        listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener);

                        // Wait until a connection is made before continuing.
                        allDone.WaitOne();
                    }
                    _shouldShutDown = false;
                    _isShuttingDown = false;
                    // Clean up resources.
                    listener.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        private static void AcceptCallback(IAsyncResult ar) 
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private static void ReadCallback(IAsyncResult ar) 
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0) 
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer,0,bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf(_endOfRequestString) > -1) {
                    // All the data has been read from the client.
                    // Raise an event in order to get a response for the client.
                    var eventArgs = new RequestReceivedEventArgs(content);
                    RaiseRequestReceived(eventArgs);
                    // Whoever subscribed needs to populate the response of the RequestReceivedEventArgs
                    Send(handler, eventArgs.Response);
                } else {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        public static event EventHandler<RequestReceivedEventArgs> RequestReceived;

        private static void RaiseRequestReceived(RequestReceivedEventArgs request)
        {
            var handler = RequestReceived;
            if (handler != null)
            {
                handler(null, request);
            }
        }

        private static void Send(Socket handler, String data) 
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar) 
        {
            try 
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket) ar.AsyncState;
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            } 
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void StopListening()
        {
            _shouldShutDown = true;
            _isShuttingDown = true;
        }
    }
}
