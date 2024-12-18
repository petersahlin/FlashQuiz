using TriviaApp.ViewModels;

namespace TriviaApp.Pages;

public partial class PlayPage : ContentPage
{
	public PlayPage(PlayPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}	
}