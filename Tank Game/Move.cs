﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

namespace TankClient
{
    class Move
    {

        ConnectClient client = new ConnectClient();
        public void catchSend(string msg)
        {
            if (msg.Equals("OBSTACLE#"))
            {
                Console.WriteLine("OBSTACLE: YOU CAN'T MOVE THERE");
            }
            else if (msg.Equals("CELL_OCCUPIED#"))
            {
                Console.WriteLine("CELL_OCCUPIED#: YOU CAN'T GO IN THERE");
            }
            else if (msg.Equals("DEAD#"))
            {
                Console.WriteLine("DEAD#: YOU GAME OVER");
            }
            else if (msg.Equals("TOO_QUICK#"))
            {
                Console.WriteLine("TOO_QUICK#: YOU MOVED TOO QUICK");
            }
            else if (msg.Equals("INVALID_CELL#"))
            {
                Console.WriteLine("INVALID_CELL#: CAN'T MOVE THERE");
            }
            else if (msg.Equals("GAME_HAS_FINISHED#"))
            {
                Console.WriteLine("GAME_HAS_FINISHED#: PLAY AGAIN");
            }
            else if (msg.Equals("GAME_NOT_STARTED_YET#"))
            {
                Console.WriteLine("GAME_NOT_STARTED_YET#: PLEASE WAIT");
            }
            else if (msg.Equals("NOT_A_VALID_CONTESTANT#"))
            {
                Console.WriteLine("NOT_A_VALID_CONTESTANT#: YOU CAN'T PLAY");
            }

        }

    }
}
