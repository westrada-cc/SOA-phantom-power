using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebClient
{
    public static class ServiceClient
    {
        public static string SendRequest(string request, string serverName, int port) 
        {
            string response = null;
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try 
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                IPHostEntry ipHostInfo = Dns.Resolve(serverName);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily, 
                    SocketType.Stream, ProtocolType.Tcp );

                // Connect the socket to the remote endpoint. Catch any errors.
                try 
                {
                    sender.Connect(remoteEP);

                    Logger.logMessage("Sending service request to IP " + serverName + ", PORT " + port.ToString() + " :" +
                                      Environment.NewLine +
                                      "\t>>" + request);

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(request);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    Logger.logMessage("Response from Published Service at IP " + serverName + ", PORT " + port.ToString() + " :" +
                                      Environment.NewLine +
                                      "\t>>" + response);

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                } 
                catch (ArgumentNullException ane) 
                {
                    //Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
                    Logger.logException(ane);

                    throw ane;
                } 
                catch (SocketException se) 
                {
                    //Console.WriteLine("SocketException : {0}",se.ToString());
                    Logger.logException(se);

                    throw se;
                } 
                catch (Exception e) 
                {
                    //Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    Logger.logException(e);

                    throw e;
                }
            } 
            catch (Exception e) 
            {
                //Console.WriteLine( e.ToString());
                Logger.logException(e);

                throw e;
            }

            return response;
        }
    }
}
