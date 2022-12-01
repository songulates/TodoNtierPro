using AutoMapper;
using DtosLayer.WorkDtos;
using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappings.AutoMapper
{
    public class WorkProfile:Profile
    {
        public WorkProfile()
        {
            CreateMap<Work, WorkListDto>().ReverseMap();
            CreateMap<Work, WorkCreateDto>().ReverseMap();
            CreateMap<Work, WorkUpdateDto>().ReverseMap();
            //worklisti workupdate ceviren mapper yazalım.
            CreateMap<WorkListDto, WorkUpdateDto>().ReverseMap();
        }
    }
}
