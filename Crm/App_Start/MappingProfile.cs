using AutoMapper;
using Crm.Core.Dto;
using Crm.Core.Models;
using Crm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerCall, CustomerCallDto>();
            Mapper.CreateMap<Permission, PermissionDto>();
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<CustomerReport, Report>();


        }
    }
}