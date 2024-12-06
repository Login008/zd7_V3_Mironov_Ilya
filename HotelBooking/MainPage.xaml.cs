using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBooking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Title = "Онлайн-бронирование гостиницы";

            Label header = new Label
            {
                Text = "Онлайн-бронирование гостиницы",
                StyleClass = new[] { "headerLabel" }
            };

            var hotels = new GroupedHotelList();
            hotelListView = new ListView
            {
                HasUnevenRows = true,
                IsGroupingEnabled = true, 
                ItemsSource = hotels.HotelGroups, 
                GroupDisplayBinding = new Binding("Category"), 
                ItemTemplate = new DataTemplate(() =>
                {
                    var stackLayout = new StackLayout
                    {
                        Padding = new Thickness(10),
                        Spacing = 5,
                        Orientation = StackOrientation.Vertical
                    };

                    var nameLabel = new Label
                    {
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        StyleClass = new[] { "nameLabel" }
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    var directorLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = Color.Gray,
                        StyleClass = new[] { "directorLabel" }
                    };
                    directorLabel.SetBinding(Label.TextProperty, "Director");

                    var numberPhoneLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = Color.Gray,
                        StyleClass = new[] { "directorLabel" }
                    };
                    numberPhoneLabel.SetBinding(Label.TextProperty, "NumberPhone", stringFormat: "Номер телефона: {0}");

                    var adressLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = Color.Gray,
                        StyleClass = new[] { "directorLabel" }
                    };
                    adressLabel.SetBinding(Label.TextProperty, "Adress");

                    var numberLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = Color.Gray,
                        StyleClass = new[] { "directorLabel" }
                    };
                    numberLabel.SetBinding(Label.TextProperty, "Number");

                    var placesLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = Color.Gray,
                        StyleClass = new[] { "directorLabel" }
                    };
                    placesLabel.SetBinding(Label.TextProperty, "Places", stringFormat: "Количество мест: {0}");

                    var priceLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = Color.Gray,
                        StyleClass = new[] { "directorLabel" }
                    };
                    priceLabel.SetBinding(Label.TextProperty, "Price", stringFormat: "Цена: {0} руб.");

                    stackLayout.Children.Add(nameLabel);
                    stackLayout.Children.Add(directorLabel);
                    stackLayout.Children.Add(numberPhoneLabel);
                    stackLayout.Children.Add(adressLabel);
                    stackLayout.Children.Add(numberLabel);
                    stackLayout.Children.Add(placesLabel);
                    stackLayout.Children.Add(priceLabel);

                    var cell = new ViewCell { View = stackLayout };
                    return cell;
                }
                )
            };

            Button selectRoomsButton = new Button
            {
                Text = "Выбор количества мест",
                StyleClass = new[] { "actionButton" },
            };
            selectRoomsButton.Clicked += goToSelection;

            Button calculateCostButton = new Button
            {
                Text = "Расчет стоимости проживания",
                StyleClass = new[] { "actionButton" },
            };
            calculateCostButton.Clicked += goToCalculation;

            ageEntry.TextChanged += (s, e) =>
            {
                if (int.TryParse(ageEntry.Text, out int result) && result > 120)
                {
                    ageEntry.Text = "120";
                } else if (int.TryParse(ageEntry.Text, out int result1) && result1 < 0 )
                {
                    ageEntry.Text = "0";
                }
            };

            checkInDatePicker.MinimumDate = DateTime.Today;
            Content = new StackLayout
            {
                Padding = new Thickness(10, 20),
                Children = { header, hotelListView, surnameEntry, ageEntry, stackWithPicker, stackPicker, selectRoomsButton, calculateCostButton }
            };
        }
        async public void goToSelection(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(surnameEntry.Text) && !string.IsNullOrEmpty(ageEntry.Text))
                {
                    if (int.Parse(ageEntry.Text) >= 18)
                    {
                        if (hotelListView.SelectedItem != null)
                            if (CheckForSurname(surnameEntry.Text))
                                if (picker.SelectedIndex != -1)
                                    await Navigation.PushModalAsync(new RoomSelectionPage(hotelListView.SelectedItem));
                                else
                                    DisplayAlert("Ошибка", "Выберите вид оплаты", "OK");
                            else
                                DisplayAlert("Ошибка", "В фамилии можно вводить только буквы", "OK");
                        else
                            DisplayAlert("Ошибка", "Выберите номер, нажав на него", "OK");
                    }
                    else
                        DisplayAlert("Ошибка", "Несовершеннолетние не могут бронировать номера", "OK");
                }
                else
                    DisplayAlert("Ошибка", "Заполните все поля", "OK");
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите свой возрастах в полных годах", "OK");
            }
        }
        async public void goToCalculation(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(surnameEntry.Text) && !string.IsNullOrEmpty(ageEntry.Text))
                {
                if (int.Parse(ageEntry.Text) >= 18)
                {
                    if (hotelListView.SelectedItem != null)
                        if (CheckForSurname(surnameEntry.Text))
                                if (picker.SelectedIndex != -1)
                                    await Navigation.PushModalAsync(new CostCalculationPage(hotelListView.SelectedItem, 1));
                                else
                                    DisplayAlert("Ошибка", "Выберите вид оплаты", "OK");
                        else
                            DisplayAlert("Ошибка", "В фамилии можно вводить только буквы", "OK");
                    else
                        DisplayAlert("Ошибка", "Выберите номер, нажав на него", "OK");
                }
                else
                    DisplayAlert("Ошибка", "Несовершеннолетние не могут бронировать номера", "OK");
            }
            else
                DisplayAlert("Ошибка", "Заполните все поля", "OK");
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите свой возрастах в полных годах", "OK");
            }
        }
        public bool CheckForSurname(string surname)
        {
            foreach (var ch in surname)
            {
                if (!char.IsLetter(ch))
                    return false;
            }
            return true;
        }
    }
}