﻿using TriviaApp.Pages;

namespace TriviaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

           
        }
    }
}
