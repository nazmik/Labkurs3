using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Labkurs3.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Labkurs3.Controllers
{
   
        
            [Route("api/[controller]")]
            [ApiController]
        public class KompaniaProdhueseController : ControllerBase
        {
            private readonly IConfiguration _configuration;
            

            public KompaniaProdhueseController(IConfiguration configuration)
            {
                _configuration = configuration;
                
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                    select KompaniaID, EmriKompanis, NumriBiznesit from dbo.KompaniaProdhuese";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("Labkurs3AppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult(table);
            }

            [HttpPost]
            public JsonResult Post(KompaniaProdhuese kmp)
            {
                string query = @"
                    insert into dbo.KompaniaProdhuese values
                    ('" + kmp.EmriKompanis + @"'
                    ,'" + kmp.NumriBiznesit + @"'
                    )
                    ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("Labkurs3AppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
                return new JsonResult("Added Succesfully");
            }
            [HttpPut]
            public JsonResult Put(KompaniaProdhuese kmp)
            {
                string query = @"
                    update dbo.KompaniaProdhuese set 
                    EmriKompanis = '" + kmp.EmriKompanis + @"'
                    ,NumriBiznesit = '" + kmp.NumriBiznesit + @"'
                    where KompaniaID = " + kmp.KompaniaID + @"
                    ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("Labkurs3AppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
                return new JsonResult("Updated Succesfully");
            }

            [HttpDelete("{id}")]
            public JsonResult Delete(int id)
            {
                string query = @"
                    delete from dbo.KompaniaProdhuese 
                    where KompaniaID = " + id + @"
                    ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("Labkurs3AppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
                return new JsonResult("Deleted Succesfully");
            }
        }
    }
    
