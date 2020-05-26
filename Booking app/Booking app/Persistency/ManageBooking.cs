using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_app.Model;

namespace Booking_app.Persistency
{
    public class ManageBooking
    {
        string _connectionString = "Server = tcp:bookingapp2semester.database.windows.net,1433;Initial Catalog = BookingAppDB; Persist Security Info=False;User ID = hmt; Password=!henrik1mike1tobias!; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        string ConnectionString
        {
            get => _connectionString;
        }

        public int ExecuteNonQueryBooking(string queryString)
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

        public bool Create(Booking booking)
        {

            int rowsAffected = 0;
                

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string queryString = $"INSERT INTO Booking VALUES({booking.BookingNo}, {booking.FacilityNo}, '{booking.Email}', @value)";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@value", booking.Date);

                command.Connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }

            return (rowsAffected == 1);
        }

        public Booking Delete(int bookingNo)
        {
            Booking booking = Get(bookingNo);
            ExecuteNonQueryBooking($"DELETE FROM Booking WHERE BookingNo={bookingNo}");
            return booking;
        }

        public List<Booking> Get()
        {
            List<Booking> bookingList = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = "select * from Booking";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Booking curBooking = new Booking();

                    curBooking.BookingNo = reader.GetInt32(0); //læser int fra første søjle
                    curBooking.FacilityNo = reader.GetInt32(1); //læser int fra anden søjle
                    curBooking.Email = reader.GetString(2); //læser int fra tredje søjle
                    curBooking.Date = reader.GetDateTime(3); //læser datetime fra fjerde søjle

                    bookingList.Add(curBooking);
                }
            }

            return bookingList;
        }

        public Booking Get(int bookingNo)
        {
            Booking booking = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = $"select * from Booking where BookingNo = {bookingNo}";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    booking = new Booking();

                    booking.BookingNo = reader.GetInt32(0); //læser int fra første søjle
                    booking.FacilityNo = reader.GetInt32(1); //læser int fra anden søjle
                    booking.Email = reader.GetString(2); //læser int fra tredje søjle
                    booking.Date = reader.GetDateTime(3); //læser datetime fra fjerde søjle

                }
            }

            return booking;
        }

        public bool Update(Booking booking, int bookingNo)
        {
            int rowsAffected = ExecuteNonQueryBooking($"UPDATE Booking SET Floor='{booking.FacilityNo}', Size='{booking.Email}', Size='{booking.Date}' WHERE BookingNo={bookingNo}");
            return (rowsAffected == 1);
        }
    }
}