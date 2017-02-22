using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Interfaces
{
    public interface IUnityOfWorkService
    {
        IUnitOfWork Create();
    }
}
