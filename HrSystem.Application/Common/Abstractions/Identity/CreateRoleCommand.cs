using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Common.Abstractions.Identity
{
    public record CreateRoleCommand(string Name) : IRequest;
    


    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IIdentityService _identityService;
        public CreateRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(CreateRoleCommand r, CancellationToken ct)
        {
            var (success, error) = await _identityService.CreateRoleAsync(r.Name , ct);

            if (success)
            {
              return Unit.Value;

            }
            throw new Exception($"Create role failed: {error}");

        }
    }


}
