using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    class GameEngine
    {
        #region Variables

        private int playerNum;      //player number (client)
        private Point startLoc;     //start location of player
        private int starDir;        //start direction of player
        private Point[,] grid;    //the grid
        private int mapSize;       //no of rows and columns in the grid
        private string mapDetails;     //map details from server
        private List<Point> brickLocations;
        private List<Point> stoneLocations;
        private List<Point> waterLocations;
        
        #endregion

        public GameEngine(string starter)
        {
            setStartStats(starter);
            mapSize = 10;
            mapDetails = "I:P1:1,1;2,3;3,4:2,5;6,8;7,0:3,1;4,2;6,8";
            brickLocations = new List<Point>();
            stoneLocations = new List<Point>();
            waterLocations = new List<Point>();
            generateGrid(mapDetails);        
        }
        private void generateGrid(string map)
        {
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

        private void setStartStats(string starter)
        {
            Console.WriteLine(starter[3]);
            playerNum = starter[3];
            string loc = starter.Split(':')[2];
            startLoc = new Point(int.Parse(loc.Split(',')[0]), int.Parse(loc.Split(',')[1]));
            starDir = int.Parse(starter.Split(':')[3]);
            Console.WriteLine(playerNum + " " + startLoc + " " + starDir);
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
    }
}
