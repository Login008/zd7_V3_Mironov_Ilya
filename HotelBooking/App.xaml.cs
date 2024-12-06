using HotelBooking.Services;
using HotelBooking.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBooking
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new CarouselPage
            {
                Children = { new MainPage(), new CostCalculationPage(new Hotel(), 0), new RoomSelectionPage(new Hotel()) }
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
