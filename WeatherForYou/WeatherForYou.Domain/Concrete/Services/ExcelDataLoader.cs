using System.Text.RegularExpressions;
using OfficeOpenXml;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Models;
using WeatherForYou.Domain.Utilities;

namespace WeatherForYou.Domain.Concrete.Services;
public class ExcelDataLoader : IDataLoader
{
    public const string _newFilesDirectory = "D:\\Projects\\WeatherForYou\\Datasets";
    private readonly IRepository<MeteorologyData> _database;

    public ExcelDataLoader(IRepository<MeteorologyData> database)
    {
        _database = database;
    }
    public IEnumerable<string> CheckForANewFiles()
    {
        var directories = Directory.GetDirectories(_newFilesDirectory);

        if (!directories.Any())
            return null;

        return directories.Where(d => CityValidator.ValidateCityName(d.Split("\\")[^1])).ToList();
    }

    public IEnumerable<MeteorologyData> GetDataFromDirectory(string[] directoryPathes)
    {
        IEnumerable<MeteorologyData> result = new List<MeteorologyData>();
        foreach (var directory in directoryPathes)
        {
            var files = Directory.GetFiles(directory, "*.xlsx");
            foreach (var file in files)
            {
                if (Regex.Match(new FileInfo(file).Name, "^20[0-2][0-9]-([0-9]|1[0-2])\\.xlsx$").Success)
                    result = result.Concat(GetDataFromFile(file));
                //File.Delete(file);
            }
        }
        return result;
    }

    public IEnumerable<MeteorologyData> GetDataFromFile(string filePath)
    {
        var days = new List<MeteorologyData>();
        using (var excelDoc = new ExcelPackage(new FileInfo(filePath)))
        {
            var sheet = excelDoc.Workbook.Worksheets.FirstOrDefault();

            if (sheet == null)
                return null;

            var city = new City { Name = filePath.Split("\\")[^2] };
            if (CheckIfColumnsIsCorrect(sheet))
            {
                int row = 2;
                while (sheet.Cells[$"A{row}"].Value != null)
                {
                    days.Add(new MeteorologyData
                    {
                        City = city,
                        Time = GetDateTime(
                                sheet.Name,
                                Convert.ToInt32(sheet.Cells[$"A{row}"].Value),
                                (DateTime)sheet.Cells[$"B{row}"].Value),
                        Temperature = Convert.ToInt32(sheet.Cells[$"C{row}"].Value),
                        WindDirection = TranslationHelper
                            .TranslateWindDirrectionName((string)sheet.Cells[$"D{row}"].Value),
                        WindSpeed = (double)sheet.Cells[$"E{row}"].Value
                    }); 
                    row++;
                }
            }
        }
        return days;
    }

    private DateTime GetDateTime(string yearAndMonth, int day, DateTime time)
    {
        var year = Convert.ToInt32(yearAndMonth.Split("-")[0]);
        var month = Convert.ToInt32(yearAndMonth.Split("-")[1]);
        return new DateTime(year, month, day, time.Hour, time.Minute, 0);
    }

    private bool CheckIfColumnsIsCorrect(ExcelWorksheet worksheet) =>
    worksheet.Cells["A1"].Text == "Число месяца"
        && worksheet.Cells["B1"].Text == "UTC"
        && worksheet.Cells["C1"].Text == "T"
        && worksheet.Cells["D1"].Text == "dd"
        && worksheet.Cells["E1"].Text == "FF";
}
