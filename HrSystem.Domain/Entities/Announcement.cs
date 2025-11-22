using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public DateTime PublishAtUtc { get; set; } = DateTime.UtcNow;
        public bool IsGlobal { get; set; } = true; // أو ربط بمستوى تنظيمي لاحقًا
    }
}
