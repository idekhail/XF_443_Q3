using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_Mid2_Lab1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        Address address; Person person;
        public InfoPage(Address address)
        {
            InitializeComponent();
            this.address = address;
            Logout.Clicked += (s, e) => Navigation.PushAsync(new MainPage());
            Control.Clicked += (s, e) => Navigation.PushAsync(new ControlPage(this.address));

            Display();
        }

        public async void Display()
        {
            person = App.AddressSQLite.GetPersonAsync(this.address.Id).Result;
            if(person != null)
                Show.Text = $"{ this.address.Id}\t{ this.person.Name}\t{ this.address.HomeNumber}\t{ this.address.City}";
            else
                await DisplayAlert("Error", "person is null", "Ok");
        }
       

        private async void GetAllPeopleInAddress_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HomeNumber.Text) && (!string.IsNullOrEmpty(City.Text)))
            {
                string data = "";
                var address = await App.AddressSQLite.GetAddressAsync(HomeNumber.Text, City.Text);

                if (address != null)
                {                     
                    var PeopleInaddress = await App.AddressSQLite.GetAllPeopleInAddressAsync(address.Id);     

                    Section.Text = "GetAllPeopleInAddress";
                    foreach (var p in PeopleInaddress)
                    {                                                                    
                        data += p.Id + "   " + p.Name + "   " + address.HomeNumber + "   " + address.City + "\n";                        
                    }
                    Show.Text = data;
                }
                else
                    await DisplayAlert("Error", "Address is null", "Ok");
            }
            else
                await DisplayAlert("Error", "HomeNumber or City is empty", "Ok");
        }

        private async void GetAllAddress_Clicked(object sender, EventArgs e)
        {

            string data = "";
            var addressPeople = await App.AddressSQLite.GetAllAddressAsync();
            if (addressPeople != null)
            {
                Section.Text = "GetAllAddress";

                foreach (var a in addressPeople)
                {
                   data += a.Id +"   " + a.HomeNumber + "   " + a.City + "\n";
                }
                Show.Text = data;
            }
            else
                await DisplayAlert("Error", "Address is null", "Ok");

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            HomeNumber.Focus();
        }

        private async void GetAllPeople_Clicked(object sender, EventArgs e)
        {
            string data = "";
            var AllPeople = await App.AddressSQLite.GetAllPeopleAsync();
            if (AllPeople != null)
            {
                Section.Text = "GetAllPeople";
                foreach (var p in AllPeople)
                {
                    data += p.Id + "   " + p.Name + "   " + p.AId + "\n";

                }
                Show.Text = data;
            }
            else
                await DisplayAlert("Error", "Address is null", "Ok");
        }
    }
}