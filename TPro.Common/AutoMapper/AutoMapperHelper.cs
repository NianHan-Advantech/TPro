using AutoMapper;
using System.Collections.Generic;

namespace TPro.Common.AutoMapper
{
    public static class AutoMapperHelper
    {
        public static TDest AutoMapTo<TSrc, TDest>(this TSrc src) where TDest : class, new()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSrc, TDest>();
            }).CreateMapper();
            var res = mapper.Map<TDest>(src);
            return res ?? new TDest();
        }

        public static TDest AutoMapTo<TSrc, TDest>(this TSrc src, TDest dest) where TDest : class, new()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSrc, TDest>();
            }).CreateMapper();
            dest = mapper.Map<TDest>(src);
            return dest ?? new TDest();
        }

        public static List<TDest> AutoMapTo<TSrc, TDest>(this List<TSrc> srcs)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSrc, TDest>();
            }).CreateMapper();
            var dests = mapper.Map<List<TDest>>(srcs);
            return dests;
        }
    }
}