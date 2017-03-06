using SchoolOfFineArt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CellContextMenu
{
    public class AlertEventArgs
    {
        public string Message { get; set; }
        public string Title { get; set; }
    }

    public partial class DetailPage : ContentPage
    {
        String fullname;
        object enterytext;
        public DetailPage(object detail)
        {


            InitializeComponent();

            if (detail is string)
            {
                detailLabel.Text = detail as string;
            }
            else if (detail is Student)
            {
                detailLabel.Text = "Photo Name - " + (detail as Student).FullName
                    + "\n Time Taken - " + (detail as Student).LastName
                    + "\n Photo Details - " + (detail as Student).MiddleName
                    + "\n Photo Source - " + (detail as Student).PhotoFilename;

                fullname = (detail as Student).FullName;
              
               
            
            }
            enterytext = detail;
            // (detail as Student).FullName = editName.Text;

            
        }
    

       void OnCompleted(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(fileName.Text))
            {

                DisplayAlert("Alert", "Please Enter Valid Input", "OK");
            }
            else
            {
                (enterytext as Student).FullName = fileName.Text;
            }

        }
        void OnCompleted1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(time.Text))
            {

                DisplayAlert("Alert", "Please Enter Valid Input", "OK");
            }
            else
            {

                (enterytext as Student).LastName = time.Text;
            }

        }
        void OnCompleted2(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(url.Text))
            {

                DisplayAlert("Alert", "Please Enter Valid Input", "OK");
            }
            else
            {
                (enterytext as Student).PhotoFilename = url.Text;
            }
        }


        void OnCompleted3(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(detail.Text))
            {

                DisplayAlert("Alert", "Please Enter Valid Input", "OK");
            }
            else
            {
                (enterytext as Student).MiddleName = detail.Text;
            }
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        void OnAddClicked(object sender, EventArgs e)
        {

        }
        void OnRemoveClicked(object sender, EventArgs e)
        {

        }

        void OnEditClicked(object sender, EventArgs e)
        {

            
        }
    }
}