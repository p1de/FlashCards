using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Infrastructure.Data
{
    public class MongoDbContext : IDbContext
    {
        private string _connectionString;
        public MongoDbContext(string connectionString) 
        {
            _connectionString = connectionString;
        }
    }
}
