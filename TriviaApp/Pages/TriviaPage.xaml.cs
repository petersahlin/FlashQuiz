using System.Diagnostics;
using TriviaApp.Services;
using TriviaApp.ViewModels;

namespace TriviaApp.Pages;

public partial class TriviaPage : ContentPage
{
    public TriviaPage(TriviaViewModel viewModel)
    {
        Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!TRIVIAPAGE INITIALIZED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        InitializeComponent();

        BindingContext = viewModel;


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _ = HandleOnAppearingAsync();
    }

    private async Task HandleOnAppearingAsync()
    {
        try
        {

            // Access the ViewModel from the BindingContext
            if (BindingContext is TriviaViewModel viewModel)
            {
                Debug.WriteLine("TriviaPage BindingContext is TriviaViewModel");
                // Trigger the GetTriviaQuestionsCommand
                await viewModel.GetTriviaQuestionsCommand.ExecuteAsync(null);
            }

            // Check what BindingContext is
            Debug.WriteLine($"TriviaPage BindingContext: {BindingContext?.GetType().Name ?? "null"}");
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Error in OnAppearing: {ex.Message}");
        }
    }
}