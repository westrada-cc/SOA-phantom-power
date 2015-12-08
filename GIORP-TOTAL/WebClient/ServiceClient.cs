using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HL7Library;
using GIORP_TOTAL;

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

                    logClientMessage("Sending service request to IP " + serverName + ", PORT " + port.ToString() + " :", request);

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(request);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    response = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    var trimmedResponse = new StringBuilder();
                    foreach(var character in response)
                    {
                        if (character == (char)28)
                        {
                            trimmedResponse.Append(HL7Library.HL7Utility.EndOfMessage);
                            break;
                        }

                        trimmedResponse.Append(character);
                    }

                    response = trimmedResponse.ToString();

                    logClientMessage("Response from Published Service at IP " + serverName + ", PORT " + port.ToString() + " :", response);

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                } 
                catch (ArgumentNullException ane) 
                {
                    //Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
                    Logger.logException(AppDomain.CurrentDomain.BaseDirectory + "\\" + "LogFile.txt", ane);

                    throw ane;
                } 
                catch (SocketException se) 
                {
                    //Console.WriteLine("SocketException : {0}",se.ToString());
                    Logger.logException(AppDomain.CurrentDomain.BaseDirectory + "\\" + "LogFile.txt", se);

                    throw se;
                } 
                catch (Exception e) 
                {
                    //Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    Logger.logException(AppDomain.CurrentDomain.BaseDirectory + "\\" + "LogFile.txt", e);

                    throw e;
                }
            } 
            catch (Exception e) 
            {
                //Console.WriteLine( e.ToString());
                Logger.logException(AppDomain.CurrentDomain.BaseDirectory + "\\" + "LogFile.txt", e);

                throw e;
            }

            return response;
        }

        private static void logClientMessage(string messageHeader, string HL7Message)
        {
            //Deserialize the message
            Message tempMsg = HL7Utility.Deserialize(HL7Message.TrimEnd('\n'));
            StringBuilder sb = new StringBuilder();

            //Format the segments
            foreach (Segment seg in tempMsg.Segments)
            {
                sb.Append("\t\t>>");
                foreach (string s in seg.Elements)
                {
                    sb.AppendFormat("{0}|", s);
                }
                sb.Append(Environment.NewLine);
            }

            //Write to log file
            Logger.logMessage(messageHeader +
                              Environment.NewLine +
                              sb.ToString(),
                              AppDomain.CurrentDomain.BaseDirectory + "\\" + "LogFile.txt");
        }
    }
}
