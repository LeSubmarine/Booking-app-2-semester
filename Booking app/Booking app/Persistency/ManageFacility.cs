using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_app.Model;
using Booking_app.Persistency.Interfaces;

namespace Booking_app.Persistency
{
    public class ManageFacility : IManageFacility
    {
        string _connectionString = "Server = tcp:bookingapp2semester.database.windows.net,1433;Initial Catalog = BookingAppDB; Persist Security Info=False;User ID = hmt; Password=!henrik1mike1tobias!; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        string ConnectionString
        {
            get => _connectionString;
        }

        public int ExecuteNonQueryFacility(string queryString)
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

        public bool Create(Facility facility)
        {
            //Insert into Hotel Values(xx, yy, zz, ...)
            //throw new NotImplementedException();

            int rowsAffected = 0;


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string queryString = $"INSERT INTO Facility VALUES({facility.FacilityNo}, {facility.Floor}, {facility.Size})";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }

            return (rowsAffected == 1);
        }

        public Facility Delete(int facilityNo)
        {
            Facility facility = Get(facilityNo);
            ExecuteNonQueryFacility($"DELETE FROM Facility WHERE FacilityNo={facilityNo}");
            return facility;
        }

        public List<Facility> Get()
        {
            List<Facility> facilityList = new List<Facility>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = "select * from Facility";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Facility curFacility = new Facility();

                    curFacility.FacilityNo = reader.GetInt32(0); //læser int fra første søjle
                    curFacility.Floor = reader.GetInt32(1); //læser int fra anden søjle
                    curFacility.Size = reader.GetInt32(2); //læser int fra tredje søjle

                    facilityList.Add(curFacility);
                }
            }

            return facilityList;
        }

        public Facility Get(int facilityNo)
        {
            Facility facility = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = $"select * from Facility where FacilityNo = {facilityNo}";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    facility = new Facility();

                    facility.FacilityNo = reader.GetInt32(0); //læser int fra første søjle
                    facility.Floor = reader.GetInt32(1); //læser int fra anden søjle
                    facility.Size = reader.GetInt32(2); //læser int fra tredje søjle

                }
            }

            return facility;
        }

        public bool Update(Facility facility, int facilityNo)
        {
            int rowsAffected = ExecuteNonQueryFacility($"UPDATE Facility SET Floor='{facility.Floor}', Size='{facility.Size}' WHERE FacilityNo={facilityNo}");
            return (rowsAffected == 1);
        }
    }
}
