using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class GameEngine
    {
        #region Variables

        private int playerNum;      //player number (client)
        private Point startLoc;     //start location of player
        private int starDir;        //start direction of player
        private Point[,] grid;    //the grid
        private int mapSize;       //no of rows and columns in the grid
        private List<Point> brickLocations;
        private List<Point> stoneLocations;
        private List<Point> waterLocations;
        private List<Point> coinLocations;
        private List<char> msgTypes;

        #endregion

        public GameEngine()
        {
            msgTypes = new List<char>();
            msgTypes.Add('S');
            msgTypes.Add('I');
            msgTypes.Add('G');
            msgTypes.Add('C');
            msgTypes.Add('L');
            //generateGrid(mapDetails);        
        }

        public void handleMessage(String message)
        {
            message = message.Substring(0, message.Length - 2);
            char firstChar = message[0];
            if (msgTypes.Contains(firstChar))
            {
                if (firstChar == 'S')
                {
                    initialize(message);
                }
                if (firstChar == 'I')
                {
                    generateGrid(message);
                }
                if (firstChar == 'G')
                {
                    updateMap(message);
                }
                if (firstChar == 'C')
                {
                    handleCoins(message);
                }
                if (firstChar == 'L')
                {
                    handleLifePacks(message);
                }
            }
            else
            {

            }
        }

        private void initialize(string starter)
        {
            playerNum = int.Parse(char.ToString(starter[3]));
            string loc = starter.Split(':')[2];
            startLoc = new Point(int.Parse(loc.Split(',')[0]), int.Parse(loc.Split(',')[1]));
            starDir = int.Parse(starter.Split(':')[3]);
            Console.WriteLine(playerNum + " " + startLoc + " " + starDir);
        }

        private void generateGrid(string map)
        {
            this.mapSize = 10;
            brickLocations = new List<Point>();
            stoneLocations = new List<Point>();
            waterLocations = new List<Point>();
            coinLocations = new List<Point>();
            setLocations(map);
            grid = new Point[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    grid[i, j] = new Point(j, i);
                    if (brickLocations.Contains(grid[i, j]))
                    {
                        Console.Write("B ");
                    }
                    else if (stoneLocations.Contains(grid[i, j]))
                    {
                        Console.Write("S ");
                    }
                    else if (waterLocations.Contains(grid[i, j]))
                    {
                        Console.Write("W ");
                    }
                    else
                    {
                        Console.Write("N ");
                    }
                    //Console.Write(grid[i, j] + " ");

                }
                Console.WriteLine();
            }
        }

        private void setLocations(string map)
        {
            string[] splittedValues = map.Split(':');
            setLocationLists(splittedValues[2], brickLocations);
            setLocationLists(splittedValues[3], stoneLocations);
            setLocationLists(splittedValues[4], waterLocations);
        }

        private void setLocationLists(string values, List<Point> locations)
        {
            Point p;
            string[] tokens = values.Split(';');
            for (int i = 0; i < tokens.Length; i++)
            {
                p = new Point(int.Parse(tokens[i].Split(',')[0]), int.Parse(tokens[i].Split(',')[1]));
                try
                {
                    locations.Add(p);
                }
                catch (NullReferenceException)
                {
                    //Console.WriteLine("exception");
                }

            }
        }

        private void updateMap(string msg)
        {

        }

        private void handleCoins(string msg)
        {
            string[] tokens = msg.Split(':');
            Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
            coinLocations.Add(p);
            CoinPile coins = new CoinPile(p, int.Parse(tokens[2]), 0, int.Parse(tokens[3]));
        }

        private void handleLifePacks(string msg)
        {
            string[] tokens = msg.Split(':');
            Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
            coinLocations.Add(p);
            LifePack lifepack = new LifePack(p, int.Parse(tokens[2]), 0);
        }
    }
}
