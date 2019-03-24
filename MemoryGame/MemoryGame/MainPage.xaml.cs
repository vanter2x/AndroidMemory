using System;
using Xamarin.Forms;

namespace MemoryGame
{
    public partial class MainPage : ContentPage
    {
        private Game.Game game;

        public MainPage()
        {
            InitializeComponent();
            game = new Game.Game(GridButtons);
            game.InitButtons();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            game.Button_OnClicked(sender, e);
        }
    }
}
