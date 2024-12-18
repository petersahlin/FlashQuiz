using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.Pages;

namespace TriviaApp.ViewModels
{
    public partial class PlayPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _questionAmount; 

        public PlayPageViewModel()
        {
           
        }

        [RelayCommand]
        private async Task PlayTrivia()
        {
            // make sure input is a valid positive number
            if (!int.TryParse(QuestionAmount, out int amount) || amount <= 0)
            {
                await Shell.Current.DisplayAlert("Invalid Input", "Please enter a valid number of questions greater than 0.", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(TriviaPage)}?questionAmount={amount}");
        }

    }
}
