using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace AppButton
{
    public partial class HomePage : ContentPage
    {
        public int intRed = 0;
        public int intGreen = 0;
        public int intBlue = 0;
        Color color;
        IDictionary<string, object> properties = Application.Current.Properties;

        public HomePage()
        {
            InitializeComponent();
            // New code for loading previous keypad text.         

            //Device.OnPlatform(iOS: () => { Padding = new Thickness(0, 20, 0, 0); });

            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            App app = Application.Current as App;
            if (app.RText == null || app.RText == "")
            {
                app.RText = "0";
                RedSub.IsEnabled = false;
                app.GText = "0";
                GreenSub.IsEnabled = false;
                app.BText = "0";
                BlueSub.IsEnabled = false;
            }
            redLabel.Text = app.RText;
            // boxRed.Color = Color.FromRgb(System.Convert.ToInt32(red.Text), 0, 0);
            greenLabel.Text = app.GText;
            // boxGreen.Color = Color.FromRgb(0, System.Convert.ToInt32(green.Text), 0);
            blueLabel.Text = app.BText;
            //  boxBlue.Color = Color.FromRgb(0, 0, System.Convert.ToInt32(blue.Text));

        }

        public void OnButtonClicked(object sender, EventArgs e)
        {
            {
                Button btn = sender as Button;
                App app = Application.Current as App;
                RedAdd.IsEnabled = intRed < 256;
                if (btn.StyleId == "RedAdd")
                {
                    ++intRed;
                    if (intRed == 255)
                        RedAdd.IsEnabled = false;
                    app.RText = redLabel.Text;
                }

                RedSub.IsEnabled = intRed > 0;
                if (btn.StyleId == "RedSub")
                {
                    --intRed;
                    if (intRed == 0)
                        RedSub.IsEnabled = false;
                    app.RText = redLabel.Text;
                }

                GreenAdd.IsEnabled = intGreen < 256;
                if (btn.StyleId == "GreenAdd")
                {
                    ++intGreen;
                    if (intGreen == 255)
                        GreenAdd.IsEnabled = false;
                    app.GText = greenLabel.Text;
                }

                GreenSub.IsEnabled = intGreen > 0;
                if (btn.StyleId == "GreenSub")
                {
                    --intGreen;
                    if (intGreen == 0)
                        GreenSub.IsEnabled = false;
                    app.GText = greenLabel.Text;
                }

                BlueAdd.IsEnabled = intBlue < 256;
                if (btn.StyleId == "BlueAdd")
                {
                    ++intBlue;
                    if (intBlue == 255)
                        BlueAdd.IsEnabled = false;
                    app.BText = blueLabel.Text;
                }


                BlueSub.IsEnabled = intBlue > 0;
                if (btn.StyleId == "BlueSub")
                {
                    --intBlue;
                    if (intBlue == 0)
                        BlueSub.IsEnabled = false;
                    app.BText = blueLabel.Text;
                }


                redLabel.Text = (intRed).ToString();
                redLabel.BackgroundColor = Color.FromRgb(intRed, 0, 0);

                greenLabel.Text = (intGreen).ToString();
                greenLabel.BackgroundColor = Color.FromRgb(0, intGreen, 0);

                blueLabel.Text = (intBlue).ToString();
                blueLabel.BackgroundColor = Color.FromRgb(0, 0, intBlue);
                color = ColorLabel.BackgroundColor = Color.FromRgb(intRed, intGreen, intBlue);

                ColorValue.Text = "RGB Value = " + String.Format("{0:X2}-{1:X2}-{2:X2}", (int)(255 * color.R), (int)(255 * color.G), (int)(255 * color.B)) + "\n" +
                "HSL Value = " + String.Format("{0:F2}, {1:F2}, {2:F2}", color.Hue, color.Saturation, color.Luminosity);

                // Save keypad text.         


            }
        }
    }
}