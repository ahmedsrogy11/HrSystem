using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using HrSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Commands.Departments
{
    public record CreateDepartmentCommand(
        string Name,
        string? Code,
        Guid BranchId
    ) : IRequest<DepartmentDto>;


    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public CreateDepartmentHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(CreateDepartmentCommand r, CancellationToken ct)
        {
            var entity = new Department
            {
                Name = r.Name,
                Code = r.Code,
                BranchId = r.BranchId
            };

            await _repo.AddAsync(entity, ct);
            return _mapper.Map<DepartmentDto>(entity);
        }
    }
}
