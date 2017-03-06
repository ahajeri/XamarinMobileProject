using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PictureFrame
{
    public partial class MainPage : ContentPage
    {
        Button addButton, removeButton;
        StackLayout loggerLayout = new StackLayout();
        int counter = 0;
        int sliderCount = 0;
        string resourceIDimage1 = "PictureFrame.Images.IMG_0722_512.jpg";
        public double timerValue = 0.0;
        int value, bvalue;
        TimeSpan timeStamp;
        string[] imageUri = new string[10];
        Uri uri, uri1, uri2, uri3,uri4;
        int iCount = 0;


        public MainPage()
        {
            InitializeComponent();
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) }); //row-0
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });//row-1
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });//row-2
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(170) });//row-3
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-4
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-5
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-6
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-7
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-8
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-9
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-10
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-11
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });//row-12
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });


            addButton = new Button
            {
                Text = "Add",
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 30
            };


            addButton.Clicked += OnButtonClicked;

            removeButton = new Button
            {
                Text = "Remove",
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 30,
                IsEnabled = false
            };
            removeButton.Clicked += OnButtonClicked;

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
            grid.Children.Add(entryImage, 0, 1);
            Grid.SetColumnSpan(entryImage, 3);
            grid.Children.Add(addButton, 0, 2);
            Grid.SetColumnSpan(addButton, 2);
            grid.Children.Add(removeButton, 2, 2);
            grid.Children.Add(image1, 0, 3);
            Grid.SetColumnSpan(image1, 3);
            grid.Children.Add(activityIndicator, 0, 4);
            Grid.SetColumnSpan(activityIndicator, 3);
            grid.Children.Add(timmerlabel, 0, 5);
            Grid.SetColumnSpan(timmerlabel, 3);
            grid.Children.Add(slider, 0, 6);
            Grid.SetColumnSpan(slider, 3);
            grid.Children.Add(switchTog, 0, 7);
            grid.Children.Add(stepper, 1, 7);
            grid.Children.Add(entryNum, 2, 7);

           
            grid.Children.Add(imagelabel1, 0, 8);
            Grid.SetColumnSpan(imagelabel1, 3);

            
            grid.Children.Add(imagelabel2, 0, 9);
            Grid.SetColumnSpan(imagelabel2, 3);

         
            grid.Children.Add(imagelabel3, 0, 10);
            Grid.SetColumnSpan(imagelabel3, 3);

         
            grid.Children.Add(imagelabel4, 0, 11);
            Grid.SetColumnSpan(imagelabel4, 3);


            grid.Children.Add(loggerLayout, 0, 12);
            Grid.SetColumnSpan(loggerLayout, 3);

            



            timeStamp = TimeSpan.FromSeconds(1);
            Device.StartTimer(timeStamp, OnTimerTick);


            imagelabel1.Text = "1: Image From FromStream = " + "PictureFrame.Images.image1.jpg";
            imagelabel2.Text = "2: Image From Uri = " + "https://developer.xamarin.com/demo/IMG_0925.JPG?width=512";
            imagelabel3.Text = "3: Image From File = " + "image6.jpe";
            imagelabel4.Text = "4: Image From Device.OnPlatform = " + "purple.jpg";

        }
        void OnImagePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "IsLoading")
            { activityIndicator.IsRunning = ((Image)sender).IsLoading; }
        }




        void OnButtonClicked(object sender, EventArgs args)
        {
            bvalue = 4;
            imageUri[iCount++] = entryImage.Text;
            Button button = (Button)sender;

            timmerlabel.Text = "Image Slider Time:" + entryImage.Text;

            if (button == addButton)
            {
                sliderCount++;
                counter = 0;
                loggerLayout.Children.Add(new Label
                {
                    Text = imageUri[sliderCount - 1]
                });
            }
            else
            {
                // Remove topmost Label from StackLayout
                loggerLayout.Children.RemoveAt(0);
                sliderCount--;
            }
            removeButton.IsEnabled = sliderCount > 0;
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
            timeStamp = TimeSpan.FromSeconds(val);
            if (timeStamp.Seconds < 1)
            {
                timeStamp = TimeSpan.FromSeconds(1);
            }

        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs args)
        {
            value = 1;
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
            counter++;


            if (bvalue != 4)
            {
                if (counter > 4)
                {
                    counter = 0;
                }
                defaultImageCall(counter);
            }

            else
            {
                //newcount = 4 + sliderCount;
                if (counter  > (4+sliderCount))
                {
                    counter = 0;
                }
                defaultImageCall(counter);
            }

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


        void defaultImageCall(int countervalue)
        {
            switch (countervalue)
            {
                case 1:
                    resourceIDimage1 = "PictureFrame.Images.image1.jpg";
                    image1.Source = ImageSource.FromStream(() =>
                    {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream(resourceIDimage1);
                        return stream;
                    });
                   
                    break;

                case 2:
                    resourceIDimage1 = "https://developer.xamarin.com/demo/IMG_0925.JPG?width=512";
                    WebRequest request = WebRequest.Create(new Uri (resourceIDimage1));
                    request.BeginGetResponse((IAsyncResult arg) =>
                    {
                        Stream stream = request.EndGetResponse(arg).GetResponseStream();
                        if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                        { MemoryStream memStream = new MemoryStream(); stream.CopyTo(memStream); memStream.Seek(0, SeekOrigin.Begin); stream = memStream; }
                        ImageSource imageSource = ImageSource.FromStream(() => stream); Device.BeginInvokeOnMainThread(() => image1.Source = imageSource);
                    }, null);
                    break;

                case 3:
                    // Load web bitmap.         
                    image1.Source = Device.OnPlatform(
                    iOS: ImageSource.FromFile("Images/image6.jpe"),
                    Android: ImageSource.FromFile("image6.jpe"),
                    WinPhone: ImageSource.FromFile("image6.jpe"));
                    break;
                case 4:
                    // Load web bitmap.         
                    resourceIDimage1 = "purple.jpg";
                    image1.Source = ImageSource.FromFile(resourceIDimage1);
                    break;

                default:
                    uriImageCall(countervalue);
                    break;
            }




        }
 

        void uriImageCall(int cvalue)
        {

            if (cvalue == 5)
            {
                uri = new Uri(imageUri[0]);
                WebRequest request = WebRequest.Create(uri);
                request.BeginGetResponse((IAsyncResult arg) =>
                {
                    Stream stream = request.EndGetResponse(arg).GetResponseStream();
                    if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                    { MemoryStream memStream = new MemoryStream(); stream.CopyTo(memStream); memStream.Seek(0, SeekOrigin.Begin); stream = memStream; }
                    ImageSource imageSource = ImageSource.FromStream(() => stream); Device.BeginInvokeOnMainThread(() => image1.Source = imageSource);
                }, null);
            }
            else if (cvalue == 6)
            {
                uri1 = new Uri(imageUri[1]);
                WebRequest request = WebRequest.Create(uri1);
                request.BeginGetResponse((IAsyncResult arg) =>
                {
                    Stream stream = request.EndGetResponse(arg).GetResponseStream();
                    if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                    { MemoryStream memStream = new MemoryStream(); stream.CopyTo(memStream); memStream.Seek(0, SeekOrigin.Begin); stream = memStream; }
                    ImageSource imageSource = ImageSource.FromStream(() => stream); Device.BeginInvokeOnMainThread(() => image1.Source = imageSource);
                }, null);
            }

            else if (cvalue == 7)
            {
                uri2 = new Uri(imageUri[2]);
                WebRequest request = WebRequest.Create(uri2);
                request.BeginGetResponse((IAsyncResult arg) =>
                {
                    Stream stream = request.EndGetResponse(arg).GetResponseStream();
                    if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                    { MemoryStream memStream = new MemoryStream(); stream.CopyTo(memStream); memStream.Seek(0, SeekOrigin.Begin); stream = memStream; }
                    ImageSource imageSource = ImageSource.FromStream(() => stream); Device.BeginInvokeOnMainThread(() => image1.Source = imageSource);
                }, null);
            }
            else if (cvalue == 8)
            {
                uri3 = new Uri(imageUri[3]);
                WebRequest request = WebRequest.Create(uri3);
                request.BeginGetResponse((IAsyncResult arg) =>
                {
                    Stream stream = request.EndGetResponse(arg).GetResponseStream();
                    if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                    { MemoryStream memStream = new MemoryStream(); stream.CopyTo(memStream); memStream.Seek(0, SeekOrigin.Begin); stream = memStream; }
                    ImageSource imageSource = ImageSource.FromStream(() => stream); Device.BeginInvokeOnMainThread(() => image1.Source = imageSource);
                }, null);
            }

            else if (cvalue == 9)
            {
                uri4 = new Uri(imageUri[4]);
                WebRequest request = WebRequest.Create(uri4);
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
}

    

