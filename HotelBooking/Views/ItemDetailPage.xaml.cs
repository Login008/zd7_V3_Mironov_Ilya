using HotelBooking.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace HotelBooking.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}