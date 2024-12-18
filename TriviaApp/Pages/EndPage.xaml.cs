using System.Diagnostics;
using TriviaApp.ViewModels;

namespace TriviaApp.Pages;

[QueryProperty(nameof(ViewModel), "viewModel")]
public partial class EndPage : ContentPage
{
    private TriviaViewModel _viewModel;

    public TriviaViewModel ViewModel
    {
        get => _viewModel;
        set
        {
            _viewModel = value;
            BindingContext = _viewModel;
        }
    }

    public EndPage()
    {
        InitializeComponent();
    }
}


