using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp.Services
{
    public interface IAlertService
    {
        Task ShowAlertAsync(string title, string message, string cancel);
    }

    public class AlertService : IAlertService
    {


        public async Task ShowAlertAsync(string title, string message, string cancel)
        {
            await Shell.Current.DisplayAlert(title, message, cancel); 
        }
    }
}
