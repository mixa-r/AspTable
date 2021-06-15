using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTable.Controllers
{
    public class DataTableViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int Total { get; set; }
        public List<DataTableViewModel> DataTableList { get; set; }
        public DataTableViewModel()
        {
            DataTableList = new List<DataTableViewModel>();
        }
    }
}
