using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HotelBooking
{
    public class GroupedHotelList
    {
        public ObservableCollection<HotelGroup> HotelGroups { get; set; }

        public GroupedHotelList()
        {
            HotelGroups = new ObservableCollection<HotelGroup>
            {
                new HotelGroup("Эконом")
                {
                    new Hotel { Name = "Гостиница А", Director = "Иванов И.И.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 },
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 },
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 },
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 }
                },
                new HotelGroup("Люкс")
                {
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 },
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 },
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 },
                    new Hotel { Name = "Гостиница Б", Director = "Петров П.П.", NumberPhone = "88005555535", Adress = "Г. Екатеринбург, ул. Ленина, д. 138", Number = "Номер", Places = 3, Price = 2500 }
                }
            };
        }
    }
}
