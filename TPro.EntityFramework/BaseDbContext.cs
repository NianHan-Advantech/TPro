using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using TPro.Common.Extentions;
using System.Data;

namespace TPro.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public List<object> GetBySql(Type entitytype, string sql)
        {
            var connection = base.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            using (var command = connection.CreateCommand())
            {
                var list = new List<object>();
                command.CommandText = sql;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = reader.MapDataToObj(entitytype);
                        list.Add(entity);
                    }
                }
                return list;
            }
        }
    }
}