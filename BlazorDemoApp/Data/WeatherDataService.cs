using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BlazorDemoApp.Data
{
    public class WeatherDataService
    {
        // Read excel(.xlsx) file
        SqlCommands conn = new SqlCommands();
        public List<WeatherData> GetWeatherDatas(DateTime startDate, DateTime endDate)
        {
            List<WeatherData> weatherDataList = new List<WeatherData>();



            string filePath = "C:/Users/mubey/source/repos/BlazorDemoApp/IstanbulWeatherDataSeperated.xlsx";

            FileInfo fileInfo = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                int totalColumn = worksheet.Dimension.End.Column;
                int totalRow = worksheet.Dimension.End.Row;
                int flag = 0;


                // yeni bir liste olustumak gerek parametreleri date avgTemp olan yeni bir class lazým yani üstteki liste gibi oluþturabilirsin
                // sonrasýnda maxTmp date minTmp gibi deðerler kullanarak listeyi olustur ve bu listeyi dönecek fonksiyonu yaz 
                // create graph (js) fonksiyonuna yolla oraya parametre verdim ancak bu þekilde dýþarýdan veri çekebiliyoruz js içine galiba ???

                for (int row = 1; row <= totalRow; row++)
                {
                    WeatherData weatherData = new WeatherData();



                    flag = 0; // flag must be 0 at every row beginning 

                    for (int column = 1; column <= totalColumn; column++)
                    {

                        if (column == 1)
                        {
                            if (DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture) <= endDate && DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture) >= startDate.AddDays(-1))
                            {
                                weatherData.Date = DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture);



                                flag = 1; // if flag == 1 so we find the filtered(by date) row. 
                            }
                            else
                            {
                                flag = 0;
                            }
                        }
                        if (column == 2 && flag == 1) weatherData.Condition = worksheet.Cells[row, column].Value.ToString();

                        if (column == 3 && flag == 1)
                        {
                            weatherData.MaxTemp = Int32.Parse(worksheet.Cells[row, column].Value.ToString());

                        }
                        if (column == 4 && flag == 1)
                        {
                            weatherData.MinTemp = Int32.Parse(worksheet.Cells[row, column].Value.ToString());

                        }

                        if (column == 5 && flag == 1) weatherData.AvgWind = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                        if (column == 6 && flag == 1) weatherData.AvgHumidity = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                        if (column == 7 && flag == 1) weatherData.AvgPressure = Int32.Parse(worksheet.Cells[row, column].Value.ToString());

                    }
                    if (flag == 1)
                    { // flag == 1 so we can add the data to list
                        weatherDataList.Add(weatherData);

                        /*
                        SqlCommand cmd = new SqlCommand("insert into TBL_WEATHER_DATA (DATE,CONDITION,MAXTEMP,MINTEMP,AVGWIND,AVGHUMIDITY,AVGPRESSURE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", conn.connection());
                        cmd.Parameters.AddWithValue("@p1", weatherData.Date);
                        cmd.Parameters.AddWithValue("@p2", weatherData.Condition);
                        cmd.Parameters.AddWithValue("@p3", weatherData.MaxTemp);
                        cmd.Parameters.AddWithValue("@p4", weatherData.MinTemp);
                        cmd.Parameters.AddWithValue("@p5", weatherData.AvgWind);
                        cmd.Parameters.AddWithValue("@p6", weatherData.AvgHumidity);
                        cmd.Parameters.AddWithValue("@p7", weatherData.AvgPressure);
                        cmd.ExecuteNonQuery();
                        conn.connection().Close();
                        */

                    }

                }
            }

            //data to json file
            /*using (StreamWriter file = File.CreateText(@"C:/Users/mubey/source/repos/BlazorDemoApp/weatherDataJson.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, weathherDataList);

            }*/


            return weatherDataList;
        }

        public List<WeatherDataForGraph> GetWeatherDataForGraph(DateTime startDate, DateTime endDate)
        {

            List<WeatherDataForGraph> weatherListDataForGraphs = new List<WeatherDataForGraph>();

            string filePath = "C:/Users/mubey/source/repos/BlazorDemoApp/IstanbulWeatherDataSeperated.xlsx";

            FileInfo fileInfo = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                int totalColumn = worksheet.Dimension.End.Column;
                int totalRow = worksheet.Dimension.End.Row;
                int flag = 0;
                int maxTmp = 0;
                int minTmp = 0;


                for (int row = 1; row <= totalRow; row++)
                {


                    WeatherDataForGraph weatherDataForGraph = new WeatherDataForGraph();

                    flag = 0; // flag must be 0 at every row beginning 

                    for (int column = 1; column <= totalColumn; column++)
                    {


                        if (column == 1)
                        {
                            if (DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture) <= endDate && DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture) >= startDate.AddDays(-1))
                            {
                                weatherDataForGraph.date = DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture);
                                flag = 1; // if flag == 1 so we find the filtered(by date) row. 
                            }
                            else
                            {
                                flag = 0;
                            }

                        }


                        if (column == 3 && flag == 1)
                        {

                            maxTmp = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                        }
                        if (column == 4 && flag == 1)
                        {

                            minTmp = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                        }



                    }

                    if (flag == 1)
                    { // flag == 1 so we can add the data to list
                        weatherDataForGraph.value = (maxTmp + minTmp) / 2;

                        weatherListDataForGraphs.Add(weatherDataForGraph);

                    }

                }
            }




            return weatherListDataForGraphs;
        }


        public List<WeatherData> GetWeatherDataFromSQL(DateTime startDate, DateTime endDate)
        {
            List<WeatherData> weatherDataList = new List<WeatherData>();

            int maxTemp = 0;
            int minTemp = 0;
            int avgWind = 0;
            int avgHumidity = 0;
            int avgPressure = 0;
            DateTime date;
            string condition;

            SqlCommand cmd = new SqlCommand("SELECT * from TBL_WEATHER_DATA ORDER BY CONVERT(datetime, DATE, 103) ASC", conn.connection());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                date = DateTime.ParseExact(dr[1].ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture);


                if (date <= endDate && date >= startDate.AddDays(-1))
                {
                    condition = dr[2].ToString();
                    maxTemp = Int32.Parse(dr[3].ToString()); // parse harici (int) þeklinde typecast dene !!!
                    minTemp = Int32.Parse(dr[4].ToString());
                    avgWind = Int32.Parse(dr[5].ToString());
                    avgHumidity = Int32.Parse(dr[6].ToString());
                    avgPressure = Int32.Parse(dr[7].ToString());

                    WeatherData weatherData = new WeatherData();

                    weatherData.Date = date;
                    weatherData.Condition = condition;
                    weatherData.MaxTemp = maxTemp;
                    weatherData.MinTemp = minTemp;
                    weatherData.AvgWind = avgWind;
                    weatherData.AvgHumidity = avgHumidity;
                    weatherData.AvgPressure = avgPressure;

                    weatherDataList.Add(weatherData);

                }

            }
            conn.connection().Close();
            conn.connection().Dispose();

            return weatherDataList;
        }



        public List<WeatherDataForGraph> GetWeatherDataForGraphSQL(DateTime startDate, DateTime endDate)
        {

            List<WeatherDataForGraph> weatherListDataForGraphs = new List<WeatherDataForGraph>();

            int maxTemp = 0;
            DateTime date;

            SqlCommand cmdDate = new SqlCommand("SELECT DATE,MAXTEMP from TBL_WEATHER_DATA ORDER BY CONVERT(datetime, DATE, 103) ASC", conn.connection());
            SqlDataReader dr = cmdDate.ExecuteReader();

            while (dr.Read())
            {
                date = DateTime.ParseExact(dr[0].ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture);


                if (date <= endDate && date >= startDate.AddDays(-1))
                {
                    maxTemp = Int32.Parse(dr[1].ToString()); // parse harici (int) þeklinde typecast dene !!!

                    WeatherDataForGraph weatherDataForGraph = new WeatherDataForGraph();

                    weatherDataForGraph.date = date;

                    weatherDataForGraph.value = maxTemp;

                    weatherListDataForGraphs.Add(weatherDataForGraph);
                }
            }
            conn.connection().Close();


            return weatherListDataForGraphs;
        }


        public List<WeatherData> GetWeatherDataFromSqlEfCore(DateTime startDate, DateTime endDate)
        {
            List<WeatherData> weatherDataList = new List<WeatherData>();
            
            WeatherDbContext weatherDbContext = new WeatherDbContext();

            var results = weatherDbContext.weatherData.Where(w => w.Date <= endDate && w.Date >= startDate.AddDays(-1)).ToList();

            weatherDataList = results;

            return weatherDataList;
        }


    }
}