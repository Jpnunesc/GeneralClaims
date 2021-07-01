using AutoMapper;
using Business.IO.Herois;
using Business.IO.Users;
using Domain.Entity;
using Domain.Entitys;

namespace Business.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FavoritosEntity,FavoritosViewModel>(MemberList.None);
            CreateMap<FavoritosViewModel, FavoritosEntity>(MemberList.None);

            CreateMap<UserEntity, UserAuthView>(MemberList.None);
            CreateMap<UserAuthView, UserEntity>(MemberList.None);
            CreateMap<UsuarioView, UserEntity>(MemberList.None);
            CreateMap<UserEntity, UsuarioView>(MemberList.None);

        }
    }

}