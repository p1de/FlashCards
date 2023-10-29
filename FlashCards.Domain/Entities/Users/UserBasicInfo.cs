using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Domain.Entities.Users
{
    public class UserBasicInfo
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
