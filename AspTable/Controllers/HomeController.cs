using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AspTable.Models;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using static AspTable.Models.DataTableHelper;
using Microsoft.Extensions.Logging;

namespace AspTable.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult _GetList([FromBody] DtParameters param)
        {
            var data = new DataTableViewModel();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "[dbo].[Paging.GetList]";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SearchVal", param.Search.Value);
                    cmd.Parameters.AddWithValue("Page", param.Start);
                    cmd.Parameters.AddWithValue("OrderBy", param.SortOrder);
                    cmd.Parameters.AddWithValue("PageSize", param.Length);
                    cmd.CommandTimeout = 120;
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        DataTableViewModel model = new DataTableViewModel();
                        model.Email = item["Email"].ToString();
                        model.FirstName = item["FirstName"].ToString();
                        model.LastName = item["LastName"].ToString();
                        model.Country = item["Country"].ToString();
                        data.DataTableList.Add(model);
                    }
                    data.Total = Convert.ToInt32(ds.Tables[1].Rows[0].ItemArray[0]);
                }
                con.Close();
            }
            var helper = new DtResult<DataTableViewModel>()
            {
                Draw = param.Draw,
                Data = data.DataTableList.ToList(),
                RecordsFiltered = data.Total,
                RecordsTotal = data.Total
            };
            return Json(helper);
        }
    }
}
