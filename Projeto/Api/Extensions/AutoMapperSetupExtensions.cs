using AutoMapper;
using Business.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Extensions
{

    public static class AddAutoMapperSetupExtensions
    {
 
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            AutoMapperConfig.RegisterMappings();
        }
    }
}
