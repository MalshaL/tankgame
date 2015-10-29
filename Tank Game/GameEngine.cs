﻿using System;
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

        private MyPlayer me;
        private string playerName;      //player name (client)
        private int playerNum;          //player number
        private Point startLoc;         //start location of player
        private int starDir;            //start direction of player
        private GridEntity[,] grid;     //the grid
        private int mapSize;            //no of rows and columns in the grid
        private List<Player> playerList;
        //private List<GridEntity> brickLocations;
        //private List<GridEntity> stoneLocations;
        //private List<GridEntity> waterLocations;
        //private List<GridEntity> coinLocations;
        private List<char> msgTypes;
        bool isFirstDecode = true;

        #endregion

        public GameEngine()             //
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
            char firstChar = message[0];
            //if (message.ElementAt(message.Length - 1) == '?')
            //{
            //    message = message.Substring(0, message.Length - 2);
            //    Console.WriteLine("????????hereeee");
            //}
            //else
            //{
            //    message = message.Substring(0, message.Length - 1);
            //    Console.WriteLine("########hereeeeeeee");
            //}
            message = message.Substring(0, message.Length - 2);
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
                    handleCoins(message, grid);
                }
                if (firstChar == 'L')
                {
                    handleLifePacks(message, grid);
                }
            }
            else
            {

            }
        }

        private void initialize(string starter)
        {
            playerList = new List<Player>();
            //playerName = starter.Substring(2, 4);
            starter = starter.Substring(2);
            string[] tokens = starter.Split(';');
            playerName = tokens[0];
            playerNum = int.Parse(playerName.Substring(1));
            string loc = tokens[1];
            startLoc = new Point(int.Parse(loc.Split(',')[0]), int.Parse(loc.Split(',')[1]));
            starDir = int.Parse(tokens[2]);
            me = new MyPlayer(startLoc, playerName, starDir);
            Console.WriteLine(me.getName() + " " + me.getCurrentP() + " " + me.getDirection());
        }

        private void generateGrid(string map)
        {
            this.mapSize = 10;
            //brickLocations = new List<Brick>();
            //stoneLocations = new List<Stone>();
            //waterLocations = new List<Water>();
            //coinLocations = new List<CoinPile>();
            grid = new GridEntity[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    grid[i, j] = new GridEntity(new Point(j, i));
                }
                //Console.WriteLine();
            }
            setLocations(map, grid);
            displayGrid(grid);
        }

        private void setLocations(string map, GridEntity[,] grid)
        {
            string[] splittedValues = map.Split(':');
            setLocationLists(splittedValues[2], "brick", grid);
            setLocationLists(splittedValues[3], "stone", grid);
            setLocationLists(splittedValues[4], "water", grid);
        }

        private void setLocationLists(string values, string type, GridEntity[,] grid)
        {
            Point p;
            string[] tokens = values.Split(';');
            for (int i = 0; i < tokens.Length; i++)
            {
                p = new Point(int.Parse(tokens[i].Split(',')[0]), int.Parse(tokens[i].Split(',')[1]));
                try
                {
                    if (type.Equals("brick"))
                    {
                        //Brick b = new Brick(p);
                        grid[p.Y, p.X] = new Brick(p);
                        //locations.Add(new Brick(p));
                    }
                    else if (type.Equals("stone"))
                    {
                        grid[p.Y, p.X] = new Stone(p);
                        //Stone b = new Stone(p);
                        //locations.Add(new Stone(p));
                    }
                    else if (type.Equals("water"))
                    {
                        grid[p.Y, p.X] = new Water(p);
                        //Water b = new Water(p);
                        //locations.Add(new Water(p));
                    }
                    
                }
                catch (NullReferenceException)
                {
                    //Console.WriteLine("exception");
                }

            }
        }

        private void updateMap(string msg)
        {
            msg = msg.Substring(2);
            string[] splittedValues = msg.Split(':');
            if (isFirstDecode)
            {
                for (int i = 0; i < splittedValues.Length-1; i++)
                {
                    string[] tokens = splittedValues[i].Split(';');
                    Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
                    if ((i) != playerNum)
                    {
                        Player player = new Player(p, tokens[0], int.Parse(tokens[2]));
                        player.updatePlayer(player, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                        playerList.Add(player);
                        grid[p.Y, p.X] = player;
                    }
                    else
                    {
                        me.updatePlayer(me, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                        grid[p.Y, p.X] = me;
                    }
                }

            }
            else{
                for (int i = 0; i < splittedValues.Length - 1; i++)
                {
                    string[] tokens = splittedValues[i].Split(';');
                    Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
                    if ((i + 1) != playerNum)
                    {
                        playerList.ElementAt(i).updatePlayer(playerList.ElementAt(i+1), p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                        grid[p.Y, p.X] = playerList.ElementAt(i);
                    }
                    else
                    {
                        me.updatePlayer(me, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                        grid[p.Y, p.X] = me;
                    }
                }
            }
            displayGrid(grid);
        }


        public void displayGrid(GridEntity[,] grid)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Console.Write(grid[i, j].getName());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void handleCoins(string msg, GridEntity[,] grid)
        {
            string[] tokens = msg.Split(':');
            Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
            grid[p.Y, p.X] = new CoinPile(p, int.Parse(tokens[2]), 0, int.Parse(tokens[3]));
            
            //coinLocations.Add(p);
            //CoinPile coins = new CoinPile(p, int.Parse(tokens[2]), 0, int.Parse(tokens[3]));
        }

        private void handleLifePacks(string msg, GridEntity[,] grid)
        {
            string[] tokens = msg.Split(':');
            Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
            grid[p.Y, p.X] = new LifePack(p, int.Parse(tokens[2]), 0);
            //coinLocations.Add(p);
            //LifePack lifepack = new LifePack(p, int.Parse(tokens[2]), 0);
        }
    }
}
