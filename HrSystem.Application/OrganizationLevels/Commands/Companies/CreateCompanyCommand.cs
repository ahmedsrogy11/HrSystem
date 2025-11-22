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

namespace HrSystem.Application.OrganizationLevels.Commands.Companies
{
    public record CreateCompanyCommand(
        string Name,
        string? Code,
        Guid OrganizationId
    ) : IRequest<CompanyDto>;


    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;

        public CreateCompanyHandler(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand r, CancellationToken ct)
        {
            var entity = new Company
            {
                Name = r.Name,
                Code = r.Code,
                OrganizationId = r.OrganizationId
            };

            await _repo.AddAsync(entity, ct);
            return _mapper.Map<CompanyDto>(entity);
        }
    }
}
