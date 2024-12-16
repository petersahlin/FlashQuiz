using System.Diagnostics;
using TriviaApp.ViewModels;

namespace TriviaApp.Pages;

public partial class PlayPage : ContentPage
{
	public PlayPage(PlayPageViewModel viewModel)
	{
		Debug.WriteLine("!!!!!!!!!PLAYPAGE CONSTRUCTOR THINGY ENTERED");

		InitializeComponent();

		BindingContext = viewModel;
	}	
}