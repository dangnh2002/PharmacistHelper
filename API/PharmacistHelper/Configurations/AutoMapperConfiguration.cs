using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacistHelper.Configurations
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                //config.CreateMap<Activity, ProjectListResponse>().ReverseMap();
            });
        }
    }
}
