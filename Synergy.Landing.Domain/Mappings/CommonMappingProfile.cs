using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Synergy.Common.Domain.Models.Common;
using Synergy.Landing.Models;

namespace Synergy.Landing.Domain.Mappings
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            this.CreateMap<Enum, FastEntityModel<int>>()
                .ForMember(x => x.Id, exp => exp.MapFrom(x => Convert.ToInt32(x, CultureInfo.InvariantCulture)))
                .ForMember(x => x.Name, exp => exp.MapFrom(x => GetDescription(x)));

            this.CreateMap<DAL.Queries.Entities.User, FastEntityModel<Guid>>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => $"{x.FirstName} {x.LastName}"));

            this.CreateMap<DAL.Queries.Entities.User, UserModel>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                .ForMember(x => x.IsActive, exp => exp.MapFrom(x => x.IsActive && x.DeletedOn == null));

            this.CreateMap<DAL.Queries.Entities.State, FastEntityModel<int>>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => x.Abbreviation));

            this.CreateMap<DAL.Queries.Entities.TaskType, FastEntityModel<int>>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => x.Name));

            this.CreateMap<DAL.Queries.Entities.TaskStatus, FastEntityModel<int>>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => x.Name));

            this.CreateMap<DAL.Queries.Entities.EventType, FastEntityModel<int>>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => x.Name));

            this.CreateMap<DAL.Queries.Entities.County, FastEntityModel<int>>()
               .ForMember(x => x.Name, exp => exp.MapFrom(x => x.Name));

            this.CreateMap<DAL.Queries.Entities.Department, FastEntityModel<int>>()
                .ForMember(x => x.Name, exp => exp.MapFrom(x => x.Name));
        }

        public static string GetDescription(Enum value)
        {
            return value.GetType().GetMember(value.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }
    }
}
