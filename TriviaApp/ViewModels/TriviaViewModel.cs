using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using TriviaApp.Models;
using TriviaApp.Pages;
using TriviaApp.Services;


namespace TriviaApp.ViewModels
{
    public partial class TriviaViewModel : BaseViewModel
    {
        private readonly ITriviaService _triviaService;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private TriviaQuestion? _currentQuestion;

        [ObservableProperty]
        private int _currentQuestionIndex;

        [ObservableProperty]
        private string? _feedbackMessage;

        [ObservableProperty]
        private bool _isQuizOver;

        public int DisplayQuestionIndex => CurrentQuestionIndex + 1;

        //Partial method triggered when CurrentQuestionIndex changes
        partial void OnCurrentQuestionIndexChanged(int oldValue, int newValue)
        {
            // Notify that DisplayQuestionIndex has changed
            OnPropertyChanged(nameof(DisplayQuestionIndex));
        }



        public ObservableCollection<TriviaQuestion> QuestionsList { get; set; }
        public ObservableCollection<string> AnswersList { get; set; } = new();

        public TriviaViewModel(ITriviaService triviaService, IAlertService alertService)
        {
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!TRIVIAVIEWMODEL INITIALIZED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            _triviaService = triviaService;
            _alertService = alertService;
            QuestionsList = new ObservableCollection<TriviaQuestion>();

        }



        [RelayCommand]
        public async Task GetTriviaQuestions()
        {
            try
            {
                var fetchedQuestions = await _triviaService.GetTriviaQuestions();

                foreach (var question in fetchedQuestions)
                {
                    question.Question = WebUtility.HtmlDecode(question.Question);
                    question.CorrectAnswer = WebUtility.HtmlDecode(question.CorrectAnswer);
                    question.IncorrectAnswers = question.IncorrectAnswers
                        .Select(answer => WebUtility.HtmlDecode(answer))
                        .ToList();
                }

                QuestionsList = new ObservableCollection<TriviaQuestion>(fetchedQuestions);

                CurrentQuestionIndex = 0;
                LoadCurrentQuestion();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
                await _alertService.ShowAlertAsync("Error", "An error occured while fetching questions. Please try again.", "OK");

            }
        }

        private void LoadCurrentQuestion()
        {
            if (CurrentQuestionIndex >= QuestionsList.Count)
            {
                IsQuizOver = true;
                //FeedbackMessage = "Quiz over! Thanks for playing!";
                return;
            }

            CurrentQuestion = QuestionsList[CurrentQuestionIndex];
            Debug.WriteLine($"Current Question: {CurrentQuestion.Question}");

            //randomize answers
            var allAnswers = new List<string>(CurrentQuestion.IncorrectAnswers) { CurrentQuestion.CorrectAnswer };
            //decode html stuff

            allAnswers = allAnswers.OrderBy(_ => Guid.NewGuid()).ToList();

            Debug.WriteLine("Shuffled Answers:");
            foreach (var answer in allAnswers)
            {
                Debug.WriteLine(answer);
            }

            Debug.WriteLine("Clearing AnswersList...");
            AnswersList.Clear();


            foreach (var answer in allAnswers)
            {
                AnswersList.Add(answer);
            }

            //debugging answerlist
            // Debug final state of AnswersList
            Debug.WriteLine("Final AnswersList:");
            foreach (var answer in AnswersList)
            {
                Debug.WriteLine(answer);
            }


        }

        [RelayCommand]
        public void SubmitAnswer(string selectedAnswer)
        {
            if (selectedAnswer == CurrentQuestion.CorrectAnswer)
            {
                FeedbackMessage = "Correct!";
            }
            else
            {
                FeedbackMessage = $"Wrong :( \nThe correct answer was: {CurrentQuestion.CorrectAnswer}";
            }

            Task.Delay(3000).ContinueWith(async _ =>
            {
                FeedbackMessage = null;
                try
                {

                    if (CurrentQuestionIndex >= QuestionsList.Count - 1)
                    {
                        IsQuizOver = true;

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Shell.Current.GoToAsync(nameof(EndPage), new Dictionary<string, object>
                            {
                                { "viewModel", this }
                            });
                        });
                     
                        return;
                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex);
                }

                CurrentQuestionIndex++;

                LoadCurrentQuestion();
            });
        }

        [RelayCommand]
        private async Task PlayAgain()
        {
            // Reset the quiz state and navigate back to the trivia page.
            CurrentQuestionIndex = 0;
            LoadCurrentQuestion();
            await Shell.Current.GoToAsync($"{nameof(TriviaPage)}", true);
        }

        [RelayCommand]
        private async Task Exit()
        {
            await Shell.Current.GoToAsync($"///PlayPage", true);
        }


    }
}
