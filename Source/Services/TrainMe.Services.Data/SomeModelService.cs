namespace TrainMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;

    public class SomeModelService : ISomeModelService
    {
        private readonly IUnitOfWork unitOfWork;

        public SomeModelService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<SomeModel> GetAll()
        {
            return this.unitOfWork.SomeModels.Get();
        }

        public IEnumerable<SomeModel> GetTop(int count)
        {
            // Just testing eager loading...
            Expression<Func<SomeModel, object>> includeUsers = sm => sm.User;

            return this.unitOfWork.SomeModels.Get(
                orderBy: q => q.OrderByDescending(sm => sm.CreatedOn),
                page: 1,
                pageSize: count,
                includeProperties: new[] { includeUsers });
        }
    }
}
