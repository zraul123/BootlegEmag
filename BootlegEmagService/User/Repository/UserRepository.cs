using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;
using BootlegEmagService.Models;

namespace BootlegEmagService.User.Repository
{
    public class UserRepository
    {
        private SQLiteConnection _connection;

        public UserRepository()
        {
            OpenDBConnection();
        }

        private void OpenDBConnection()
        {
            if(_connection == null)
            {
                var dbPath = ConfigurationManager.ConnectionStrings["UserDBConnection"].ConnectionString;
                _connection = new SQLiteConnection(dbPath);
            }
        }

        //Login
        public Models.UserModel find(string name, string password)
        {
            int count;

            _connection.Open();

            //check if user + password combo exists
            string stm = $"SELECT count FROM user WHERE name='{name}' AND password='{password}'";
            using var check = new SQLiteCommand(stm, _connection);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {
                count = Convert.ToInt32(rdr["count"]);
                int nextCount = count + 1;
                string stm2 = $"UPDATE user SET count='{nextCount.ToString()}' WHERE name='{name}'";
                using var check2 = new SQLiteCommand(stm2, _connection);
                using SQLiteDataReader rdr1 = check2.ExecuteReader();

                string stm1 = $"SELECT role FROM user WHERE name='{name}' AND password='{password}'";
                using var check1 = new SQLiteCommand(stm1, _connection);
                using SQLiteDataReader rdr2 = check1.ExecuteReader();
                rdr2.Read();
                string role = Convert.ToString(rdr2["role"]);
                _connection.Close();

                UserModel user = new UserModel { Name = name, Password = password, Role = role, Counter = count };

                return user;
            }
            return null;
        }

        //Register
        public UserModel register(string name, string password, string role )
        {

            //establish connection
            _connection.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(_connection);

            //check if user exists
            string stm = $"SELECT * FROM user WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, _connection);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {
                return null;
                //check if role exists
            }
            else if (role == "ADMIN" || role == "SHOPPER" || role == "SELLER")
            {
                // insert user into table
                cmd.CommandText = "INSERT INTO user(name, password, role, count) VALUES(@name, @password, @role, @count)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@count", "1");
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                _connection.Close();
                UserModel user = new UserModel { Name = name, Password = password, Role = role, Counter = 0 };

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}

