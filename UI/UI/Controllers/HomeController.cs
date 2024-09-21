namespace UI.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        ViewData["Title"] = "Home Page";

        return View("Index");
    }

    [HttpGet]
    public ActionResult Privacy()
    {
        ViewData["Title"] = "Privacy Policy";

        return View("Privacy");
    }

    [HttpGet]
    public ActionResult Error()
    {
        ViewData["Title"] = "Error";

        return View("Error");
    }
}
