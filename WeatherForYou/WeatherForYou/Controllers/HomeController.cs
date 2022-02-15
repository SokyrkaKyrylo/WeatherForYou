using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Concrete.Services;
using WeatherForYou.Domain.Models;
using WeatherForYou.Models;

namespace WeatherForYou.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ILogger<HomeController> logger, IRepository<MeteorologyData> repository, IDataLoader dataLoader, IDataProcessor processor)
        {
            _logger = logger;
            _repository = repository;
            _dataLoader = dataLoader;
            _dataProcessor = processor;
        }

        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<MeteorologyData> _repository;
        private readonly IDataLoader _dataLoader;
        private readonly IDataProcessor _dataProcessor;

        public IActionResult Index()
        {
            //var directories = _dataLoader.CheckForANewFiles();
            //if (directories.Count() > 0)
            //{
            //    var resultList = _dataLoader.GetDataFromFile("D:\\Projects\\WeatherForYou\\Datasets\\київ\\2012-2.xlsx");
            //    _repository.AddRange(resultList);
            //    _repository.Save();
            //}
            //var result = _repository.CheckIfDataIsValid();
            //if (!result.Status)
            //    _dataProcessor.RestoreDataLinearInterpolationAsync(result.InvalidMeteorologiesData);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}