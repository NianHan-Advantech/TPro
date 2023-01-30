using System.Collections.Generic;
using TPro.Models.Admin.DbDtos;
using TPro.Models.Others;
using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.IServiceProvider
{
    public interface IDbService
    {
        ResponseModel GetAllTableNames();

        ResponseModel GetTableInfo(string fullname);

        ResponseModel GetTableDatasByType(string entityname, string tablename);
        ResponseModel SaveEntityInfo(string entityname, List<EntityPropertyDto> entityProperties);
    }
}