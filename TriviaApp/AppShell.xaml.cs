using TriviaApp.Pages;

namespace TriviaApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TriviaPage), typeof(TriviaPage));
            Routing.RegisterRoute(nameof(EndPage), typeof(EndPage));
            Routing.RegisterRoute(nameof(PlayPage), typeof(PlayPage));


        }
    }
}
