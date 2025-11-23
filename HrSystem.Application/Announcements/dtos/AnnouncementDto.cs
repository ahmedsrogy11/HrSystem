using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Application.Announcements.dtos
{
    public record AnnouncementDto(
        Guid Id,
        string Title,
        string Body,
        DateTime PublishAtUtc,
        bool IsGlobal
    );
}
