using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WeatherForYou.Domain.Concrete.Services;

namespace WeatherForYou.Controllers
{
    public class MeteorologyController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string cityName, IFormFileCollection files)
        {
            if (files is null)
            {
                return BadRequest();
            }

            if (files.Select(file => Regex.Match(file.FileName, "^20[0-2][0-9]-([0-9]|1[0-2])\\.xlsx$").Success)
                .Any(f => f.Equals(false)))
            {
                ModelState.AddModelError("files", "Incorrect file name, should year-month format");
                return View();
            }

            string dir = ExcelDataLoader._newFilesDirectory + "\\" + cityName;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            foreach (var file in files)
            {
                var path = ExcelDataLoader._newFilesDirectory + "\\" + cityName + "\\" + file.FileName;
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
