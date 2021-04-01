using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {

        static void Main(string[] args)
        {
            // Инициализация
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            TcpListener server = new TcpListener(localAddr, port);
            // Запуск в работу
            server.Start();

            Console.WriteLine("Сервер начал работу... ");
            // Бесконечный цикл
            while (true)
            {//аопашопапшщоп
                try
                {
                    // Подключение клиента
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    // Обмен данными
                    try
                    {
                        if (stream.CanRead)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            StringBuilder myCompleteMessage = new StringBuilder();
                            int numberOfBytesRead = 0;
                            do
                            {
                                numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                            }
                            while (stream.DataAvailable);
                            Byte[] responseData = Encoding.UTF8.GetBytes("УСПЕШНО!");
                            stream.Write(responseData, 0, responseData.Length); //отправка данных на сервер
                        }
                    }
                    finally
                    {
                        stream.Close();
                        client.Close();
                        Console.WriteLine("Сервер отключен ");
                    }
                }
                catch
                {
                    server.Stop();
                    break;
                }
            }

            Console.ReadLine();

        }
    }
}
