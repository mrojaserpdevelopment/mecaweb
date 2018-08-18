using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlarnaWebAPI.Config
{
    public static class DbConnection
    {
        public static String BuildConnectionString()
        {
            //Props config = new Props("Config.txt");
            Props config = new Props(Constants.CONFIG_FILE);
            String DataSource = config.get("datasource", "");
            String Database = config.get("initialcatalog", "");
            String UserId = config.get("userid", "");
            String Password = config.get("password", "");
            String IntegratedSecurity = config.get("integratedsecurity", "");

            // Build the connection string from the provided datasource and database
            String connString = @"data source=" + DataSource +
            ";initial catalog=" + Database +
            ";integrated security=" + IntegratedSecurity +
            ";user id=" + UserId +
            ";password=" + Password +
            ";MultipleActiveResultSets=True;App=EntityFramework;";

            return connString;
        }
    }
}