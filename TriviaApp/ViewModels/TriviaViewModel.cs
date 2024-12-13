using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TriviaApp.Models;
using TriviaApp.Services;

namespace TriviaApp.ViewModels
{
    public partial class TriviaViewModel : BaseViewModel
    {
        private readonly ITriviaService _triviaService;
        private readonly IAlertService _alertService;
        [ObservableProperty]
        ObservableCollection<TriviaQuestion> _questionsList = new ObservableCollection<TriviaQuestion>();

        public TriviaViewModel(ITriviaService triviaService, IAlertService alertService)
        {
            _triviaService = triviaService;
            _alertService = alertService;
            Title = "Game in Progress";
        }

        [RelayCommand]
        public async Task GetTriviaQuestions()
        {
            try
            {
                var fetchedQuestions = await _triviaService.GetTriviaQuestions();
                QuestionsList = new ObservableCollection<TriviaQuestion>(fetchedQuestions);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
                await _alertService.ShowAlertAsync("Error", "An error occured while fetching questions. Please try again.", "OK");
                //toast instead?
            }
        }


    }
}
