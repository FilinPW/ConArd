using System;
using System.IO.Ports;
using System.Threading;

namespace Conard
{
    class Program
    {
        static void Main(string[] args)
        {
            SendData sd = new SendData();

            Arduino ard = new Arduino();
            SerialPort port = ard.ConnectPort();
            String sendData = "";
            String id = "id=your_id";
            String temperature;
            String pingTime;

            while (true)
            {
                pingTime = "ping_time=".Insert(10, sd.Pings().ToString());
                temperature = "temperature=".Insert(12, ard.getTemperature(port).ToString().Replace(',', '.'));

                sendData = sendData.Insert(sendData.Length, id);
                sendData = sendData.Insert(sendData.Length, "&");
                sendData = sendData.Insert(sendData.Length, pingTime);
                sendData = sendData.Insert(sendData.Length, "&");
                sendData = sendData.Insert(sendData.Length, temperature);
                sd.SendPost(sendData);

                Console.WriteLine();
                Console.WriteLine(DateTime.Now);
                Console.WriteLine(temperature);
                Console.WriteLine(pingTime);

                Thread.Sleep(300000);
            }

            //port.Close();
        }
    }
}
