using System.Text.RegularExpressions;
using OfficeOpenXml;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Dtos;
using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Concrete.Services;
public class ExcelDataLoader : IDataLoader
{
    private const string _newFilesDirectory = "D:\\Projects\\WeatherForYou\\Datasets";

    public IEnumerable<string> CheckForANewFiles()
    {
        var directories = Directory.GetDirectories(_newFilesDirectory);

        if (!directories.Any())
            return null;

        return directories.Where(d => CityValidator.ValidateCityName(d.Split("\\")[^1])).ToList();
    }

    public IEnumerable<MeteorologyDataDto> GetDataFromDirectory(string[] directoryPathes)
    {
        IEnumerable<MeteorologyDataDto> result = new List<MeteorologyDataDto>();
        foreach (var directory in directoryPathes)
        {
            var files = Directory.GetFiles(directory, "*.xlsx");
            foreach (var file in files)
            {
                if (Regex.Match(new FileInfo(file).Name, "^20[0-2][0-9]-([0-9]|1[0-2])\\.xlsx$").Success)
                    result = result.Concat(GetDataFromFile(file));
                File.Delete(file);
            }
        }
        return result;
    }

    public IEnumerable<MeteorologyDataDto> GetDataFromFile(string filePath)
    {
        var days = new List<MeteorologyDataDto>();
        using (var excelDoc = new ExcelPackage(new FileInfo(filePath)))
        {
            var sheet = excelDoc.Workbook.Worksheets.FirstOrDefault();

            if (sheet == null)
                return null;

            if (CheckIfColumnsIsCorrect(sheet))
            {
                int row = 2;
                while (sheet.Cells[$"A{row}"].Value != null)
                {                   
                    days.Add(new MeteorologyDataDto
                    {
                        Time = GetDateTime(
                            sheet.Name,
                            Convert.ToInt32(sheet.Cells[$"A{row}"].Value),
                            (DateTime)sheet.Cells[$"B{row}"].Value),
                        Temperature = Convert.ToInt32(sheet.Cells[$"C{row}"].Value),
                        WindDirection = (string)sheet.Cells[$"D{row}"].Value,
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
