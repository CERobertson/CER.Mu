namespace CER.Runtime.Serialization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;

    public static class StreamExtensions
    {
        public static void Serialize<T>(this Stream stream, T obj)
        {
            stream.Serialize<T>(obj, new DataContractJsonSerializerSettings());
        }

        public static void Serialize<T>(this Stream stream, T obj, DataContractJsonSerializerSettings settings)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), settings);
            serializer.WriteObject(stream, obj);
        }

        public static T Deserialize<T>(this Stream stream) where T : class
        {
            return stream.Deserialize<T>(new DataContractJsonSerializerSettings());
        }

        public static T Deserialize<T>(this Stream stream, DataContractJsonSerializerSettings settings) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T), settings);
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

        public static byte[] ToByteArray(this string s)
        {
            byte[] bytes = new byte[s.Length * sizeof(char)];
            Buffer.BlockCopy(s.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string _ToString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
