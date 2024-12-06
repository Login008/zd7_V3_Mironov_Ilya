using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HotelBooking
{
    public class HotelGroup : ObservableCollection<Hotel>
    {
        public string Category { get; private set; }

        public HotelGroup(string category)
        {
            Category = category;
        }
    }
}
