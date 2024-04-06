using AerialView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

//using NuGet.Protocol;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AerialView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

 
        public IActionResult Index(string SubChildName, int RptId, string ConnString)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found in configuration.");
            
            try
            {
                using SqlConnection con = new(connectionString);
                con.Open();
                string menuParentQuery = "SELECT * FROM Menu_Parent";
                string subMenuQuery = "SELECT * FROM Menu_Child_New";
                string childSubMenuQuery = "SELECT * FROM ReportDATA_View";

                using SqlCommand menuParentCmd = new SqlCommand(menuParentQuery, con);
                using SqlDataReader menuParentReader = menuParentCmd.ExecuteReader();
                DataTable menuParentDt = new DataTable();
                menuParentDt.Load(menuParentReader);

                using SqlCommand subMenuCmd = new SqlCommand(subMenuQuery, con);
                using SqlDataReader subMenuReader = subMenuCmd.ExecuteReader();
                DataTable subMenuDt = new DataTable();
                subMenuDt.Load(subMenuReader);

                using SqlCommand childSubMenuCmd = new SqlCommand(childSubMenuQuery, con);
                using SqlDataReader childSubMenuReader = childSubMenuCmd.ExecuteReader();
                DataTable childSubMenuDt = new DataTable();
                childSubMenuDt.Load(childSubMenuReader);

                // Close the data readers
                menuParentReader.Close();
                subMenuReader.Close();
                childSubMenuReader.Close();

                // Populate the model
                //var model = new SidebarMenuModel
                //{
                //    MenuParent = menuParentDt,
                //    SubMenu = subMenuDt,
                //    ChildSubMenu = childSubMenuDt,
                //    RptId = RptId
                //};

                var sidebarMenuModel = new SidebarMenuModel
                {
                    MenuParent = menuParentDt,
                    SubMenu = subMenuDt,
                    ChildSubMenu = childSubMenuDt,
                    ConnString = ConnString,
                    RptId = RptId
                };

                //var dataGridModel = new DataGridModel {
                //    RptId = RptId,

                //};

                // Parse the connection string to get dynamic configuration
                sidebarMenuModel.ParseConnectionString(ConnString);



                var combinedModel = new CombinedModel
                {
                    SidebarMenu = sidebarMenuModel,
                    //DataGrid = dataGridModel
                };

                ViewData["SubChildName"] = SubChildName;
                ViewData["RptId"] = RptId;
                // In your Index action method
                ViewData["DynamicConfig"] = sidebarMenuModel.GetDynamicConfig();

                //ViewData["ConnString"] = ConnString;

                GlobalSettings.RptId = RptId;






                return View(combinedModel);
            }
            catch (SqlException)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }


        }




        public IActionResult Home()
        {
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
