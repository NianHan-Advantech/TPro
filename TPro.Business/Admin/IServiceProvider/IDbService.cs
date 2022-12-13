﻿using TPro.Models.ResponseDtos;

namespace TPro.Business.Admin.IServiceProvider
{
    public interface IDbService
    {
        ResponseModel GetAllTableNames();

        ResponseModel GetTableInfo(string fullname);
    }
}