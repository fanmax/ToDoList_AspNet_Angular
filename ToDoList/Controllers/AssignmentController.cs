using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToDoList.Models;


namespace ToDoList.Controllers
{
    [Authorize]
    public class AssignmentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Assignment>> List()
        {
            string userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            return db.Assignments.Where(a => a.UsuarioId == userId).ToArray();
        }

        //public HttpResponseMessage Add2(AssignmentViewModel model)
        //{
        //    var modelStateErrors = ModelState.Values.ToList();

        //    List<string> errors = new List<string>();

        //    foreach (var s in modelStateErrors)
        //    {
        //        foreach (var e in s.Errors)
        //            if(e.ErrorMessage != null && e.ErrorMessage.Trim() != "")
        //            {
        //                errors.Add(e.ErrorMessage);
        //            }
        //    }

        //    if (errors.Count == 0)
        //    {
        //        try
        //        {

        //            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/");

        //            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;



        //            string userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

        //            var currentUser = UserManager.FindById(userId);

        //            var assignment = new Assignment() {
        //                Name = model.Name,
        //                UsuarioId = currentUser.Id,
        //                DateStart = DateTime.Now                    
        //            };

        //            for (int i = 0; i < files.Count; i++)
        //            {
        //                System.Web.HttpPostedFile file = files[i];

        //                string fileName = new FileInfo(file.FileName).Name;

        //                Guid id = new Guid();

        //                string modifiedFileName = currentUser.Id + '_' + id.ToString() + '_' + fileName;

        //                if (!File.Exists(sPath + Path.GetFileName(modifiedFileName)))
        //                {
        //                    file.SaveAs(sPath + Path.GetFileName(modifiedFileName));
        //                    assignment.Attachment = "/Uploads/" + modifiedFileName;
        //                }

        //            }

        //            db.Assignments.Add(assignment);
        //            db.SaveChangesAsync();

        //            return Request.CreateResponse(HttpStatusCode.Accepted);
        //        }
        //        catch
        //        {
        //            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        //        }
        //    }
        //    else
        //    {
        //        return Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, errors);
        //    }         

        //}

        [HttpPost]
        public async Task<HttpResponseMessage> Add()
        {

            try
            {

                string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/");

                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                string userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

                var currentUser = UserManager.FindById(userId);

                var assignment = new Assignment()
                {
                    Name = System.Web.HttpContext.Current.Request.Form["Name"],
                    UsuarioId = currentUser.Id,
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
                        assignment.Attachment = "/Uploads/" + modifiedFileName;
                    }

                }

                db.Assignments.Add(assignment);
                await db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.Accepted);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut]
        public async Task<HttpResponseMessage> Edit(int id)
        {

            try
            {

                string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/");

                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                string userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

                var currentUser = UserManager.FindById(userId);

                var assignment = db.Assignments.Where(a => a.Id == id && a.UsuarioId == currentUser.Id).FirstOrDefault();

                assignment.Name = System.Web.HttpContext.Current.Request.Form["Name"];


                for (int i = 0; i < files.Count; i++)
                {
                    System.Web.HttpPostedFile file = files[i];

                    string fileName = new FileInfo(file.FileName).Name;

                    Guid _id = new Guid();

                    string modifiedFileName = currentUser.Id + '_' + _id.ToString() + '_' + fileName;

                    if (!File.Exists(sPath + Path.GetFileName(modifiedFileName)))
                    {
                        file.SaveAs(sPath + Path.GetFileName(modifiedFileName));
                        assignment.Attachment = "/Uploads/" + modifiedFileName;
                    }

                }

                db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.Accepted);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut]
        public async Task<HttpResponseMessage> Complete(int id)
        {

            try
            {
                Assignment assignment = db.Assignments.Where(a => a.Id == id).FirstOrDefault();

                if (assignment.DateEnd == null)
                    assignment.DateEnd = DateTime.Now;
                else
                    assignment.DateEnd = null;

                db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;

                await db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Remove(int id)
        {
            try
            {
                Assignment assignment = db.Assignments.Where(a => a.Id == id).FirstOrDefault();

                db.Entry(assignment).State = System.Data.Entity.EntityState.Deleted;

                await db.SaveChangesAsync();

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
