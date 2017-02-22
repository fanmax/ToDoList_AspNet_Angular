using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Infra.Data.Context;
using ToDoList.Infra.Data.Repositories;

namespace ToDoList.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly TodoListContext _context;

        private IAssigmentRepository _assignmentRepository;

        public UnitOfWork(TodoListContext context)
        {
            _context = context;
        }

        public IAssigmentRepository AssignmentRepository
        {
            get
            {
                if (_assignmentRepository == null)
                {
                    _assignmentRepository = new AssigmentRepository(_context);
                }

                return _assignmentRepository;


            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
