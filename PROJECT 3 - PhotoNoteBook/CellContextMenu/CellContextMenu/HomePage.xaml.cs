using System;
using SchoolOfFineArt;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace CellContextMenu
{
    public partial class HomePage : ContentPage
    {
        StackLayout loggerLayout = new StackLayout();
        int counter = 0;
        public double timerValue = 0.0;
        int value, bvalue;
        TimeSpan timeStamp;
        string[] imageUri = new string[10];
        //Defining data to access xml
        StudentBody filenames = new StudentBody();
        StudentBody filenames1 = new StudentBody();
        Student file = new Student();
        List<Student> Filelist = new List<Student>();
        SchoolViewModel xmlfile = new SchoolViewModel();
        SchoolViewModel xmlfile1 = new SchoolViewModel();
        List<Student> File = new List<Student>();

        public HomePage()
        {
            InitializeComponent();

            filenames = xmlfile.GetObjectPhotoFile();

        var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) }); //row-0
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(170) });//row-1
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });//row-2
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });//row-3
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-4
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-5
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-6
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });//row-7
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-8
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });



            this.Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0);

            // Assemble the page.
            this.Content = new StackLayout
            {



                Children =
                {
                   new ScrollView
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Content = grid
                    }
                }
            };


            grid.Children.Add(projectTitle, 0, 0);
            Grid.SetColumnSpan(projectTitle, 3);
            grid.Children.Add(image1, 0, 1);
            Grid.SetColumnSpan(image1, 3);
            grid.Children.Add(activityIndicator, 0, 2);
            Grid.SetColumnSpan(activityIndicator, 3);
            grid.Children.Add(timmerlabel, 0, 3);
            Grid.SetColumnSpan(timmerlabel, 3);
            grid.Children.Add(slider, 0, 4);
            Grid.SetColumnSpan(slider, 3);
            grid.Children.Add(switchTog, 0, 5);
            grid.Children.Add(stepper, 1, 5);
            grid.Children.Add(entryNum, 2, 5);
            grid.Children.Add(loggerLayout, 0, 6);
            Grid.SetColumnSpan(loggerLayout, 3);
            grid.Children.Add(butNav, 0, 7);
            Grid.SetColumnSpan(butNav, 3);





            timeStamp = TimeSpan.FromSeconds(1);
            Device.StartTimer(timeStamp, OnTimerTick);

        }

       

        async void OnbutNavClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CellContextMenuPage());
        }

        void OnImagePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "IsLoading")
            { activityIndicator.IsRunning = ((Image)sender).IsLoading; }
        }





        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            value = 2;
            timeStamp = TimeSpan.FromSeconds(args.NewValue);
            if (timeStamp.Seconds < 1)
            {
                timeStamp = TimeSpan.FromSeconds(1);
            }
        }


        void Entry_Completed(object sender, EventArgs e)
        {
            value = 3;
            //cast sender to access the properties of the Entry
            int val = int.Parse(entryNum.Text);
            int finalvalue = 60;
            if (val > 60)
            {
                entryNum.Text = finalvalue.ToString();
                val = int.Parse(entryNum.Text);
                DisplayAlert("Alert", "Value is more than 60, reset value to 60", "OK");

            }
            timeStamp = TimeSpan.FromSeconds(val);
            slider.Value = val;
            stepper.Value = val;
            if (timeStamp.Seconds < 1)
            {
                timeStamp = TimeSpan.FromSeconds(1);
            }

        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs args)
        {
            value = 1;
            entryNum.Text = args.NewValue.ToString();
            timeStamp = TimeSpan.FromSeconds(args.NewValue);
            if (timeStamp.Seconds < 1)
            {
                timeStamp = TimeSpan.FromSeconds(1);

            }
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            Device.StartTimer(timeStamp, OnTimerTick);
        }

        // int  newcount;


        bool OnTimerTick()
        {
            entryNum.Text = timeStamp.Seconds.ToString();
            timmerlabel.Text = "Display Time: " + timeStamp.Seconds.ToString() + " Sec";
            String imageFile = filenames.Students[counter].PhotoFilename;
            counter++;

                if (counter > (filenames.Students.Count -1))
                {
                    counter = 0;
                }



            defaultImageCall(imageFile);


            if (@switchTog.IsToggled)
            {
                return false;
            }

            if (stepper.Value > 1 && value == 1)
            {
                Device.StartTimer(timeStamp, OnTimerTick);
                return false;
            }

            if (slider.Value > 1 && value == 2)
            {
                Device.StartTimer(timeStamp, OnTimerTick);
                return false;
            }


            if (value == 3)
            {
                Device.StartTimer(timeStamp, OnTimerTick);
                return false;
            }

            return true;

        }

    
     
        void defaultImageCall(String imagephoto)
        {
            String urifile = imagephoto;
            WebRequest request = WebRequest.Create(new Uri(urifile));
            request.BeginGetResponse((IAsyncResult arg) =>
            {
                Stream stream = request.EndGetResponse(arg).GetResponseStream();
                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                { MemoryStream memStream = new MemoryStream(); stream.CopyTo(memStream); memStream.Seek(0, SeekOrigin.Begin); stream = memStream; }
                ImageSource imageSource = ImageSource.FromStream(() => stream); Device.BeginInvokeOnMainThread(() => image1.Source = imageSource);
            }, null);
        }
    }
}
