using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
namespace AppButton
{
    public class App : Application
    {
        const string rText = "rText";
        const string bText = "bText";
        const string gText = "gText";


        public App()
        {
            if (Properties.ContainsKey(rText))
            {
                RText = (String)Properties[rText];
            }
            if (Properties.ContainsKey(gText))
            {
                GText = (String)Properties[gText];
            }
            if (Properties.ContainsKey(bText))
            {
                BText = (String)Properties[bText];
            }

            MainPage = new HomePage();

        }

        public string RText { get; set; }
        public string GText { get; set; }
        public string BText { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {

            // Handle when your app sleeps
            Properties[rText] = RText;
            Properties[gText] = GText;
            Properties[bText] = BText;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }
    }
}
