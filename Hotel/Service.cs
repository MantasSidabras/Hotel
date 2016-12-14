using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Service
    {
        SqlConnection _connection;
        public Service(SqlConnection connection)
        {
            _connection = connection;
        }

        public void AddAmenity(Room room, string name, double price)
        {
            try
            {
                _connection.Open();

                SqlCommand insert = new SqlCommand();
                insert.Connection = _connection;
                insert.CommandType = CommandType.Text;
                insert.CommandText = "INSERT INTO Amenity (Name, Price, RoomId) VALUES(@N, @P, @R)";
                insert.Parameters.Add(new SqlParameter("@N", SqlDbType.NVarChar, 50, "Name"));
                insert.Parameters.Add(new SqlParameter("@P", SqlDbType.Float, 50, "Price"));
                insert.Parameters.Add(new SqlParameter("@R", SqlDbType.Int, 50, "RoomId"));

                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Name, Price, RoomId FROM Amenity", _connection);
                da.InsertCommand = insert;
                DataSet ds = new DataSet();
                da.Fill(ds, "Amenity");
                DataRow row = ds.Tables["Amenity"].NewRow();
                row["Name"] = name;
                row["Price"] = price;
                row["RoomId"] = room.Id;

                ds.Tables["Amenity"].Rows.Add(row);

                da.Update(ds.Tables["Amenity"]);

                da.Dispose();
                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to insert Amenity");
            }
        }

        public void DeleteAmenity(Room room, string name)
        {
            try
            {
                _connection.Open();
                SqlCommand delete = new SqlCommand();
                delete.Connection = _connection;
                delete.CommandType = CommandType.Text;
                delete.CommandText = "Delete FROM Amenity Where Id = @Q AND Name = @W";

                delete.Parameters.Add(new SqlParameter("@Q", SqlDbType.Int, 50, "Id"));
                delete.Parameters.Add(new SqlParameter("@W", SqlDbType.NVarChar, 50, "Name"));

                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Name, Price, RoomId From Amenity", _connection);
                da.DeleteCommand = delete;

                DataSet ds = new DataSet();
                da.Fill(ds, "Amenity");

                ds.Tables["Amenity"].PrimaryKey = new[] { ds.Tables["Amenity"].Columns["Name"],
                                                          ds.Tables["Amenity"].Columns["RoomId"] };
                ds.Tables["Amenity"].Rows.Find(new object[] { name, room.Id }).Delete();
                da.Update(ds.Tables["Amenity"]);

                da.Dispose();
                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to delete amenity");
            }
        }

        public void UpdateAmenity(Room room, string name, Room newRoom)
        {
            using (_connection)
            {
                try
                {
                    SqlCommand update = new SqlCommand();
                    update.Connection = _connection;
                    update.CommandType = CommandType.Text;
                    update.CommandText = "UPDATE Amenity SET RoomId = @R WHERE Name = @N AND RoomId = @RO";

                    update.Parameters.Add(new SqlParameter("@N", SqlDbType.NVarChar, 50, "Name"));
                    update.Parameters.Add(new SqlParameter("@R", SqlDbType.Int, 50, "RoomId"));
                    update.Parameters.Add(new SqlParameter("@RO", SqlDbType.Int, 50, "RoomIdOld"));

                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Name, Price, RoomId From Amenity", _connection);
                    da.DeleteCommand = update;

                    DataSet ds = new DataSet();
                    da.Fill(ds, "Amenity");
                    ds.Tables["Amenity"].Rows[0]["Name"] = name;
                    ds.Tables["Amenity"].Rows[0]["RoomIdOld"] = room.Id;
                    ds.Tables["Amenity"].Rows[0]["RoomId"] = newRoom.Id;
                   

                    da.Update(ds.Tables["Amenity"]);

                    da.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update amenity");
                }
            }
        }

        private DataSet SelectAmenity(Room room, string name)
        {
            _connection.Open();
            SqlCommand select = new SqlCommand("");
            select.Connection = _connection;
            select.CommandType = CommandType.Text;
            select.CommandText = "SELECT Id, Name, Price FROM Amenity Where Id = @Q AND Name = @W";

            select.Parameters.Add(new SqlParameter("@Q", SqlDbType.NVarChar, 50, "Name"));
            select.Parameters.Add(new SqlParameter("@W", SqlDbType.Float, 50, "Price"));

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.Fill(ds, "Amenity");
            da.Dispose();

            _connection.Close();
            return ds;
        }
    }
}

