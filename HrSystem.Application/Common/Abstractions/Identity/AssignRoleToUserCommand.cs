using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Common.Abstractions.Identity
{
    public record AssignRoleToUserCommand(Guid UserId, string RoleName) : IRequest;





    public class AssignRoleToUserCommandHandler
        : IRequestHandler<AssignRoleToUserCommand>
    {
        private readonly IIdentityService _identity;

        public AssignRoleToUserCommandHandler(IIdentityService identity)
        {
            _identity = identity;
        }

        public async Task<Unit> Handle(AssignRoleToUserCommand r, CancellationToken ct)
        {
            var (success, error) = 
                await _identity.AssignRoleToUserAsync(r.UserId, r.RoleName, ct);


            if (!success)
                throw new InvalidOperationException(
                    $"Failed to assign role '{r.RoleName}' to user with ID '{r.UserId}': {error}");

            return Unit.Value;
        }
    }
}
