namespace TrainMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Course> GetAll()
        {
            return this.unitOfWork.Courses.Get();
        }

        public IEnumerable<Course> GetTop(int count)
        {
            // Just testing eager loading...
            Expression<Func<Course, object>> includeUsers = c => c.Author;

            return this.unitOfWork.Courses.Get(
                orderBy: q => q.OrderByDescending(sm => sm.CreatedOn),
                page: 1,
                pageSize: count,
                includeProperties: new[] { includeUsers });
        }
    }
}
