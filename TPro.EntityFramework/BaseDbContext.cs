using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TPro.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public List<object> GetBySql(Type entitytype, string sql)
        {
            var connection = base.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
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
                        try
                        {
                            var entity = entitytype.Assembly.CreateInstance(entitytype.FullName);
                            var properties = entitytype.GetProperties();
                            foreach (var item in properties)
                            {
                                try
                                {
                                    var value = reader[item.Name];
                                    if (item.PropertyType.Name.Contains("String"))
                                        value = value.ToString();
                                    item.SetValue(entity, value);
                                }
                                catch (Exception ex)
                                {
                                    if (ex is ArgumentException)
                                        continue;
                                    else if (ex is ArgumentOutOfRangeException)
                                        continue;
                                    throw;
                                }
                            }
                            list.Add(entity);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                return list;
            }
        }
    }
}