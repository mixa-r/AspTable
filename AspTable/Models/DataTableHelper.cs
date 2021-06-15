using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspTable.Models
{
    public class DataTableHelper
    {
        public class DtResult<T>
        {
            [JsonProperty("draw")]
            public int Draw { get; set; }
            [JsonProperty("recordsTotal")]
            public int RecordsTotal { get; set; }
            [JsonProperty("recordsFiltered")]
            public int RecordsFiltered { get; set; }
            [JsonProperty("data")]
            public IEnumerable<T> Data { get; set; }
            [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
            public string Error { get; set; }
            public string PartialView { get; set; }
        }

        public abstract class DtRow
        {
            public virtual string DtRowId => null;
            public virtual string DtRowClass => null;
            public virtual object DtRowData => null;
            public virtual object DtRowAttr => null;
        }
        public class DtParameters
        {
            public int Draw { get; set; }
            public DtColumn[] Columns { get; set; }
            public DtOrder[] Order { get; set; }
            public int Start { get; set; }
            public int Length { get; set; }
            public DtSearch Search { get; set; }
            public string SortOrder => Columns != null && Order != null && Order.Length > 0
                ? (Columns[Order[0].Column].Data +
                   (Order[0].Dir == DtOrderDir.Desc ? " " + Order[0].Dir : string.Empty))
                : null;
            public IEnumerable<string> AdditionalValues { get; set; }
        }

        public class DtColumn
        {
            public string Data { get; set; }
            public string Name { get; set; }
            public bool Searchable { get; set; }
            public bool Orderable { get; set; }
            public DtSearch Search { get; set; }
        }

        public class DtOrder
        {
            public int Column { get; set; }
            public DtOrderDir Dir { get; set; }
        }

        public enum DtOrderDir
        {
            Asc,
            Desc
        }

        public class DtSearch
        {
            public string Value { get; set; }
            public bool Regex { get; set; }
        }
    }
}
