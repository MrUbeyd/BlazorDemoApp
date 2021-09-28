using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BlazorDemoApp
{
    public class SqlCommands
    {
        
       
        public SqlConnection connection()
        {
            
            SqlConnection connect = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=BlazorDemoDB;Integrated Security=True");
            connect.Open();
            return connect;
        }

    }
}
