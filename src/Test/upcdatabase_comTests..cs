//Uses XML-RPC.NET 2.5.0 written by Charles Cook
//https://www.nuget.org/packages/xmlrpcnet/2.5.0

using System;
using CookComputing.XmlRpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CER.Test
{
    [TestClass]
    public class upcdatabase_comTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new Client();
            var s = c.GetDescription("0788687100144");
        }

        [XmlRpcUrl("http://www.upcdatabase.com/xmlrpc")]
        public interface IUPCDatabase : IXmlRpcProxy
        {
            [XmlRpcMethod("help")]
            XmlRpcStruct Help();

            [XmlRpcMethod("lookup")]
            XmlRpcStruct Lookup(XmlRpcStruct s);
        }

        public class Client
        {
            //turns out this is not a free service.
            const string RPCKEY = "0000000000000000000000000000000000000000"; 
            private IUPCDatabase _proxy;

            public Client()
            {
                this._proxy = XmlRpcProxyGen.Create<IUPCDatabase>();
            }

            public string GetDescription(string strUPC)
            {
                var request = new XmlRpcStruct();
                var response = new XmlRpcStruct();
                                

                request.Add("rpc_key", RPCKEY);
                request.Add("upc", strUPC);

                response = _proxy.Lookup(request);
                return response["description"] + " " + response["size"];
            }
        }
    }
}
