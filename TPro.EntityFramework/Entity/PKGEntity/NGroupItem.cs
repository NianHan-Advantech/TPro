using System;
using System.Collections.Generic;

namespace TPro.EntityFramework.Entity.PKGEntity
{
    public class NGroupItem
    {
        public int Id { get; set; }

        #region Project Info

        public string ModelName { get; set; }
        public string PartNumber { get; set; }
        public string ProjectCode { get; set; }
        public int MNVer { get; set; }

        #endregion Project Info

        #region Label Info
        public int LabelId { get; set; }
        public string LabelKind { get; set; }
        public string BlankType { get; set; }
        public string Plant { get; set; }

        #endregion Label Info

        #region PKG Info

        public int RequestId { get; set; }
        public int RequestIdVer { get; set; }
        public string Guid { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public string ModelNo { get; set; }
        public string PartNo { get; set; }
        public int Ver { get; set; }
        public string MadeIn { get; set; }
        public int OldGroupId { get; set; }
        public int InfoType { get; set; }
        public string Description { get; set; }

        #endregion PKG Info

        #region Eflow Info

        public int EVer { get; set; }
        public int EflowNo { get; set; }
        public string Comment { get; set; }

        #endregion Eflow Info

        public virtual ICollection<NGFileItem> GFiles { get; set; }
        public virtual ICollection<NInOutPut> Inputs { get; set; }
        public virtual ICollection<NInOutPut> Outputs { get; set; }
    }
    public class NGHistory
    {
        public int Id { get; set; }
        public string FileIds { get; set; }
        public string Operation { get; set; }
        public string Reason { get; set; }
        public int OperatorId { get; set; }
        public int Ver { get; set; }
        public DateTime Time { get; set; }
        public int EflowNo { get; set; }
        public string PN { get; set; }
    }
    public class NGFileItem
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string Url { get; set; }
        public DateTime Time { get; set; }
    }
    public class NInOutPut
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string PutLeft { get; set; }
        public string PutCenter { get; set; }
        public string PutRight { get; set; }
    }
    public class NBarCode
    {
        public int Id { get; set; }

    }
}