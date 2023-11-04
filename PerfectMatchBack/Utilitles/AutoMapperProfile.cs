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
            CreateMap<CityDTO, City>().
                ForMember(destiny => destiny.IdDepartmentNavigation, origin => origin.Ignore());
            CreateMap<City, CityDTO>().
                ForMember(destiny => destiny.DepartmentName, origin => origin.MapFrom(
                    dest => dest.IdDepartmentNavigation.DepartmentName));
            #endregion
            #region Image
            CreateMap<ImageDTO, Image>().ReverseMap();
            #endregion
            #region Publication
            CreateMap<PublicationDTO, Publication>().
             ForMember(destiny => destiny.IdAnimalTypeNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdBreedNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdCityNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdOwnerNavigation, origin => origin.Ignore()).
             ForMember(destiny => destiny.IdGenderNavigation, origin => origin.Ignore()).
             ForMember(destity => destity.Images, oring => oring.Ignore());
            CreateMap<Publication, PublicationDTO>().
                ForMember(destiny => destiny.TypeName, origin => origin.MapFrom(dest => dest.IdAnimalTypeNavigation.AnimalTypeName)).
                ForMember(destiny => destiny.CityName, origin => origin.MapFrom(dest => dest.IdCityNavigation.CityName)).
                ForMember(destiny => destiny.BreedName, origin => origin.MapFrom(dest => dest.IdBreedNavigation.BreedName)).
                ForMember(destiny => destiny.NameOwner, origin => origin.MapFrom(dest => dest.IdOwnerNavigation.Name)).
                ForMember(destiny => destiny.GenderName, origin => origin.MapFrom(dest => dest.IdGenderNavigation.GenderName));
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
                   ForMember(destiny => destiny.password, origin => origin.MapFrom(dest => Encryption.Decrypt(dest.IdAccessNavigation.Password)));
            #endregion
            #region Gender

            CreateMap<GenderDTO, Gender>()
                .ReverseMap();
            #endregion
            #region Notification
            CreateMap<NotificationDTO, Notification>().
                ForMember(destiny => destiny.IdMovementNavigation, origin => origin.Ignore()).
                ForMember(destiny => destiny.IdUserNavigation, origin => origin.Ignore()).
                ForMember(destiny => destiny.IdMovementNavigation, origin => origin.Ignore());
            CreateMap<Notification, NotificationDTO>().
                ForMember(destiny => destiny.NameUserFK, origin => origin.MapFrom(dest => dest.IdUserFKNavigation.Name)).
                ForMember(destiny => destiny.NameUser, origin => origin.MapFrom(dest => dest.IdUserNavigation.Name)).
                ForMember(destiny => destiny.Description, origin => origin.MapFrom(dest => dest.IdPublicationNavigation.Description)).
                ForMember(destiny => destiny.NamePublication, origin => origin.MapFrom(dest => dest.IdMovementNavigation.IdPublicationNavigation.AnimalName)).
                ForMember(destiny => destiny.NamePublication1, origin => origin.MapFrom(dest => dest.IdPublicationNavigation.AnimalName)).
                ForMember(destiny => destiny.Description1, origin => origin.MapFrom(dest => dest.IdPublicationNavigation.Description)).
                ForMember(destiny => destiny.ImagePublication, origin => origin.MapFrom(dest => dest.IdPublicationNavigation.Images.FirstOrDefault().DataImage)).
                ForMember(destiny => destiny.Date, origin => origin.MapFrom(
                dest => dest.IdMovementNavigation.Date.ToString("dd/MM/yyyy")));
            #endregion
            #region comment
            CreateMap<CommentDTO, Comment>().
                ForMember(destiny => destiny.IdPublicationNavigation, origin => origin.Ignore()).
                ForMember(destiny => destiny.IdUserNavigation, origin => origin.Ignore()).
                ForMember(destiny => destiny.InverseIdCommentFkNavigation, origin => origin.Ignore()).
                ForMember(destiny => destiny.IdCommentFkNavigation, origin => origin.Ignore());
            CreateMap<Comment, CommentDTO>().
                ForMember(destiny => destiny.NameOwnerComment, origin => origin.MapFrom(dest => dest.IdCommentFkNavigation.IdUserNavigation.Name)).
                ForMember(destiny => destiny.NameUser, origin => origin.MapFrom(dest => dest.IdUserNavigation.Name)).
                ForMember(destiny => destiny.NameOwnerPublication, origin => origin.MapFrom(dest => dest.IdPublicationNavigation.IdOwnerNavigation.Name));

            #endregion
            #region Movement

            CreateMap<MovementDTO, Movement>()
                .ReverseMap();
            #endregion

        }
    }
}
