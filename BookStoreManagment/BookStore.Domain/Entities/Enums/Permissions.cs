using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Enums
{
    public enum Permissions
    {
        Create=1,
        Get,
        Update,
        Delete,
        Observe,
        BookBuy,
        BookOrder,
        BookRate
    }
}
