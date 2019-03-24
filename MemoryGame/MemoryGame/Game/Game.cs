using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MemoryGame.Game
{
    public class Game
    {
        private bool clicked = false;
        private Grid _grid;

        public Game(Grid grid)
        {
            _grid = grid;
        }

        public void InitButtons()
        {
            Grid gb = new Grid();
            List<int> list = new List<int>();
            Random random = new Random(new DateTime().Millisecond);

            list.AddRange(Enumerable.Range(1, 12));

            foreach (var item in _grid.Children)
            {
                var btn = item as GButton;
                var rnd = random.Next(0, list.Count);
                var btNr = list[rnd] > 6 ? list[rnd] - 6 : list[rnd];
                list.RemoveAt(rnd);
                btn.Click = false;
                btn.CommandParameter = btNr;
                btn.Text = "";
            }
        }

        public async void Button_OnClicked(object sender, EventArgs e)
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

        private async Task Sleep(int msec)
        {
            await Task.Delay(msec);
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
            _grid.Children.ForEach(x => gButtons.Add(x as GButton));

            return gButtons;
        }

        /// <summary>
        ///     Block or unblock all buttons
        /// </summary>
        /// <param name="blocked"></param>
        private void BlockAllButtons(bool blocked)
        {
            if (blocked == true)
                GetAllButtons().ForEach(x => x.IsEnabled = false);
            else
                GetAllButtons().ForEach(x => x.IsEnabled = true);
        }
    }
}