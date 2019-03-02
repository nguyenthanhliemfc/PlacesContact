using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacesContact.Models;
using PlacesContact.RestClient;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlacesContact
{
    public partial class MainPage : ContentPage
    {
        private static readonly string PlaceAPIkey = "YOUR_API_HERE";

        private string googleQuery =
            "https://maps.googleapis.com/maps/api/place/textsearch/json?query={0}+{1}&type={2}&language=it&key=" +
            PlaceAPIkey;

        private readonly string nearbyQuery =
            "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&type={3}&keyword={4}&key=" +
            PlaceAPIkey;

        private readonly string detailsQuery =
            "https://maps.googleapis.com/maps/api/place/details/json?placeid={0}&fields=name,rating,formatted_address,formatted_phone_number&key=" +
            PlaceAPIkey;

        public string radius = "2000";
        public string typeSearch = "Company";
        public MainPage()
        {
            InitializeComponent();
            AddTypeToPicker();
        }

        private async void NearBySearch()
        {
            ActivityIndicatorStatus.IsVisible = true;
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            var result = await NearByPlaceSearch(nearbyQuery, location.Latitude.ToString(),
                location.Longitude.ToString(), radius, typeSearch, "", "");
            var listBusiness = new ObservableCollection<BusinessContact.Result>();
            foreach (var item in result.results)listBusiness.Add(item);
            LabelTotalResult.Text = "Total result: " + listBusiness.Count;
            ListViewResult.ItemsSource = listBusiness;
            ActivityIndicatorStatus.IsVisible = false;
        }


        static async Task<BusinessContact.RootObject> NearByPlaceSearch(string googleQuery, string lat, string lng, string radius, string type,string keyword, string nextPageToken)
        {
            var pagetoken = nextPageToken != null ? "&pagetoken=" + nextPageToken : null;
            var requestUri = string.Format(googleQuery, lat, lng, radius, type, keyword) + pagetoken;
            try
            {
                var restClient = new RestClient<BusinessContact.RootObject>();
                var result = await restClient.GetAsync(requestUri);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Near by place search error: " + e.Message);
            }
            return null;
        }

        static async Task<BusinessContactDetail.RootObject> PlaceDetailsSearch(string detailsQuery,string placeID, string nextPageToken)
        {
            var pagetoken = nextPageToken != null ? "&pagetoken=" + nextPageToken : null;
            var requestUri = string.Format(detailsQuery, placeID) + pagetoken;
            try
            {
                var restClient = new RestClient<BusinessContactDetail.RootObject>();
                var result = await restClient.GetAsync(requestUri);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Near by place search error: " + e.Message);
            }
            return null;
        }

        private void AddTypeToPicker()
        {
            var typeList = new List<string>();
            typeList.Add("company");
            typeList.Add("accounting");
            typeList.Add("airport");
            typeList.Add("amusement_park");
            typeList.Add("aquarium");
            typeList.Add("art_gallery");
            typeList.Add("atm");
            typeList.Add("bakery");
            typeList.Add("bank");
            typeList.Add("bar");
            typeList.Add("beauty_salon");
            typeList.Add("bicycle_store");
            typeList.Add("book_store");
            typeList.Add("bowling_alley");
            typeList.Add("bus_station");
            typeList.Add("cafe");
            typeList.Add("campground");
            typeList.Add("car_dealer");
            typeList.Add("car_rental");
            typeList.Add("car_repair");
            typeList.Add("car_wash");
            typeList.Add("casino");
            typeList.Add("cemetery");
            typeList.Add("church");
            typeList.Add("city_hall");
            typeList.Add("clothing_store");
            typeList.Add("convenience_store");
            typeList.Add("courthouse");
            typeList.Add("dentist");
            typeList.Add("department_store");
            typeList.Add("doctor");
            typeList.Add("electrician");
            typeList.Add("electronics_store");
            typeList.Add("embassy");
            typeList.Add("fire_station");
            typeList.Add("florist");
            typeList.Add("funeral_home");
            typeList.Add("furniture_store");
            typeList.Add("gas_station");
            typeList.Add("gym");
            typeList.Add("hair_care");
            typeList.Add("hardware_store");
            typeList.Add("hindu_temple");
            typeList.Add("home_goods_store");
            typeList.Add("hospital");
            typeList.Add("insurance_agency");
            typeList.Add("jewelry_store");
            typeList.Add("laundry");
            typeList.Add("lawyer");
            typeList.Add("library");
            typeList.Add("liquor_store");
            typeList.Add("local_government_office");
            typeList.Add("locksmith");
            typeList.Add("lodging");
            typeList.Add("meal_delivery");
            typeList.Add("meal_takeaway");
            typeList.Add("mosque");
            typeList.Add("movie_rental");
            typeList.Add("movie_theater");
            typeList.Add("moving_company");
            typeList.Add("museum");
            typeList.Add("night_club");
            typeList.Add("painter");
            typeList.Add("park");
            typeList.Add("parking");
            typeList.Add("pet_store");
            typeList.Add("pharmacy");
            typeList.Add("physiotherapist");
            typeList.Add("plumber");
            typeList.Add("police");
            typeList.Add("post_office");
            typeList.Add("real_estate_agency");
            typeList.Add("restaurant");
            typeList.Add("roofing_contractor");
            typeList.Add("rv_park");
            typeList.Add("school");
            typeList.Add("shoe_store");
            typeList.Add("shopping_mall");
            typeList.Add("spa");
            typeList.Add("stadium");
            typeList.Add("storage");
            typeList.Add("store");
            typeList.Add("subway_station");
            typeList.Add("supermarket");
            typeList.Add("synagogue");
            typeList.Add("taxi_stand");
            typeList.Add("train_station");
            typeList.Add("transit_station");
            typeList.Add("travel_agency");
            typeList.Add("veterinary_care");
            typeList.Add("zoo");
            PickerType.ItemsSource = typeList;
            PickerType.SelectedItem = "Company";
        }

        private void ButtonSearch_OnClicked(object sender, EventArgs e)
        {
            NearBySearch();
        }

        private void SliderRadius_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            radius = SliderRadius.Value.ToString();
            LabelRadius.Text = radius;
        }

        private void PickerType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            typeSearch = PickerType.SelectedItem.ToString();
        }

        private async void ListViewResult_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ActivityIndicatorStatus.IsVisible = true;
            try
            {
                var selectedPlace = ListViewResult.SelectedItem as BusinessContact.Result;
                var result = await PlaceDetailsSearch(detailsQuery, selectedPlace.place_id, "");
                var content = "";
                if (result.result.geometry != null)
                    content = "Location: " + result.result.geometry.location + "\nAddress: " +
                              result.result.formatted_address + "\nPhone: " + result.result.formatted_phone_number;
                else
                    content = "Address: " + result.result.formatted_address + "\nPhone: " +
                              result.result.formatted_phone_number;
                ActivityIndicatorStatus.IsVisible = false;
                await DisplayAlert(selectedPlace.name, content, "OK");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            ActivityIndicatorStatus.IsVisible = false;
        }
    }
}
