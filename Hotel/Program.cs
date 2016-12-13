using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Program
    {
        const string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\MANTAS\documents\visual studio 2015\Projects\Hotel\Hotel\HotelDb.mdf';Integrated Security = True";

        static void Main(string[] args)
        {
            HotelDbEntities context = new HotelDbEntities();
            Reservation reservation = new Reservation(context);
            Menu menu = new Menu(context, reservation);

            var guests = new List<Person>();

            //reservation.ReserveRoom(guests, context.Rooms.FirstOrDefault(), DateTime.Now, DateTime.Now.AddDays(1));
            int input;
            Room room = new Room();
            DateTime CheckInDate = DateTime.MinValue;
            DateTime CheckOutDate = DateTime.MinValue;

            do
            {
                menu.DisplayCurrentSetup(room);
                menu.DisplayAvailableRooms();

                menu.DisplayOptions();

                int.TryParse(Console.ReadLine(), out input);



                switch (input)
                {
                    case 1:
                        Console.WriteLine("Select available room");
                        int roomNumber;
                        int.TryParse(Console.ReadLine(), out roomNumber);
                        if (roomNumber != 0)
                        {
                            room = context.Rooms.Where(x => x.Nr == roomNumber).Select(x => x).FirstOrDefault();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter first name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter last name:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Enter personal code:");
                        string personalCode = Console.ReadLine();
                        guests.Add(new Person() { FirstName = firstName, LastName = lastName, PersonalCode = personalCode });
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        Console.WriteLine("Check in date: ");
                        DateTime.TryParse(Console.ReadLine(), out CheckInDate);
                        Console.WriteLine("Check out date: ");
                        DateTime.TryParse(Console.ReadLine(), out CheckOutDate);
                        break;

                       
                    case 5:
                        if (CheckInDate != DateTime.MinValue && guests.Capacity != 0)
                        {
                            var reservationConfirmed = reservation.ReserveRoom(guests, context.Rooms.FirstOrDefault(), CheckInDate, CheckOutDate);
                        }
                        else
                        {
                            Console.WriteLine("Failed to register. Make sure to fill all fields.");
                        }
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    default:
                    case 8:
                        Console.Clear();
                        menu.DisplayAllRooms();
                        break;

                }

            } while (input != 11);

        }



        private static void ReadDb()
        {
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, FirstName, LastName, PersonalCode, CheckIn, CheckOut, RoomId FROM Guest", connection);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Guest");

            connection.Close();
            Console.WriteLine(ds.Tables["Guest"].Rows[0]["RoomId"]);
        }
    }
}
