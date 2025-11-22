using FluentValidation;
using HrSystem.Application.OrganizationLevels.Commands.Branches;
using HrSystem.Application.OrganizationLevels.Commands.Companies;
using HrSystem.Application.OrganizationLevels.Commands.Departments;
using HrSystem.Application.OrganizationLevels.Commands.Organizations;
using HrSystem.Application.OrganizationLevels.Commands.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels
{
    public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الجهة مطلوب.")
                .MaximumLength(200).WithMessage("اسم الجهة يجب ألا يتجاوز 200 حرف.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("الكود يجب ألا يتجاوز 50 حرف.");
        }
    }

    public class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        public UpdateOrganizationValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("المعرف مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الجهة مطلوب.")
                .MaximumLength(200).WithMessage("اسم الجهة يجب ألا يتجاوز 200 حرف.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("الكود يجب ألا يتجاوز 50 حرف.");
        }
    }



    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الشركة مطلوب.")
                .MaximumLength(200).WithMessage("اسم الشركة يجب ألا يتجاوز 200 حرف.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("الكود يجب ألا يتجاوز 50 حرف.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage("OrganizationId مطلوب.");
        }
    }

    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("المعرف مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الشركة مطلوب.")
                .MaximumLength(200).WithMessage("اسم الشركة يجب ألا يتجاوز 200 حرف.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("الكود يجب ألا يتجاوز 50 حرف.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage("OrganizationId مطلوب.");
        }
    }




    public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.")
                .MaximumLength(200).WithMessage("اسم الفرع يجب ألا يتجاوز 200 حرف.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("كود الفرع يجب ألا يتجاوز 50 حرف.");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("العنوان يجب ألا يتجاوز 500 حرف.");

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("CompanyId مطلوب.");
        }
    }

    public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("المعرف مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.")
                .MaximumLength(200).WithMessage("اسم الفرع يجب ألا يتجاوز 200 حرف.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("كود الفرع يجب ألا يتجاوز 50 حرف.");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("العنوان يجب ألا يتجاوز 500 حرف.");

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("CompanyId مطلوب.");
        }
    }



    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الإدارة مطلوب.")
                .MaximumLength(200);

            RuleFor(x => x.Code)
                .MaximumLength(50);

           

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("BranchId مطلوب.");
        }
    }

    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الإدارة مطلوب.")
                .MaximumLength(200);

            RuleFor(x => x.Code)
                .MaximumLength(50);

            

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("BranchId مطلوب.");
        }
    }




    public class CreateTeamValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفريق مطلوب.")
                .MaximumLength(200);

            RuleFor(x => x.Code)
                .MaximumLength(50);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("DepartmentId مطلوب.");
        }
    }


    public class UpdateTeamValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفريق مطلوب.")
                .MaximumLength(200);

            RuleFor(x => x.Code)
                .MaximumLength(50);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("DepartmentId مطلوب.");
        }
    }


}
