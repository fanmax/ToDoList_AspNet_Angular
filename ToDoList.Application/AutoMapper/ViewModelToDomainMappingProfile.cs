using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.ViewModels;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AssignmentViewModel, Assignment>();
        }
    }
}
