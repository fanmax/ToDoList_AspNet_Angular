using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repositories
{
    public class AssigmentRepository : Repository<Assignment>, IAssigmentRepository
    {
        public AssigmentRepository(TodoListContext context) 
            : base(context)
        {
        }

        public IEnumerable<Assignment> GetAll(string userId)
        {
            if(userId != null)
                return DbSet.Where(a => a.UserId == userId).ToList();

            return DbSet.ToList();
        }
    }
}
