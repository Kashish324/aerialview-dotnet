using AerialView.Models; 
using Microsoft.AspNetCore.Mvc;



namespace AerialView.Controllers
{
    public class DataGridController : Controller
    {
        public IActionResult DataGridOverview()
        {
            return View();
        }

    }
}


//namespace DevExtreme.NETCore.Demos.Controllers
//{
//    public class DataGridController : Controller
//    {

    
//        public IActionResult DataGridOverview()
//        {
//            int? rptId = TempData["RptId"] as int?;
//            Console.WriteLine("RptId value: " + rptId);
//            return View();

//        }
//    }
//}




