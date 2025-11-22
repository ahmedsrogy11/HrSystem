using FluentValidation;
using HrSystem.Application.SupportTickets.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.SupportTickets
{
    public class CreateSupportTicketValidator : AbstractValidator<CreateSupportTicketCommand>
    {
        public CreateSupportTicketValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).NotEmpty();
        }
    }



    public class UpdateSupportTicketValidator : AbstractValidator<UpdateSupportTicketCommand>
    {
        public UpdateSupportTicketValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
