using AutoMapper;
using BusinessLayer.Extensions;
using BusinessLayer.ValidationRules;
using CommonLayer.ResponseObject;
using DataAccesLayer.UnitOfWork;
using DtosLayer.Interfaces;
using DtosLayer.WorkDtos;
using EntitiesLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createvalidator;
        private readonly IValidator<WorkUpdateDto> _updatevalidator;
        //constructora ilgili interface DI aracılığıyla geciyo
        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createvalidator, IValidator<WorkUpdateDto> updatevalidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createvalidator = createvalidator;
            _updatevalidator = updatevalidator;
            //ctroda uow almak
        }
        //work create edilirken  WorkCreateDto yu  work e ceviryoruz.
        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var validatonresult = _createvalidator.Validate(dto);
            if (validatonresult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {
               

                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, validatonresult.ConvertToCustomValidationError());
            }
            //WorkCreateDto worke cevrilecek.

            //{
            //    IsCompleted = dto.IsCompleted,
            //    Definition = dto.Definition
            //});
            //saveshanges async

        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {
            //busines işi önce veriyi çekiyoruz, dataaccese gidiyoruzuow aracılığıyla, 
            //çektiğimiz veride kontrol yapıyoruz, controlden sonra maplama işlemi yapıyoruz.
            //geriye dönüyoruz worklistle
            //elimizde list of work var bunu worklistdtoya cevir
            //var list = await _uow.GetRepository<Work>().GetAll();
            //var worklist = new List<WorkListDto>();
            //if (list != null && list.Count > 0)
            //{
            //    foreach (var work in list)
            //    {
            //        worklist.Add(new()
            //        {
            //            Definition = work.Definition,
            //            Id = work.Id,
            //            IsCompleted = work.IsCompleted

            //        });

            //    }
            //}
            var data= _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            //ilgili şey izlemeden bize geri dönecek.
            //paratez içinden gelen sonuc dto ya cevrilecek.
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
            //return new()
            //{
            //    Definition = work.Definition,

            //    IsCompleted = work.IsCompleted
            //};
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            var result = _updatevalidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{dto.Id} ye ait data bulunamadı");
            }
            else
            {
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
            }
        }
           
            //_uow.GetRepository<Work>().Update(new()
            //{
            //    IsCompleted = dto.IsCompleted,
            //    Definition = dto.Definition,
            //    Id = dto.Id
            //});
            //saveshanges async
           
        
        //worklisti worklistdtoya cevirmek için automapper kullncaz
    }
}
