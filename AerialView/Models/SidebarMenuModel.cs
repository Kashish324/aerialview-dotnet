using System.Data;

namespace AerialView.Models
{
    public class SidebarMenuModel
    {
        public (int MenuCode, string Icon)[] MenuIcons { get; set; }
        public DataTable MenuParent { get; set; }
        public DataTable SubMenu { get; set; }
        public DataTable ChildSubMenu { get; set; }
        public string ConnString { get; set; }
        public int RptId { get; set; }

        public string UserID { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public bool Encrypt { get; set; }
        public bool TrustServerCertificate { get; set; }
        public bool PersistSecurityInfo { get; set; }


        public SidebarMenuModel()
        {
            MenuIcons = new[]
            {
                (1, "ri-menu-2-line"),
                (2, "ri-file-chart-2-line"),
                (3, "ri-tools-line"),
                (4, "ri-settings-4-fill"),
                (5, "ri-dashboard-line"),
                (6, "ri-account-pin-box-line"),
                (7, "ri-terminal-window-line")
            };
        }

        public void ParseConnectionString(string connString)
        {
            Dictionary<string, string> dynamicConfig = new Dictionary<string, string>();

            if (connString != null)
            {
                dynamicConfig = connString.Split(';')
                                          .Select(part => part.Split('='))
                                          .Where(split => split.Length == 2)
                                          .ToDictionary(split => split[0].Trim(), split => split[1].Trim());
            }



            UserID = dynamicConfig.ContainsKey("User ID") ? dynamicConfig["User ID"] : "sa";
            Password = dynamicConfig.ContainsKey("Password") ? dynamicConfig["Password"] : "admin@123";
            Server = dynamicConfig.ContainsKey("Data Source") ? dynamicConfig["Data Source"] : "localhost";
            Database = dynamicConfig.ContainsKey("Initial Catalog") ? dynamicConfig["Initial Catalog"] : "";

            Encrypt = false;
            TrustServerCertificate = true;
            PersistSecurityInfo = dynamicConfig.ContainsKey("Persist Security Info") && dynamicConfig["Persist Security Info"].Equals("True", StringComparison.OrdinalIgnoreCase);
        }

        public dynamic GetDynamicConfig()
        {
            return new
            {
                user = UserID,
                password = Password,
                server = Server,
                database = Database,
                options = new
                {
                    encrypt = Encrypt,
                    trustServerCertificate = TrustServerCertificate,
                    persistSecurityInfo = PersistSecurityInfo
                }
            };
        }
    }
}


