using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using BootlegEmagService.Events.Model;
using Microsoft.Extensions.Configuration;

namespace BootlegEmagService.Events.Repository
{
    public class EventRepository
    {
        private SQLiteConnection _connection;

        public EventRepository()
        {
            OpenConnection();
        }

        private void OpenConnection()
        {
            if (_connection == null)
            {
                var dbPath = ConfigurationManager.ConnectionStrings["EventsDBConnection"].ConnectionString;
                _connection = new SQLiteConnection(dbPath);
            }
        }

        public void RegisterUserAction(UserActionEvent userAction)
        {
            _connection.Open();
            var command = new SQLiteCommand(_connection);
            command.CommandText = "INSERT INTO UserAction(UserId, Type) VALUES(@userId, @type)";
            command.Parameters.AddWithValue("@userId", userAction.UserId);
            command.Parameters.AddWithValue("@type", userAction.Type);
            command.Prepare();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void RegisterProductAction(ProductActionEvent productAction)
        {
            _connection.Open();
            var command = new SQLiteCommand(_connection);
            command.CommandText = "INSERT INTO ProductAction(UserId, ProductId, Type) VALUES(@userId, @productId, @type)";
            command.Parameters.AddWithValue("@userId", productAction.UserId);
            command.Parameters.AddWithValue("@userId", productAction.ProductId);
            command.Parameters.AddWithValue("@type", productAction.Type);
            command.Prepare();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public int GetLoginActionCount(string userId)
        {
            _connection.Open();
            var command = new SQLiteCommand(_connection);
            command.CommandText = "SELECT COUNT(UserId) FROM UserAction WHERE UserId = @userId AND Type = 0";
            command.Parameters.AddWithValue("@userId", userId);
            string result = (string)command.ExecuteScalar();
            return Int32.Parse(result);
        }

        public void RegisterDiscountForUser(string userId)
        {
            _connection.Open();
            var command = new SQLiteCommand(_connection);
            command.CommandText = "INSERT INTO Discounts(UserId, Discount) VALUES(@userId, @discount)";
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@discount", 20);
            command.Prepare();
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
