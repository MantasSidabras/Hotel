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
            Console.WriteLine("3. Manage amenities");
            Console.WriteLine("4. Select Date");
            Console.WriteLine("5. Confirm reservation");
            Console.WriteLine("6. Cancel reservation");
            Console.WriteLine("8. Display all rooms");
            Console.WriteLine("9. View all guests");
            Console.WriteLine("11. EXIT");
        }

        public void DisplayAmenitiesOptions()
        {
            Console.WriteLine("1. Add amenity");
            Console.WriteLine("2. Remove amenity");
            Console.WriteLine("4. Update amenity");
        }

        public void DisplayCurrentSetup(Room room, List<Person> guests, DateTime start, DateTime end)
        {
            Console.WriteLine("-------------------------------------------------");
            if(start != DateTime.MinValue && end != DateTime.MinValue)
            {
                Console.WriteLine($"from {start} to {end}");
            }
            if (room.Nr != 0)
            {
                Console.WriteLine("Currently selected room is " + room.Nr);
                double cost = room.Price_for_night * guests.Count + room.Amenities.Sum(x => x.Price).Value;

                Console.WriteLine("Current price: " + cost);
            }
            if(guests.Count != 0)
            {
                Console.WriteLine("Guests: ");
            }
            foreach(var guest in guests)
            {
                Console.WriteLine($"{guest.FirstName} {guest.LastName} ");
            }

            Console.WriteLine("-------------------------------------------------");
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

        internal void ViewAllGuests()
        {
            Console.WriteLine("List of all guests: ");
            foreach (var guest in _context.Guests)
            {
                Console.WriteLine($"{guest.FirstName} {guest.LastName} ");
            }
        }
    }
}
