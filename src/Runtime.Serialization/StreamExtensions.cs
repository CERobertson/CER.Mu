namespace CER.Runtime.Serialization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;

    public static class StreamExtensions
    {
        public static void Serialize<T>(this Stream stream, T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
        }

        public static T Deserialize<T>(this Stream stream) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            return serializer.ReadObject(stream) as T;
        }

        public static bool IsEqualTo(this Stream left_stream, Stream right_stream)
        {
            const int bufferSize = 2048 * 2;
            var buffer1 = new byte[bufferSize];
            var buffer2 = new byte[bufferSize];

            while (true)
            {
                int count1 = left_stream.Read(buffer1, 0, bufferSize);
                int count2 = right_stream.Read(buffer2, 0, bufferSize);

                if (count1 != count2)
                {
                    return false;
                }

                if (count1 == 0)
                {
                    return true;
                }

                int iterations = (int)Math.Ceiling((double)count1 / sizeof(Int64));
                for (int i = 0; i < iterations; i++)
                {
                    if (BitConverter.ToInt64(buffer1, i * sizeof(Int64)) !=
                        BitConverter.ToInt64(buffer2, i * sizeof(Int64)))
                    {
                        return false;
                    }
                }
            }
        }
    }
}
