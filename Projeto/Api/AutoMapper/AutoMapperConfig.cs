using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }

}
