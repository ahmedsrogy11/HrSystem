using AutoMapper;
using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Application.Overtime.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Queries
{
    public record GetOvertimeRequestByIdQuery(Guid Id) : IRequest<OvertimeRequestDto?>;





    public class GetOvertimeRequestByIdHandler
       : IRequestHandler<GetOvertimeRequestByIdQuery, OvertimeRequestDto?>
    {
        private readonly IOvertimeRequestRepository _repo;
        private readonly IMapper _mapper;

        public GetOvertimeRequestByIdHandler(IOvertimeRequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OvertimeRequestDto?> Handle(GetOvertimeRequestByIdQuery r, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(r.Id, ct);
            return entity is null ? null : _mapper.Map<OvertimeRequestDto>(entity);
        }
    }


}
