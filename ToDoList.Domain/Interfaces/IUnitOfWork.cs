using System;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssigmentRepository AssignmentRepository { get; }

        void Commit();
    }
}
