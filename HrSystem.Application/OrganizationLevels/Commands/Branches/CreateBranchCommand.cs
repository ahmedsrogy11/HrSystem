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

namespace HrSystem.Application.OrganizationLevels.Commands.Branches
{
    public record CreateBranchCommand(
       string Name,
       string? Code,
       string? Address,
       Guid CompanyId
    ) : IRequest<BranchDto>;


    public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, BranchDto>
    {
        private readonly IBranchRepository _repo;
        private readonly IMapper _mapper;

        public CreateBranchHandler(IBranchRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BranchDto> Handle(CreateBranchCommand r, CancellationToken ct)
        {
            var entity = new Branch
            {
                Name = r.Name,
                Code = r.Code,
                Address = r.Address,
                CompanyId = r.CompanyId
            };

            await _repo.AddAsync(entity, ct);

            return _mapper.Map<BranchDto>(entity);
        }
    }
}
