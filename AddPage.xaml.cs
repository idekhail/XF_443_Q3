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
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            Add_Address();

            Add_Person();
        }

        public async void Add_Address()
        {
            if ((!string.IsNullOrEmpty(HomeNumber.Text)) && (!string.IsNullOrEmpty(City.Text)))
            {
                var address = new Address()
                {
                    HomeNumber = HomeNumber.Text,
                    City = City.Text,
                };
                await App.AddressSQLite.SaveAddressAsync(address);
            }
            else
                await DisplayAlert("Error", "Feilds are empty", "Ok");
        }
        public async void Add_Person()
        {
            if (!string.IsNullOrEmpty(Name.Text))
            {
                var address = await App.AddressSQLite.GetAddressAsync(HomeNumber.Text, City.Text);
                await DisplayAlert("Address", address.Id + "   " + address.HomeNumber + "   " + address.City, "Ok");

                var person = new Person()
                {
                    Name = Name.Text,
                    AId = address.Id,
                };
                await App.AddressSQLite.SavePersonAsync(person);
                await DisplayAlert("Person", person.Id + "   " +  person.Name + " " + person.AId, "Ok");

                await Navigation.PopAsync();
            }
        }


        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}