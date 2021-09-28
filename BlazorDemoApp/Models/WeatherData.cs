using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemoApp.Data
{
    public class WeatherData
    {
        public DateTime Date { get; set; }

        public string Condition { get; set; }

        public int MaxTemp { get; set; }

        public int MinTemp { get; set; }

        public int AvgWind { get; set; }

        public int AvgHumidity { get; set; }

        public int AvgPressure { get; set; }

        


    }
}