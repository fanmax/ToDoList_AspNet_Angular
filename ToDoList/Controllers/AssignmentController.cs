using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ViewModels;


namespace ToDoList.Controllers
{
    [Authorize]
    public class AssignmentController : ApiController
    {
        private readonly IAssignmentService _assignmmentService;

        public AssignmentController(IAssignmentService assignmmentService)
        {
            _assignmmentService = assignmmentService;
        }

        [HttpGet]
        public IEnumerable<AssignmentViewModel> List()
        {
            string userId = User.Identity.GetUserId();

            return _assignmmentService.GetAll(userId);
        }        

        [HttpPost]
        public HttpResponseMessage Add()
        {

            try
            {

                string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/");

                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                string userId = User.Identity.GetUserId();

                var assignmentViewModel = new AssignmentViewModel()
                {
                    Name = System.Web.HttpContext.Current.Request.Form["Name"],
                    UserId = userId,
                    DateStart = DateTime.Now
                };

                for (int i = 0; i < files.Count; i++)
                {
                    System.Web.HttpPostedFile file = files[i];

                    string fileName = new FileInfo(file.FileName).Name;

                    Guid id = Guid.NewGuid();

                    string modifiedFileName = id.ToString() + '_' + fileName;

                    if (!File.Exists(sPath + Path.GetFileName(modifiedFileName)))
                    {
                        file.SaveAs(sPath + Path.GetFileName(modifiedFileName));
                        assignmentViewModel.Attachment = "/Uploads/" + modifiedFileName;
                    }

                }                

                _assignmmentService.Add(assignmentViewModel);                

                return Request.CreateResponse(HttpStatusCode.Accepted);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut]
        public HttpResponseMessage Edit(int id)
        {

            try
            {

                string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/");

                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                string userId = User.Identity.GetUserId();

                var assignment = _assignmmentService.GetById(id);

                assignment.Name = System.Web.HttpContext.Current.Request.Form["Name"];


                for (int i = 0; i < files.Count; i++)
                {
                    System.Web.HttpPostedFile file = files[i];

                    string fileName = new FileInfo(file.FileName).Name;

                    Guid _id = new Guid();

                    string modifiedFileName = userId + '_' + _id.ToString() + '_' + fileName;

                    if (!File.Exists(sPath + Path.GetFileName(modifiedFileName)))
                    {
                        file.SaveAs(sPath + Path.GetFileName(modifiedFileName));
                        assignment.Attachment = "/Uploads/" + modifiedFileName;
                    }

                }

                if(assignment.UserId == userId)
                    _assignmmentService.Update(assignment);

                return Request.CreateResponse(HttpStatusCode.Accepted);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut]
        public HttpResponseMessage Complete(int id)
        {

            try
            {
                var assignment = _assignmmentService.GetById(id);

                if (assignment.DateEnd == null)
                    assignment.DateEnd = DateTime.Now;
                else
                    assignment.DateEnd = null;

                _assignmmentService.Update(assignment);

                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }


        }

        [HttpDelete]
        public HttpResponseMessage Remove(int id)
        {
            try
            {
                var assignment = _assignmmentService.GetById(id);
                _assignmmentService.Remove(id);                

                string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/");

                if (File.Exists(sPath + Path.GetFileName(assignment.Attachment)))
                {
                    File.Delete(sPath + Path.GetFileName(assignment.Attachment));
                }

                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
