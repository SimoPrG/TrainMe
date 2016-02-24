namespace TrainMe.Web.Areas.Course.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using TrainMe.Data;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Areas.Course.InputModels.Course;
    using TrainMe.Web.Areas.Course.ViewModels.Category;
    using TrainMe.Web.Areas.Course.ViewModels.Course;
    using TrainMe.Web.Common;
    using TrainMe.Web.Controllers;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CourseController : BaseController
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private TrainMeDbContext db = new TrainMeDbContext();

        public CourseController(ICourseService courseService, IUserService userService, ICategoryService categoryService)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = this.courseService.GetById(id.Value);
            if (course == null)
            {
                return this.HttpNotFound();
            }

            this.HttpContext.Items[WebConstants.HttpRequestItemsCourseKey] = course;

            var courseDetailsViewModel = this.Mapper.Map<CourseDetailsViewModel>(course);

            return this.View(courseDetailsViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(CourseEnrollInputModel courseEnrollInputModel)
        {
            if (courseEnrollInputModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = this.courseService.GetById(courseEnrollInputModel.CourseId);
            if (course == null)
            {
                return this.HttpNotFound();
            }

            var user = this.userService.GetById(this.User.Identity.GetUserId());
            if (course.Author == user)
            {
                this.TempData[TempDataKeys.Warning] = "You cannot enroll for your own course.";
            }
            else if (course.Attendees.Contains(user))
            {
                this.TempData[TempDataKeys.Info] = "You are already enrolled for this course.";
            }
            else
            {
                course.Attendees.Add(user);
                this.courseService.Update();

                this.TempData[TempDataKeys.Success] = "You have successfully enrolled for this course.";
            }

            return this.RedirectToAction("Details", new { id = courseEnrollInputModel.CourseId });
        }

        [HttpGet]
        public ActionResult Create()
        {
            var courseInputModel = new CourseInputModel
            {
                AllCategories = this.categoryService.All().To<CategoryViewModel>().To<SelectListItem>().ToList()
            };

            return this.View(courseInputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseInputModel courseInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var course = this.Mapper.Map<Course>(courseInputModel);
                course.AuthorId = this.User.Identity.GetUserId();

                this.courseService.Add(course);

                return this.RedirectToAction("Details", new { Id = course.Id });
            }

            courseInputModel.AllCategories =
                this.categoryService.All().To<CategoryViewModel>().To<SelectListItem>().ToList();
            return this.View(courseInputModel);
        }

        // GET: Course/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", course.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // POST: Course/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,AuthorId,CategoryId,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", course.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // GET: Course/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}