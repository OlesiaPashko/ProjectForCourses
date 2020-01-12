using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Contracts.V1.Requests;

namespace Project.Models.Mappings
{
    public class ViewModelToDTOMappingProfile:Profile
    {
        public ViewModelToDTOMappingProfile()
        {
            CreateMap<UserModel, User>().ForMember(au => au.FirstName, map => map.MapFrom(vm => vm.FirstName));
        }
    }
}
