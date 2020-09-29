using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Game.Entities
{
    public class Place
    {
        public int Value { get; private set; }

        public int MaxPlace { get; set; }

        public Place(int val, int max)
        {
            Value = val;
            MaxPlace = max;
        }

        public static Place operator --(Place place)
        {
            int val = place.Value;
            val--;
            if (val < 0)
                val = place.MaxPlace - 1;
            return new Place(val, place.MaxPlace);
        }

        public static Place operator ++(Place place)
        {
            int val = place.Value;
            val++;
            if (val >= place.MaxPlace)
                val = 0;
            return new Place(val, place.MaxPlace);
        }

        public Place GetNext()
        {
            int val = Value;
            val++;
            if (val >= MaxPlace)
                val = 0;
            return new Place(val, MaxPlace);
        }
        public Place GetPrevious()
        {
            int val = Value;
            val--;
            if (val < 0)
                val = MaxPlace - 1;
            return new Place(val, MaxPlace);
        }
    }
}
