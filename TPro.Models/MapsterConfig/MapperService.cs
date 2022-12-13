using Mapster;
using TPro.EntityFramework.Entity;
using TPro.Models.AuthDtos;

namespace TPro.Models.MapsterConfig
{
    public class MapperService : TypeAdapterConfig
    {
        public MapperService()
        {
            NewConfig<TPUser, AuthTPUser>();
        }
    }
}