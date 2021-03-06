﻿namespace TrainMe.Web.Areas.Course.ViewModels.Category
{
    using System.Web.Mvc;
    using AutoMapper;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<CategoryViewModel, SelectListItem>()
                .ForMember(sli => sli.Value, opts => opts.MapFrom(cvm => cvm.Id.ToString()))
                .ForMember(sli => sli.Text, opts => opts.MapFrom(cvm => cvm.Name));
        }
    }
}