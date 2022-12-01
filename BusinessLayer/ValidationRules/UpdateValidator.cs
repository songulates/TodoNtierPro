using DtosLayer.WorkDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class UpdateValidator:AbstractValidator<WorkUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition is required");
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
