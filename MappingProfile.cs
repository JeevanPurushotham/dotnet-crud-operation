namespace EmployeeAdminPortal
{
    using AutoMapper;
    using EmployeeAdminPortal.Models.Entities;
    using EmployeeAdminPortal.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping between DTO and entity
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<AddEmployeedto, Employee>();
        }
    }

}
