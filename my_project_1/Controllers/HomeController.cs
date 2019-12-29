using Microsoft.AspNet.Identity;
using my_project_1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace my_project_1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.categories.ToList();
            return View(list);
        }

        public ActionResult Detiles(int JobId)
        {
            var Job = db.Jobs.Find(JobId);

            if (Job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;

            return View(Job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();

            
            var jobid = (int)Session["JobId"];

            var check = db.Applyforjobs.Where(a => a.JobId == jobid && a.UserId == UserId).ToList();

            if (check.Count<1)
            {
                var Job = new Applyforjob();

                Job.UserId = UserId;
                Job.JobId = jobid;
                Job.Massage = Message;
                Job.ApplyDate = DateTime.Now;

                db.Applyforjobs.Add(Job);
                db.SaveChanges();
                ViewBag.Result = "تمت الاضافة بنجاح";
            }
            else
            {
                ViewBag.Result = "لقد سبق وتقدمت لنقس الوظيفة";

            }
            
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult GetJobUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.Applyforjobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsForJob(int id)
        {
            var Job = db.Applyforjobs.Find(id);

            if (Job == null)
            {
                return HttpNotFound();
            }
           

            return View(Job);
        }
        [Authorize]
        public ActionResult GetJobByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = from app in db.Applyforjobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserId
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.Jobtital
                          into gro
                          select new JobViewModel
                          {
                              JobTitle = gro.Key,
                              Items = gro
                              

                          };
            return View(grouped.ToList());
        }


        public ActionResult Edit(int id)
        {
            var job = db.Applyforjobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult Edit(Applyforjob job)
        {

            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobUser");
            }
            return View(job);
        }
      

        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(string searchName)
        {
            var result = db.Jobs.Where(a => a.Jobtital.Contains(searchName)

           || a.Jobcontent.Contains(searchName)
           || a.category.categoryName.Contains(searchName)
           || a.category.categoryDescription.Contains(searchName)).ToList();
            return View(result);

        }
        public ActionResult Delete(int id)
        {
            var job = db.Applyforjobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(Applyforjob job)
        {
            try
            {
                // TODO: Add delete logic here
                var myjob = db.Applyforjobs.Find(job.Id);
                db.Applyforjobs.Remove(myjob);
                db.SaveChanges();

                return RedirectToAction("GetJobUser");
            }
            catch
            {
                return View();
            }
        }
            [HttpGet]
        public ActionResult Contact()
        {
          

            return View();
        }
        [HttpPost]
        public ActionResult Contact(contact contact)
        {
            var mail = new MailMessage();
            var Lgininfo = new NetworkCredential("ahmedelfnaaaan22@gmail.com", "01098615586");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("ahmedelfnaaaan22@gmail.com"));
            mail.Subject = contact.subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل:" + contact.Name + "<br>" +
                "بريد المرسل:" + contact.Email + "<br>" +
                "عنوان الرسالة:" + contact.subject + "<br>" +
                "نص الرسالة:<b>" + contact.Massage;

            mail.Body = body;
            var smtpclint = new SmtpClient("smt.gmail.com", 587);
            smtpclint.EnableSsl = true;
            smtpclint.Credentials = Lgininfo;
            smtpclint.Send(mail);

            return RedirectToAction("Index");
        }
    }
}