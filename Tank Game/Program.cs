using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Tank_Game;
using TankClient;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectClient client = new ConnectClient();
            client.recivePool();

            client.sendPool();
            Console.ReadLine();

            //string initialMsg = "S:P1:1,1:0#";
            //string mapDetails = "I:P1:1,1;2,3;3,4:2,5;6,8;7,0:3,1;4,2;6,8#";
            //
            //gameEngine.handleMessage(initialMsg);
            //gameEngine.handleMessage(mapDetails);
            //Console.ReadLine();
        }
    }
}
