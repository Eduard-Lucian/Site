using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Services
{
    public interface IHotelService
    {
        IEnumerable<Hotel> GetAllHotels();
        Hotel GetHotelById(int id);
        void CreateHotel(Hotel hotel, string destinatieText);
        void UpdateHotel(Hotel hotelModificat, string destinatieText);
        void DeleteHotel(int id);
    }
}