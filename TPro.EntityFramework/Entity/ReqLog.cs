using System;

namespace TPro.EntityFramework.Entity
{
    public class ReqLog
    {
        public long Id { get; set; }
        public DateTime HandleTime { get; set; }
        public string ReqStr { get; set; }
        public string UserInfo { get; set; }
    }
}