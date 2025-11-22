using HrSystem.Application.Loans.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Loans.Commands
{
    public record DeleteLoanRequestCommand(Guid Id) : IRequest<bool>;




    public class DeleteLoanRequestHandler : IRequestHandler<DeleteLoanRequestCommand, bool>
    {
        private readonly ILoanRequestRepository _repo;

        public DeleteLoanRequestHandler(ILoanRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteLoanRequestCommand r, CancellationToken ct)
        {
            await _repo.DeleteAsync(r.Id, ct);
            return true;
        }
    }
}
