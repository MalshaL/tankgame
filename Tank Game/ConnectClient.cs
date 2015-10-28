using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tank_Game;

namespace TankClient
{
    class ConnectClient
    {
        private NetworkStream clientStream; //Stream - outgoing
        private TcpClient client; //To talk back to the client
        private BinaryWriter writer; //To write to the clients

        private NetworkStream serverStream; //Stream - incoming        
        private TcpListener listener; //To listen to the clinets        
        public string reply = ""; //The message to be written

        private string clientmsg = null;
        private Thread clientThread = null;
        String x = Console.ReadLine();
        GameEngine gameEngine = new GameEngine();

        public void ReceiveData()
        {

            bool errorOcurred = false;

            //Socket for listning
            Socket clientAccept = null;

            try
            {
                Console.WriteLine("Client is starting");

                //create new client socket
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);

                //Starts listening
                this.listener.Start();

                //Establishing connection
                while (true)
                {
                    //connection is connected socket
                    clientAccept = listener.AcceptSocket();

                    //check for connection
                    if (clientAccept.Connected)
                    {
                        //Console.WriteLine("Client connected sussesfully");

                        //To read from socket create NetworkStream object associated with socket
                        this.serverStream = new NetworkStream(clientAccept);


                        SocketAddress ipAd = clientAccept.RemoteEndPoint.Serialize();
                        int port = 100;

                        string address = clientAccept.RemoteEndPoint.ToString();
                        List<Byte> clientInput = new List<byte>();

                        int asw = 0;
                        while (asw != -1)
                        {
                            asw = this.serverStream.ReadByte();
                            clientInput.Add((Byte)asw);
                        }

                        reply = Encoding.UTF8.GetString(clientInput.ToArray());
                        this.serverStream.Close();
                        string ip = address.Substring(0, address.IndexOf(":"));


                        try
                        {
                            string ss = reply.Substring(0, reply.IndexOf(";"));
                            port = Convert.ToInt32(ss);
                        }

                        catch (Exception)
                        {
                            port = 100;
                        }
                        Console.WriteLine(ip + ": " + reply);
                        gameEngine.handleMessage(reply);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! \n " + e.Message);
                errorOcurred = true;
            }
            finally
            {
                if (clientAccept != null)
                    if (clientAccept.Connected)
                        clientAccept.Close();
                if (errorOcurred)
                    this.ReceiveData();
            }
        }



        public void SendData()
        {

            //Opening the connection
            this.client = new TcpClient();
            /*   if (System.Console.KeyAvailable) { 
               Object t = System.Console.ReadKey(true).Key;
               String msg = "";
               if (t.Equals(ConsoleKey.UpArrow))
               {
                    msg = "UP#";
               }
               else if (t.Equals(ConsoleKey.DownArrow))
               {
                    msg = "DOWN#";
               }
               else if (t.Equals(ConsoleKey.LeftArrow))
               {
                   msg = "LEFT#";
               }
               else if (t.Equals(ConsoleKey.RightArrow))
               {
                   msg = "RIGHT#";
               }
               Console.WriteLine(msg);
               if (this.client.Connected)
               {
                   //To write to the socket
                   this.clientStream = client.GetStream();

                   //Create objects for writing across stream
                   this.writer = new BinaryWriter(clientStream);
                   Byte[] tempStr = Encoding.ASCII.GetBytes(x);

                   //writing to the port                
                   this.writer.Write(tempStr);
                   Console.WriteLine(msg);
                   this.writer.Close();
                   this.clientStream.Close();
               }
               }*/

            try
            {


                this.client.Connect(IPAddress.Parse("127.0.0.1"), 6000);

                if (this.client.Connected)
                {
                    //To write to the socket
                    this.clientStream = client.GetStream();

                    //Create objects for writing across stream
                    this.writer = new BinaryWriter(clientStream);
                    Byte[] tempStr = Encoding.ASCII.GetBytes(x);

                    //writing to the port                
                    this.writer.Write(tempStr);
                    Console.WriteLine("\t Data: " + x + " is written to " + IPAddress.Parse("127.0.0.1") + " on " + 6000);
                    this.writer.Close();
                    this.clientStream.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (WRITING) to " + IPAddress.Parse("127.0.0.1") + " on " + 6000 + "Failed! \n " + e.Message);
            }
            finally
            {
                this.client.Close();
            }
        }




        public void recivePool()
        {
            clientThread = new Thread(new ThreadStart(ReceiveData));
            clientThread.Start();
        }
        public void sendPool()
        {

            clientThread = new Thread(new ThreadStart(SendData));
            clientThread.Start();
        }
    }
}

