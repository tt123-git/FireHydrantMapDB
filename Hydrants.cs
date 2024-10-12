using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Runtime.Remoting.Messaging;
using K4os.Compression.LZ4.Internal;

namespace HydrantMap
{
    class HydrantMap
    {
        private static MySqlConnection conn;

        static void connect() 
        {
            string server = "127.0.0.1";
            string database = "firehydrantdb";
            string user = "root";
            string password = "root";
            string port = "3306";
            string sslM = "none";

            string connString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}",
                        server, port, user, password, database, sslM);

            conn = new MySqlConnection(connString);
            try
            {
                conn.Open();

                Console.WriteLine("Connection Successful");

                string query = "SELECT id,latitude,longitude FROM locations";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {
                            string id = reader.GetString(0);
                            double latitude = reader.GetDouble(1);
                            double longitude = reader.GetDouble(2);

                            Console.WriteLine($": {id},Latitude: {latitude}, Longitude: {longitude}");
                        }
                    }
                    
                }
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message + connString);
            }


        }

        static void Main(string[] args)
        {
            connect();
        }
    }
}
    
        
