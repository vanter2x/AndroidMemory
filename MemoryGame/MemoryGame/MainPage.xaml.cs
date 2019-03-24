
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Icu.Text;
using Java.Awt.Font;
using MemoryGame.Game;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MemoryGame
{
    public partial class MainPage : ContentPage
    {
        private bool clicked = false;

        public MainPage()
        {
            InitializeComponent();
            InitButtons();
        }

        private void InitButtons()
        {
            Grid gb = new Grid();
            List<int> list = new List<int>();

            list.AddRange(Enumerable.Range(1,12));

            foreach (var item in GridButtons.Children)
            {
                var bt = item as GButton;
                var rnd = new Random().Next(0, list.Count);
                var btNr = list[rnd] > 6 ? list[rnd] - 6 : list[rnd];
                list.RemoveAt(rnd);
                bt.Click = false;
                bt.CommandParameter = btNr;
                bt.Text ="";
            }
        }

        private async Task Sleep(int msec)
        {
            await Task.Delay(msec);
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            GButton btnTemp = sender as GButton;

            if (clicked == false)
            {
                btnTemp.Text = btnTemp.CommandParameter.ToString();
                btnTemp.Click = true;
                btnTemp.IsEnabled = false;
                clicked = true;
            }
            else
            {
                BlockAllButtons(true);
                btnTemp.Text = btnTemp.CommandParameter.ToString();
                btnTemp.Click = true;
                btnTemp.IsEnabled = false;
                clicked = false;
                var btns = GetClickedButton();
                await Sleep(2000);
                ButtonState(btns, CheckPair(btns));
            }
        }

        private bool CheckPair(GButton[] buttons) =>
            buttons[0].CommandParameter.ToString() == buttons[1].CommandParameter.ToString() ? true : false;
        

        private GButton[] GetClickedButton()
        {
            var gButtons = GetAllButtons().FindAll(x => x.Click == true);

            return gButtons.ToArray();
        }

        private void ButtonState(GButton[] buttons, bool theSame)
        {
            if (theSame == true)
            {
                buttons.ForEach(x => x.IsVisible = false);
                buttons.ForEach(x => x.Click = false);
                BlockAllButtons(false);
            }
            else
            {
                buttons.ForEach(x => x.Text = "");
                buttons.ForEach(x => x.IsEnabled = true);
                buttons.ForEach(x => x.Click = false);
                BlockAllButtons(false);

            }
        }

        private List<GButton> GetAllButtons()
        {
            List<GButton> gButtons = new List<GButton>();
            GridButtons.Children.ForEach(x => gButtons.Add(x as GButton));

            return gButtons;
        }

        private void BlockAllButtons(bool blocked)
        {
            if(blocked == true)
                GetAllButtons().ForEach(x => x.IsEnabled = false);
            else
                GetAllButtons().ForEach(x => x.IsEnabled = true);
        }
    }
}
