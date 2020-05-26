using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_app.Model;

namespace Booking_app.Persistency
{
    public class ManageZealandUser
    {
        string _connectionString = "Server = tcp:bookingapp2semester.database.windows.net,1433;Initial Catalog = BookingAppDB; Persist Security Info=False;User ID = hmt; Password=!henrik1mike1tobias!; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        string ConnectionString
        {
            get => _connectionString;
        }

        public int ExecuteNonQueryZealandUser(string queryString)
        {
            int rowsAffected = (-1);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                command.Connection.Close();
            }
            return rowsAffected;
        }

        public bool Create(ZealandUser zealandUser)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string queryString = $"INSERT INTO ZealandUser VALUES('{zealandUser.Email}', '{zealandUser.Name}', '{zealandUser.Password}', '{zealandUser.School}')";
                rowsAffected = ExecuteNonQueryZealandUser(queryString);
            }

            return (rowsAffected == 1);
        }

        public ZealandUser Delete(string email)
        {
            ZealandUser zealandUser = Get(email);
            ExecuteNonQueryZealandUser($"DELETE FROM ZealandUser WHERE Email='{email}'");
            return zealandUser;
        }

        public List<ZealandUser> Get()
        {
            List<ZealandUser> zealandUserList = new List<ZealandUser>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = "select * from ZealandUser";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ZealandUser curZealandUser = new ZealandUser();

                    curZealandUser.Email = reader.GetString(0); //læser string fra første søjle
                    curZealandUser.Name = reader.GetString(1); //læser string fra anden søjle
                    curZealandUser.Password = reader.GetString(2); //læser string fra tredje søjle
                    curZealandUser.School = reader.GetString(3); //læser string fra fjerde søjle

                    zealandUserList.Add(curZealandUser);
                }
            }

            return zealandUserList;
        }

        public ZealandUser Get(string email)
        {
            ZealandUser zealandUser = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = $"select * from ZealandUser where Email = '{email}'";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    zealandUser = new ZealandUser();

                    zealandUser.Email = reader.GetString(0); //læser string fra første søjle
                    zealandUser.Name = reader.GetString(1); //læser string fra anden søjle
                    zealandUser.Password = reader.GetString(2); //læser string fra tredje søjle
                    zealandUser.School = reader.GetString(3); //læser string fra fjerde søjle
                }
            }

            return zealandUser;
        }

        public bool Update(ZealandUser zealandUser, string email)
        {
            int rowsAffected = ExecuteNonQueryZealandUser($"UPDATE ZealandUser SET Name='{zealandUser.Name}', Password='{zealandUser.Password}', School='{zealandUser.School}' WHERE Email='{email}'");
            return (rowsAffected == 1);
        }
    }
}
