using System.Collections.Generic;

namespace TPro.Models.Others
{
    public class ElTableColumn
    {
        public string prop { get; set; }
        public string label { get; set; }
        public string width { get; set; }
        public virtual List<ElTableColumn> children { get; set; }
    }
}