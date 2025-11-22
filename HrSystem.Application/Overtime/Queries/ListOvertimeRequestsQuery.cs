using AutoMapper;
using HrSystem.Application.Overtime.Abstractions;
using HrSystem.Application.Overtime.Dto;
using HrSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Overtime.Queries
{
    public record ListOvertimeRequestsQuery(
    Guid? EmployeeId,
    OvertimeStatus? Status,
    DateTime? DateFrom,
    DateTime? DateTo,
    int Page = 1,
    int PageSize = 20
    ) : IRequest<(IReadOnlyList<OvertimeRequestDto>, int)>;






    public class ListOvertimeRequestsHandler
      : IRequestHandler<ListOvertimeRequestsQuery, (IReadOnlyList<OvertimeRequestDto>, int)>
    {
        private readonly IOvertimeRequestRepository _repo;
        private readonly IMapper _mapper;

        public ListOvertimeRequestsHandler(IOvertimeRequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<OvertimeRequestDto>, int)> Handle(
            ListOvertimeRequestsQuery r,
            CancellationToken ct)
        {
            var (entities, total) = await _repo.ListAsync(
                r.EmployeeId,
                r.Status,
                r.DateFrom,
                r.DateTo,
                r.Page,
                r.PageSize,
                ct);

            var dtos = entities
                .Select(_mapper.Map<OvertimeRequestDto>)
                .ToList();

            return (dtos, total);
        }
    }





}
