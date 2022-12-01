using AutoMapper;
using BusinessLayer.Mappings.AutoMapper;
using BusinessLayer.Services;
using BusinessLayer.ValidationRules;
using DataAccesLayer.Context;
using DataAccesLayer.UnitOfWork;
using DtosLayer.WorkDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
           
            services.AddDbContext<WorkContext>(opt =>
            {
                opt.UseSqlServer("server=DESKTOP-1TE9E4I;database=WorkDb;integrated security=true;");
            });
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });
            //bir mapper nesnesi alalım
            //bu mapperı dependeciy injection ile ele alalım
            //bu bize busines tarfında Imapper kullanabileceğiz.
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            //DI ile IWorkService ve  IUow gecebileceğiz
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();
            services.AddTransient<IValidator<WorkCreateDto>, CreateValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, UpdateValidator>();
        }
    }
}
