using AutoMapper;
using System;
using System.Collections.Generic;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ViewModels;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly UnityOfWorkService _unityOfWork;

        public AssignmentService()
        {           
            _unityOfWork = new UnityOfWorkService();
        }

        public void Add(AssignmentViewModel obj)
        {

            var unityOfWork = _unityOfWork.Create();

            var assigmentRepository = unityOfWork.AssignmentRepository;
            var assignment = Mapper.Map<AssignmentViewModel, Assignment>(obj);
            assigmentRepository.Add(assignment);

            unityOfWork.Commit();
        
        }

        public IEnumerable<AssignmentViewModel> GetAll(string userId)
        {
            var unityOfWork = _unityOfWork.Create();

            var assigmentRepository = unityOfWork.AssignmentRepository;

            var assignment = assigmentRepository.GetAll(userId);

            return Mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentViewModel>>(assignment);
        }

        public AssignmentViewModel GetById(int id)
        {
            var unityOfWork = _unityOfWork.Create();

            var assigmentRepository = unityOfWork.AssignmentRepository;
                        
            var assignment = assigmentRepository.GetById(id);            

            return Mapper.Map<Assignment, AssignmentViewModel>(assignment);
        }

        public void Remove(int id)
        {
            var unityOfWork = _unityOfWork.Create();

            var assigmentRepository = unityOfWork.AssignmentRepository;
            assigmentRepository.Remove(id);

            unityOfWork.Commit();
        }

        public void Update(AssignmentViewModel obj)
        {
            var unityOfWork = _unityOfWork.Create();

            var assigmentRepository = unityOfWork.AssignmentRepository;
            var assignment = Mapper.Map<AssignmentViewModel, Assignment>(obj);
            assigmentRepository.Update(assignment);

            unityOfWork.Commit();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
