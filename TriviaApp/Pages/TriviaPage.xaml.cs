using System.Diagnostics;
using TriviaApp.Services;
using TriviaApp.ViewModels;

namespace TriviaApp.Pages;

public partial class TriviaPage : ContentPage
{
    public TriviaPage(TriviaViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

   
}