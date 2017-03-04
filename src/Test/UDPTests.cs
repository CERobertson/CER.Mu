using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CER.Test {
    [TestClass]
    public class UDPTests {
        [TestMethod]
        public void UDPClient() {
            //Creates a UdpClient for reading incoming data.
            UdpClient receivingUdpClient = new UdpClient(52138);

            //Creates an IPEndPoint to record the IP Address and port number of the sender. 
            // The IPEndPoint will allow you to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

            string returnData = Encoding.ASCII.GetString(receiveBytes);
        }
    }
}
