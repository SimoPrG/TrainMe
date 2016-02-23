namespace TrainMe.Web.Areas.Course.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using TrainMe.Data;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Areas.Course.ViewModels;
    using TrainMe.Web.Controllers;
    using TrainMe.Web.Infrastructure.Common;
    using TrainMe.Web.Infrastructure.Mapping;

    public class HomeController : BaseController
    {
        private readonly ICourseService courseService;
        private TrainMeDbContext db = new TrainMeDbContext();

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [ValidateInput(false)]
        public ActionResult Index(string querry, string orderBy, int page = 1)
        {
            const int PageSize = 10;

            int totalCoursesCount = this.courseService.CountCourses(querry);
            int totalPages = (totalCoursesCount + PageSize - 1) / PageSize;

            page = Validator.ValidatePage(page, totalPages);

            var courseViewModels =
                this.courseService.GetAll(querry, orderBy, page, PageSize).To<CourseViewModel>().ToList();

            var indexViewModel = new IndexViewModel
            {
                CourseViewModels = courseViewModels,
                Querry = querry,
                OrderBy = orderBy,
                OrderByNameParam = string.IsNullOrEmpty(orderBy) ? QuerryStrings.CourseNameDesc : string.Empty,
                OrderByCategoryParam = orderBy == QuerryStrings.CourseCategoryName ?
                    QuerryStrings.CourseCategoryNameDesc : QuerryStrings.CourseCategoryName,
                OrderByAuthorParam = orderBy == QuerryStrings.CourseAuthorName ?
                    QuerryStrings.CourseAuthorNameDesc : QuerryStrings.CourseAuthorName,
                Page = page,
                TotalPages = totalPages
            };

            return this.View(indexViewModel);
        }

        // GET: Course/Courses/Details/5
        public ActionResult Details(int? id)
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

        // GET: Course/Courses/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Course/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,AuthorId,CategoryId,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", course.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", course.CategoryId);
            return View(course);
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
