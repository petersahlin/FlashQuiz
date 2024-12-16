using System.Diagnostics;
using TriviaApp.ViewModels;

namespace TriviaApp.Pages;

//public partial class EndPage : ContentPage
//{
//    public EndPage()
//    {
//        InitializeComponent();
//    }

//    public EndPage(TriviaViewModel viewModel)
//    {
//        InitializeComponent();
//        BindingContext = viewModel;
//        Debug.WriteLine($"BindingContext is set to: {BindingContext?.GetType().Name}");
//    }
//}
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
            BindingContext = _viewModel; // Set the BindingContext when ViewModel is received.
            Debug.WriteLine($"BindingContext is set to: {BindingContext?.GetType().Name}");
        }
    }

    public EndPage()
    {
        InitializeComponent();
    }
}


