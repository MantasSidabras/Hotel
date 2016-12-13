using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Menu
    {
        HotelDbEntities _context;
        Reservation _reservation;
        public Menu(HotelDbEntities context, Reservation reservation)
        {
            _context = context;
            _reservation = reservation;
        }

        public void DisplayAvailableRooms()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Available Rooms: ");
            foreach(var room in _context.Rooms)
            {
                Console.WriteLine($"number: {room.Nr} size: {room.Size}");
            }
            Console.WriteLine("-------------------------------------------------");
        }
            


        public void DisplayOptions()
        {
            Console.WriteLine("1. Choose room for reservation");
            Console.WriteLine("2. Add guest");
            Console.WriteLine("3. ");
            Console.WriteLine("4. Select Date");
            Console.WriteLine("5. Confirm reservation");
            Console.WriteLine("8. Display all rooms");
            Console.WriteLine("11. EXIT");
        }

        public void DisplayCurrentSetup(Room room)
        {
            if(room.Nr != 0)
            {
                Console.WriteLine("Currently selected room is " + room.Nr);
            }
        }

        public void DisplayAllRooms()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("All Rooms: ");
            foreach (var room in _context.Rooms)
            {
                Console.Write($"number: {room.Nr} size: {room.Size} ");
                if(room.Taken_from != null)
                {
                    Console.Write($"taken from: {room.Taken_from} taken until: {room.Taken_until}\n");
                }
                else
                {
                    Console.Write("\n");
                }
            }
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
