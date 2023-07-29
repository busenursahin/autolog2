using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using autolog.Models;
using autolog.Helper;
using autolog.Data;
using autolog.Abstract;

namespace autolog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string message)
    {
        DbHelper dbHelper = new DbHelper();
        List<LogType> logTypes = dbHelper.GetLogTypes();

        ViewData["result"] = message;

        return View(logTypes);
    }


    [HttpPost]
    public IActionResult PrintLog(string className, string message)
    {
        LogFactory logFactory = new LogFactory();
        ILog loggerInstance = logFactory.GetInstance(className);
        string result = loggerInstance.Log(message);
        return RedirectToAction("Index", new { message = result });
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
