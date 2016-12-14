using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Reservation : IReservation
    {
        HotelDbEntities _context;
        public Reservation(HotelDbEntities context)
        {
            _context = context;
        }

        public bool ReserveRoom(List<Person> guests, Room room, DateTime start, DateTime end)
        {

            if (GetAvailableRooms(start, end).Contains(room))
            {

                foreach (var guest in guests)
                {
                    room.Guests.Add(new Guest()
                    {
                        FirstName = guest.FirstName,
                        LastName = guest.LastName,
                        PersonalCode = guest.PersonalCode
                    });
                    room.Taken_from = start;
                    room.Taken_until = end;
                }
                room.TimesUsed += 1;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddGuest(string firstName, string lastName, string personalCode)
        {
            var guest = new Guest()
            {
                FirstName = firstName,
                LastName = lastName,
                PersonalCode = personalCode
            };
            _context.Guests.Add(guest);
            _context.SaveChanges();
        }

        public List<Room> GetAvailableRooms(DateTime fromDate, DateTime ToDate)
        {
            var availableRooms = (from r in _context.Rooms
                                  where r.Guests.Count == 0
                                  && r.State == "clean"
                                  //&& ((r.Taken_from == null && r.Taken_until == null)
                                  //|| !(fromDate <= r.Taken_until && r.Taken_from >= ToDate))  // No overlapping dates

                                  select r).ToList();

            return availableRooms;
        }

        public void RemoveGuests(int nr)
        {
            
            var room = (from r in _context.Rooms
                      where r.Nr == nr
                      select r).FirstOrDefault();

            room.Taken_from = null;
            room.Taken_until = null;

            foreach (Guest g in room.Guests.ToList())
            {
                _context.Guests.Remove(g);
            }
            _context.SaveChanges();
        }
    
        public void UpdateRoom(int nr, double price)
        {
            var room = (from r in _context.Rooms
                        where r.Nr == nr
                        select r).FirstOrDefault();

            room.Price_for_night = price;
            _context.SaveChanges();
        }

        public Guest SearchGuest(string searchKey)
        {
            foreach(Guest g in _context.Guests)
            {
                if(g.FirstName.Contains(searchKey) || g.LastName.Contains(searchKey))
                {
                    return g;
                }
            }
            return null;
        }
    }
}
