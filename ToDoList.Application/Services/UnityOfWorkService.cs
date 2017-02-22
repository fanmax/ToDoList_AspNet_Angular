using ToDoList.Application.Interfaces;
using ToDoList.Domain.Interfaces;
using ToDoList.Infra.Data.Context;
using ToDoList.Infra.Data.UoW;

namespace ToDoList.Application.Services
{
    public class UnityOfWorkService : IUnityOfWorkService
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork(new TodoListContext());
        }
    }
}
