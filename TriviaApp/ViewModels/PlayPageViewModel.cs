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
        public PlayPageViewModel()
        {
            Title = "Welcome to this Trivia app";
        }

        [RelayCommand]
        private async Task PlayTrivia()
        {
            await Shell.Current.GoToAsync($"{nameof(TriviaPage)}", true);
        }

    }
}
