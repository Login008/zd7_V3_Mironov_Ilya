using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBooking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomSelectionPage : ContentPage
    {
        Hotel hotel1;
        public RoomSelectionPage(object hotel)
        {
            hotel1 = hotel as Hotel;

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(10),
                Spacing = 5,
                Orientation = StackOrientation.Vertical
            };
            if (hotel1.Name != null)
            {
                var nameLabel = new Label
                {
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    StyleClass = new[] { "nameLabel" }
                };
                nameLabel.Text = hotel1.Name;

                var directorLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Color.Gray,
                    StyleClass = new[] { "directorLabel" }
                };
                directorLabel.Text = hotel1.Director;

                var numberPhoneLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Color.Gray,
                    StyleClass = new[] { "directorLabel" }
                };
                numberPhoneLabel.Text = $"Номер телефона: {hotel1.NumberPhone}";

                var adressLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Color.Gray,
                    StyleClass = new[] { "directorLabel" }
                };
                adressLabel.Text = hotel1.Adress;

                var numberLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Color.Gray,
                    StyleClass = new[] { "directorLabel" }
                };
                numberLabel.Text = hotel1.Number;

                var placesLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Color.Gray,
                    StyleClass = new[] { "directorLabel" }
                };
                placesLabel.Text = $"Количество мест: {hotel1.Places}";

                var priceLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Color.Gray,
                    StyleClass = new[] { "directorLabel" }
                };
                priceLabel.Text = $"Цена: {hotel1.Price} руб.";

                stackLayout.Children.Add(nameLabel);
                stackLayout.Children.Add(directorLabel);
                stackLayout.Children.Add(numberPhoneLabel);
                stackLayout.Children.Add(adressLabel);
                stackLayout.Children.Add(numberLabel);
                stackLayout.Children.Add(placesLabel);
                stackLayout.Children.Add(priceLabel);
            }

            InitializeComponent();

            Title = "Выбор количества мест";

            Label header = new Label
            {
                Text = "Выбор количества мест",
                StyleClass = new[] { "headerLabel" }
            };

            numberOfPlaces.TextChanged += (s, e) =>
            {
                try
                {
                    if (int.TryParse(numberOfPlaces.Text, out int result) && result > 4)
                    {
                        numberOfPlaces.Text = "4";
                    }
                    else if (int.TryParse(numberOfPlaces.Text, out int result1) && result1 < 1)
                    {
                        numberOfPlaces.Text = "1";
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Нельзя вводить отрицательные и/или нецелые числа", "OK");
                }
            };

            numberOfRooms.TextChanged += (s, e) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(numberOfPlaces.Text))
                    {
                        if (int.TryParse(numberOfRooms.Text, out int result) && result > int.Parse(numberOfPlaces.Text))
                        {
                            numberOfRooms.Text = numberOfPlaces.Text;
                        }
                    }
                    else if (int.TryParse(numberOfPlaces.Text, out int result1) && result1 < 1)
                    {
                        numberOfPlaces.Text = "1";
                    }
                    else
                    {
                        numberOfRooms.Text = "";
                        DisplayAlert("Ошибка", "Введите сначала количество мест", "OK");
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Нельзя вводить отрицательные и/или нецелые значения", "OK");
                }
            };

            Button calculateCostButton = new Button
            {
                Text = "Расчет стоимости проживания",
                StyleClass = new[] { "actionButton" },
            };
            calculateCostButton.Clicked += goToCalculate;

            Button returnToMainButton = new Button
            {
                Text = "Возврат на главную страницу",
                StyleClass = new[] { "actionButton" },
                Command = new Command(async() => 
                { 
                    try
                    {
                        await Navigation.PopModalAsync();
                    }
                    catch
                    {
                    }
                })
            };

            Content = new StackLayout
            {
                Padding = new Thickness(10, 20),
                Children = { header, stackLayout, numberOfPlaces, numberOfRooms, calculateCostButton, returnToMainButton }
            };
        }
        async public void goToCalculate(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(numberOfPlaces.Text) && !string.IsNullOrEmpty(numberOfRooms.Text))
                {
                    if (hotel1.Name != null && int.Parse(numberOfPlaces.Text) > 0 && int.Parse(numberOfRooms.Text) > 0)
                        await Navigation.PushModalAsync(new CostCalculationPage(hotel1, int.Parse(numberOfPlaces.Text)));
                    else if (int.Parse(numberOfPlaces.Text) < 1 || int.Parse(numberOfRooms.Text) < 1)
                        DisplayAlert("Ошибка", "Нельзя вводить значения меньше единицы", "OK");
                    else
                        DisplayAlert("Ошибка", "Отель для заселения не был выбран", "OK");
                }
                else
                    DisplayAlert("Ошибка", "Заполните все поля", "OK");
            }
            catch
            {
                DisplayAlert("Ошибка", "Вводите только целые адекватные значения", "OK");
            }
        }
    }
}