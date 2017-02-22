﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IAssigmentRepository : IRepository<Assignment>
    {
        IEnumerable<Assignment> GetAll(string userId);
    }
}
