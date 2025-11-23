using FluentValidation;
using HrSystem.Application.Announcements.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements
{
    public class CreateAnnouncementValidator : AbstractValidator<CreateAnnouncementCommand>
    {
        public CreateAnnouncementValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Body).NotEmpty();
        }
    }




    public class UpdateAnnouncementValidator : AbstractValidator<UpdateAnnouncementCommand>
    {
        public UpdateAnnouncementValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Body).NotEmpty();
        }
    }



}
