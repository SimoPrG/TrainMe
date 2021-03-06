﻿namespace TrainMe.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using TrainMe.Services.Web;

    public abstract class BaseController : Controller
    {
        public ISanitizer Sanitizer { get; set; }

        public ICacheService Cache { get; set; }

        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();
    }
}
