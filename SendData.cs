using System;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;

namespace Conard
{
    class SendData
    {
        public long Pings()
        {
            String[] hosts = { "mail.ru", "ya.ru", "google.ru", "vk.com", "facebook.com", "youtube.com", "google.com", "ok.ru", "yandex.ru", "avito.ru" };
            int errorConnect = 0;
            long ping = 0;
            Ping pingSender = new Ping();
            PingReply reply;
            PingOptions options = new PingOptions();
            string data = "01234567890123456789012345678901";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 5000;

            for (int i = 0; i < hosts.Length; i++) {
                try
                {
                    reply = pingSender.Send(hosts[i], timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        //Console.WriteLine(reply.RoundtripTime);
                        ping += reply.RoundtripTime;
                    }
                    else
                    {
                        errorConnect++;
                    }
                }
                catch
                {
                    errorConnect++;
                }
            }
            if (hosts.Length - errorConnect > 0)
            {
                return ping / (hosts.Length - errorConnect);
            }
            else
            {
                return -1;
            }
        }


        public void SendPost(String data)
        {
            WebRequest request = WebRequest.Create("http://your_server/temperature.php");
            Stream dataStream;
            request.Method = "POST";
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            try
            {
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: Ошибка при отправке данных на сервер:" + e.ToString());
            }
        }
    }
}