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
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<MeteorologyData> _repository;
        private readonly IDataLoader _dataLoader;

        public HomeController(ILogger<HomeController> logger, IRepository<MeteorologyData> repository, IDataLoader dataLoader)
        {
            _logger = logger;
            _repository = repository;
            _dataLoader = dataLoader;
        }

        public IActionResult Index()
        {
            
            var directories = _dataLoader.CheckForANewFiles();
            if (directories.Count() > 0)
            {
                var resultList = _dataLoader.GetDataFromDirectory(directories.ToArray());
                _repository.AddRange(resultList);
            }
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