using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Domain.ValueObjects
{
    public record Money(decimal Amount, string Currency = "EGP")
    {
        public static Money Zero (string currency = "EGP") => new Money(0m, currency);
    };
}
