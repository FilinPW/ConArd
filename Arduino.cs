using System;
using System.IO.Ports;

namespace Conard
{
    class Arduino
    {
        public SerialPort ConnectPort()
        {
            SerialPort port;
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                Console.WriteLine("Отсутствуют доступные порты");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Доступные порты:");

            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine("[" + i.ToString() + "] " + ports[i].ToString());
            }

            port = new SerialPort();
            int num;

            string n = Console.ReadLine();
            try
            {
                num = int.Parse(n);
            }
            catch(Exception e)
            {
                Console.WriteLine("Неверно указан номер порта");
                num = -1;
            }

            if ((num >= ports.Length) || (num < 0))
            {
                while ((num >= ports.Length) || (num < 0)) {
                    Console.WriteLine("Необходимо выбрать номер порта из списка");
                    for (int i = 0; i < ports.Length; i++)
                    {
                        Console.WriteLine("[" + i.ToString() + "] " + ports[i].ToString());
                    }
                    n = Console.ReadLine();
                    try
                    {
                        num = int.Parse(n);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Неверно указан номер порта");
                        num = -1;
                    }
                }
            }

            try
            {
                port.PortName = ports[num];
                port.BaudRate = 115200;
                port.ReadTimeout = 1000000;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.ReadTimeout = 900000;
                port.WriteTimeout = 1000;
                port.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: невозможно открыть порт:" + e.ToString());
                return port;
            }

            return port;
        }

        public float getTemperature(SerialPort port)
        {
            String portLine;
            float tem;

            try
            {
                port.Write("1");
                portLine = port.ReadLine();
            }
            catch
            {
                portLine = "";
            }

            portLine = portLine.Replace('.', ',');
            tem = Convert.ToSingle(portLine);

            return tem;
        }

    }
}
