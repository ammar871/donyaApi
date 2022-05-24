using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{


    public class CommandProfile : Profile
    {

        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CreateCategory, Category>();
            CreateMap<SubCategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();


            CreateMap<SubCategoryUpdateDto, SubCategory>();

            CreateMap<CreateAdds, Adds>();
            CreateMap<ReadAddsDto, Adds>();
            CreateMap<AddsUpdateDto, Adds>();

            CreateMap<CategoryProfessionUpdateDto, CategoryProfission>();

            CreateMap<ProfessionUpdate, Profession>();

            CreateMap<ProfessionalUpdate, professional>();

            //User
            CreateMap<UserForRegister, User>();

            CreateMap<AdminForRegister, User>();


            CreateMap<User, UserDetailResponse>();
            CreateMap<User, AdminDetailResponse>();
            CreateMap<UserForUpdate, User>();
            CreateMap<DriverForRegister, User>();

            CreateMap<DriverForRegister, Driver>();




        }

    }

}