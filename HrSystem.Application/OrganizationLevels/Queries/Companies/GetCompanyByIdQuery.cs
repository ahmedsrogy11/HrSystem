using AutoMapper;
using HrSystem.Application.OrganizationLevels.Abstractions;
using HrSystem.Application.OrganizationLevels.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.OrganizationLevels.Queries.Companies
{
    public record GetCompanyByIdQuery(Guid Id) : IRequest<CompanyDto?>;


    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;

        public GetCompanyByIdHandler(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CompanyDto?> Handle(GetCompanyByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<CompanyDto>(entity);
        }
    }
}

