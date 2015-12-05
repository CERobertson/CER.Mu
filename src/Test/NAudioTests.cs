using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAudio.Wave;
using System.Threading;

namespace CER.Test
{
    [TestClass]
    public class NAudioTests
    {
        [TestMethod]
        public void EnumerateWaveInDevices()
        {
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                Console.WriteLine("Device {0}: {1}, {2} channels",
                    waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
            }
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.DataAvailable += waveIn_DataAvailable;
            waveIn.WaveFormat = recordingFormat;
            waveIn.StartRecording();
            Thread.Sleep(6000);
            writer.Close();
        }

        private static WaveFormat recordingFormat = new WaveFormat();
        private WaveFileWriter writer = new WaveFileWriter("D:\\Audio\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".wav", recordingFormat);
        private WaveInEvent waveIn;

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            long maxFileLength = recordingFormat.AverageBytesPerSecond * 60;
            int toWrite = (int)Math.Min(maxFileLength - writer.Length, e.BytesRecorded);
            if (toWrite > 0)
                writer.Write(e.Buffer, 0, e.BytesRecorded);
            else
                waveIn.StopRecording();

        }

    }
}
