using System;
using System.Collections.Generic;
using ToDoList.Application.ViewModels;

namespace ToDoList.Application.Interfaces
{
    public interface IAssignmentService : IDisposable
    {
        void Add(AssignmentViewModel obj);

        AssignmentViewModel GetById(int id);

        IEnumerable<AssignmentViewModel> GetAll(string userId);

        void Update(AssignmentViewModel obj);

        void Remove(int id);
    }
}
