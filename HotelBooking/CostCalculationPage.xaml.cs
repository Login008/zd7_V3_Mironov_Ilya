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
    public partial class CostCalculationPage : ContentPage
    {
        public CostCalculationPage(object hotel, int numberOfPlaces)
        {
            Hotel hotel1 = hotel as Hotel;
            int places = numberOfPlaces;
            InitializeComponent();

            Title = "Калькулятор стоимости";

            Label header = new Label
            {
                Text = "Калькулятор стоимости",
                StyleClass = new[] { "headerLabel" }
            };

            Entry duration = new Entry { Placeholder = "Срок проживания в днях", Keyboard = Keyboard.Numeric, StyleClass = new[] { "inputField" }, PlaceholderColor = Color.Black };
            Label label = new Label { StyleClass = new[] { "nameLabel" }, HorizontalTextAlignment = TextAlignment.Center };

            duration.TextChanged += (s, e) =>
            {
                try
                {
                    if (int.TryParse(duration.Text, out int result) && result > 365)
                    {
                        duration.Text = "365";
                    }
                    else if (int.TryParse(duration.Text, out int result1) && result1 < 1)
                    {
                        duration.Text = "1";
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Нельзя вводить отрицательные и/или нецелые значения", "OK");
                }
            };

            Button calculateButton = new Button
            {
                Text = "Рассчитать стоимость",
                StyleClass = new[] { "actionButton" },
                Command = new Command(() =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(duration.Text))
                        {
                            if (hotel1.Name != null)
                            {
                                if (int.Parse(duration.Text) >= 1)
                                {
                                    double sum = hotel1.Price * int.Parse(duration.Text) * numberOfPlaces;
                                    if (numberOfPlaces == 4)
                                        sum /= 2;
                                    label.Text = $"Итого: {sum}";
                                }
                                else
                                    DisplayAlert("Ошибка", "Нельзя вводить значения меньше единицы", "OK");
                            }
                            else
                                DisplayAlert("Ошибка", "Отель не был выбран, вернитесь на главную страницу, заполните все данные, выберите отель и нажмите кнопку 'Рассчет стоимости проживания'", "OK");
                        }
                        else
                            DisplayAlert("Ошибка", "Заполните поле", "OK");
                    }
                    catch
                    {
                        DisplayAlert("Ошибка", "Вводите только целые адекватные значения", "OK");
                    }
                })
            };

            Content = new StackLayout
            {
                Padding = new Thickness(10, 20),
                Children = { header, duration, label, calculateButton }
            };
        }
    }
}