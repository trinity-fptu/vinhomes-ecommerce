using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        InCart = 1,
        HasCheckedOut,
        Cancelled,
        Approved,
        Declined,
    }
}
