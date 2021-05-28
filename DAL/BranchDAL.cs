using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Web_S10203108.Models;

namespace Web_S10203108.DAL
{
    public class BranchDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;
        //Constructor
        public BranchDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("NPBookConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public List<Branch> GetAllBranches()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT * FROM Branch";
            
            //Open a database connection
            conn.Open();
            
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            
            //Read all records until the end, save data into a branch list
            List<Branch> branchList = new List<Branch>();
            
            while (reader.Read())
            {
                branchList.Add(
                    new Branch
                    {
                        BranchNo = reader.GetInt32(0), // 0 - 1st column
                        Address = reader.GetString(1), // 1 - 2nd column
                        Telephone = reader.GetString(2), // 2 - 3rd column
                    }
                );
            }
            
            //Close DataReader
            reader.Close();
            
            //Close the database connection
            conn.Close();
            
            return branchList;
        }
    }
}