namespace CER.Test
{
    using CER.Runtime.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class SerializableTests<T, S>
    {
        public delegate Stream Assertion(T objectToTest, S serializableDataContext);
        public  Assertion[] Assertions = new Assertion[]
        {
            (obj, sdc) => 
                {
                    var stream = new MemoryStream();
                    stream.Serialize<S>(sdc);
                    return stream;
                }
        };
    }
}