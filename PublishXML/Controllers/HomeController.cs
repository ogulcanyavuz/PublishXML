using Microsoft.AspNetCore.Mvc;
using PublishXML.Models;
using PublishXML.XmlCRUD;
using System.Diagnostics;
using System.Net;
using System.Xml;

namespace PublishXML.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            XmlEdit xmlEdit = new XmlEdit();
     
            return this.Content(xmlEdit.SaveXml("http://convert.stockmount.com/xml/publish/28094/xmloutlet", "ozelXML"), "text/xml", System.Text.Encoding.UTF8);
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