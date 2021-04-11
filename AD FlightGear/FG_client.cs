using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace AD_FlightGear
{
    class FG_client
    {
        private string[]  _Lines_from_csv;
        public FG_client(string[] lines_from_csv)
        {
            _Lines_from_csv = lines_from_csv;
        }
        //send lines one by one from @_Lines_from_csv to FG
        void send_to_fg()
        {
            try
            {
                var client = new TcpClient("localhost", 5400);
                var stream = client.GetStream();
                //listener.Start();
                int i = 0;
                while (i < _Lines_from_csv.Length)
                {
                    byte[] sendbuf = Encoding.ASCII.GetBytes(_Lines_from_csv[i]);
                    stream.Write(sendbuf, 0, sendbuf.Length);
                    i++;
                    Thread.Sleep(100);
                }

               stream.Close();
               client.Close();
            }
            catch
            {
                Console.WriteLine("failed sending data to FG");
            }
        }
    }
}
