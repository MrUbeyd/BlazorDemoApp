using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemoApp.Data
{
    public class WeatherDataService
    {
       // Read excel(.xlsx) file

        public List<WeatherData> GetWeatherDatas(DateTime startDate,DateTime endDate)
        {
            List<WeatherData> weatherDataList = new List<WeatherData>();

            

            string filePath = "C:/Users/mubey/source/repos/BlazorDemoApp/IstanbulWeatherDataSeperated.xlsx";

            FileInfo fileInfo = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ( ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                int totalColumn = worksheet.Dimension.End.Column;
                int totalRow = worksheet.Dimension.End.Row;
                int flag = 0;
                

                // yeni bir liste olustumak gerek parametreleri date avgTemp olan yeni bir class lazým yani üstteki liste gibi oluþturabilirsin
                // sonrasýnda maxTmp date minTmp gibi deðerler kullanarak listeyi olustur ve bu listeyi dönecek fonksiyonu yaz 
                // create graph (js) fonksiyonuna yolla oraya parametre verdim ancak bu þekilde dýþarýdan veri çekebiliyoruz js içine galiba ???

                for (int row =1;row<= totalRow; row++)
                {
                    WeatherData weatherData = new WeatherData();

                   

                    flag = 0; // flag must be 0 at every row beginning 

                    for (int column = 1; column <= totalColumn; column++)
                    {
                        
                        if (column == 1)
                        {
                            if (DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture)<=endDate&& DateTime.ParseExact(worksheet.Cells[row, column].Value.ToString(), "dd.MM.yyyy", CultureInfo.CurrentCulture)>=startDate.AddDays(-1)) 
                            {
                                weatherData.Date = worksheet.Cells[row, column].Value.ToString();

                                

                                flag = 1; // if flag == 1 so we find the filtered(by date) row. 
                            }
                            else
                            {
                                flag = 0;
                            }
                        }
                        if (column == 2 && flag==1) weatherData.Condition = worksheet.Cells[row, column].Value.ToString();

                        if (column == 3 && flag == 1)
                        { 
                           weatherData.MaxTemp = worksheet.Cells[row, column].Value.ToString();
                           
                        }
                        if (column == 4 && flag == 1)
                        {
                            weatherData.MinTemp = worksheet.Cells[row, column].Value.ToString();
                            
                        }
                        
                        if (column == 5 && flag == 1) weatherData.AvgWind = worksheet.Cells[row, column].Value.ToString();
                        if (column == 6 && flag == 1) weatherData.AvgHumidity = worksheet.Cells[row, column].Value.ToString();
                        if (column == 7 && flag == 1) weatherData.AvgPressure = worksheet.Cells[row, column].Value.ToString();
                        
                    }
                    if (flag == 1) { // flag == 1 so we can add the data to list
                        weatherDataList.Add(weatherData);

                        

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

                // yeni bir liste olustumak gerek parametreleri date avgTemp olan yeni bir class lazým yani üstteki liste gibi oluþturabilirsin
                // sonrasýnda maxTmp date minTmp gibi deðerler kullanarak listeyi olustur ve bu listeyi dönecek fonksiyonu yaz 
                // create graph (js) fonksiyonuna yolla oraya parametre verdim ancak bu þekilde dýþarýdan veri çekebiliyoruz js içine galiba ???

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
                            
                            maxTmp = Int32.Parse(worksheet.Cells[row, column].Value.ToString()); // NEW !!!
                        }
                        if (column == 4 && flag == 1)
                        {
                           
                            minTmp = Int32.Parse(worksheet.Cells[row, column].Value.ToString()); // NEW !!!
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


    }
}