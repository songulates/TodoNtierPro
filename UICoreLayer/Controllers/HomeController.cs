using AutoMapper;
using BusinessLayer.Services;
using DtosLayer.WorkDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UICoreLayer.Extensions;
using UICoreLayer.Models;

namespace UICoreLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workservice;
        
        

        public HomeController(IWorkService workservice)
        {
            _workservice = workservice;
          
        }

        public async Task<IActionResult> Index()
        {
            var response = await _workservice.GetAll();
            return View(response.Data);
        }
        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            var response=await _workservice.Create(dto);
            return this.ResponseRedirectToAction(response, "Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            //get by id ile listdto alıyoz geriye workupdatedto döncez.
            //sen workupdatedtoya göre ilgili kaydı getir.
            var response = await _workservice.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response);
            //return View(new WorkUpdateDto { 
            //Id=dto.Id,
            //Definition=dto.Definition,
            //IsCompleted=dto.IsCompleted
            //});
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
            var response = await _workservice.Update(dto);
            return this.ResponseRedirectToAction(response,"Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            var response=await _workservice.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
        }
        public IActionResult NotFound(int code)
        {
            return View();
        }

    }
}
   