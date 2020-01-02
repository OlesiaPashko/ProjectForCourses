using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace Project.Models.Mappings
{
    public class ViewModelToDTOMappingProfile:Profile
    {
        public ViewModelToDTOMappingProfile()
        {
            CreateMap<RegistrationModel, User>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Login));
        }
    }
}
