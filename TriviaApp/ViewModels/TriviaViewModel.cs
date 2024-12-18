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
    public partial class TriviaViewModel : BaseViewModel, IQueryAttributable
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
        public int YourScore { get; set; } = 0;
        public int QuestionAmount { get; set; } = 10;


        public ObservableCollection<TriviaQuestion> QuestionsList { get; set; }
        public ObservableCollection<string> AnswersList { get; set; } = new();

        public TriviaViewModel(ITriviaService triviaService, IAlertService alertService)
        {
            _triviaService = triviaService;
            _alertService = alertService;
            QuestionsList = new ObservableCollection<TriviaQuestion>();
        }

        //Partial method triggered when CurrentQuestionIndex changes
        partial void OnCurrentQuestionIndexChanged(int oldValue, int newValue)
        {
            OnPropertyChanged(nameof(DisplayQuestionIndex));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("questionAmount", out var amount))
            {
                if (int.TryParse(amount.ToString(), out int parsedAmount))
                {
                    QuestionAmount = parsedAmount;
                }
                else
                {
                    QuestionAmount = 10;
                }
            }
            GetTriviaQuestionsCommand.Execute(null);
        }

        [RelayCommand]
        public async Task GetTriviaQuestions()
        {
            YourScore = 0;
            try
            {
                var fetchedQuestions = await _triviaService.GetTriviaQuestions(QuestionAmount);

                if (fetchedQuestions == null || fetchedQuestions.Count == 0)
                {
                    Debug.WriteLine("No questions returned from the trivia service.");
                    await _alertService.ShowAlertAsync("Error", "No questions available. Please try again.", "OK");
                    return;
                }

                //html formatting
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
            CurrentQuestion = QuestionsList[CurrentQuestionIndex];

            //randomize answers
            var allAnswers = new List<string>(CurrentQuestion.IncorrectAnswers) { CurrentQuestion.CorrectAnswer };

            allAnswers = allAnswers.OrderBy(_ => Guid.NewGuid()).ToList();

            AnswersList.Clear();
            foreach (var answer in allAnswers)
            {
                AnswersList.Add(answer);
            }
        }

        [RelayCommand]
        public void SubmitAnswer(string selectedAnswer)
        {
            if (selectedAnswer == CurrentQuestion.CorrectAnswer)
            {
                FeedbackMessage = "Correct!";
                YourScore++;
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
            // Reset the quiz index and navigate back to the trivia page.
            CurrentQuestionIndex = 0;
         
            await Shell.Current.GoToAsync($"{nameof(TriviaPage)}?questionAmount={QuestionAmount}", true);
        }

        [RelayCommand]
        private async Task Exit()
        {
            QuestionAmount = 0;
            await Shell.Current.GoToAsync($"///PlayPage", true);
        }


    }
}
