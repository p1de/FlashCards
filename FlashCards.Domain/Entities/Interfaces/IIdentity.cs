using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Domain.Entities.Interfaces
{
    public interface IIdentity
    {
        public string Id { get; set; }
    }
}
