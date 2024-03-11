using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;
public class ProcedureController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
