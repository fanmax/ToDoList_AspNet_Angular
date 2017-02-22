using AutoMapper;
using ToDoList.Application.ViewModels;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Assignment, AssignmentViewModel>();
        }   
    }
}
