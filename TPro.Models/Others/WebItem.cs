using System.Collections.Generic;

namespace TPro.Models.Others
{
    public class WebItem
    {
        public dynamic key { get; set; }
        public dynamic value { get; set; }
        public dynamic label { get; set; }
        public virtual List<WebItem> children { get; set; }
    }

    public class ElTableColumn
    {
        public string prop { get; set; }
        public string label { get; set; }
        public string width { get; set; }
        public virtual List<ElTableColumn> children { get; set; }
    }
}