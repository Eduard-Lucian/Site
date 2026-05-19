using System.Collections.Generic;
using TravelBookingWeb.Models;
using TravelBookingWeb.ViewModels;

namespace TravelBookingWeb.Services
{
    public interface IActivitateService
    {
        IEnumerable<Activitate> GetAllActivitati();
        Activitate GetActivitateById(int id);
        ActivitateViewModel GetViewModelForCreate();
        ActivitateViewModel GetViewModelForEdit(int id);
        void CreateActivitate(ActivitateViewModel model);
        void UpdateActivitate(ActivitateViewModel model);
        void DeleteActivitate(int id);
    }
}