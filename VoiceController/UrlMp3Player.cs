using NAudio.Wave;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace VoiceController
{
    public class UrlMp3Player
    {
        public void PlayMp3FromUrl(string url)
        {
            using (var ms = new MemoryStream())
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var buffer = new byte[Int16.MaxValue];
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
                {
                    using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.PlaybackStopped += (sender, e) =>
                            {
                                try
                                {
                                    if (waveOut != null)
                                        waveOut.Stop();
                                }
                                catch { }
                            };
                        waveOut.Play();

                        while (waveOut.PlaybackState == PlaybackState.Playing)
                            Thread.Sleep(10);
                    }
                }
            }
        }
    }
}
