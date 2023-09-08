using AutoMapper;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using System.Globalization;

namespace PerfectMatchBack.Utilitles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region AccessDTO
            CreateMap<AccessDTO, Access>().ReverseMap();
            #endregion
            #region  AnimalType
            CreateMap<AnimalTypeDTO, AnimalType>().ReverseMap();
            #endregion
            #region BreedDTO 
            CreateMap<BreedDTO, Breed>().
                ForMember(destiny => destiny.IdAnimalTypeNavigation,
                origen => origen.Ignore());
            CreateMap<Breed, BreedDTO>().
                ForMember(destiny => destiny.NameType,
                origen => origen.MapFrom(dest => dest.IdAnimalTypeNavigation.AnimalTypeName));
            #endregion
            #region City
            CreateMap<CityDTO, City>().ReverseMap();
            #endregion
            #region Image
            CreateMap<ImageDTO, Image>().
                ForMember(destiny => destiny.IdPublicationNavigation, origin => origin.Ignore()).ReverseMap();
           
                
                
            #endregion
            #region Publication
            CreateMap<PublicationDTO, Publication>().
             ForMember(destiny => destiny.IdAnimalTypeNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdBreedNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdCityNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdOwnerNavigation, origin => origin.Ignore());
            CreateMap<Publication, PublicationDTO>().
                ForMember(destiny => destiny.NameType, origin => origin.MapFrom(dest => dest.IdAnimalTypeNavigation.AnimalTypeName)).
                ForMember(destiny => destiny.NameCity, origin => origin.MapFrom(dest => dest.IdCityNavigation.CityName)).
                ForMember(destiny => destiny.NameBreed, origin => origin.MapFrom(dest => dest.IdBreedNavigation.BreedName)).
                ForMember(destiny => destiny.NameOwner, origin => origin.MapFrom(dest => dest.IdOwnerNavigation.Name));
            #endregion
            #region Role
            CreateMap<RoleDTO, Role>().ReverseMap();
            #endregion
            #region User
            CreateMap<UserDTO, User>().
            ForMember(destiny => destiny.IdAccessNavigation, origin => origin.Ignore()).
            ForMember(destiny => destiny.IdCityNavigation, origin => origin.Ignore()).
            ForMember(destiny => destiny.IdRoleNavigation, origin => origin.Ignore()).
            ForMember(destiny => destiny.BirthDate, origin => origin.MapFrom(
                dest => DateTime.ParseExact(dest.BirthDate,"dd/MM/yyyy",CultureInfo.InvariantCulture)));
            CreateMap<User, UserDTO>().
                ForMember(destiny => destiny.NameRole, origin => origin.MapFrom(dest => dest.IdRoleNavigation.RoleName)).
                ForMember(destiny => destiny.NameCity, origin => origin.MapFrom(dest => dest.IdCityNavigation.CityName)).
                ForMember(destiny => destiny.BirthDate, origin => origin.MapFrom(
                    dest => dest.BirthDate.Value.ToString("dd/MM/yyyy"))).
                    ForMember(destiny => destiny.Password, origin => origin.Ignore());
            #endregion

        }
    }
}
