using AutoMapper;

namespace TPro.Models.AutoMapperConfig
{
    /// <summary>
    /// 全局配置AutoMapper
    /// 如需使用在Startup.cs中注册
    /// services.AddAutoMapper(config => config.AddProfile<MapperConfig>());
    /// </summary>
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
        }
    }
}