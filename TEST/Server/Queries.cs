using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TEST.Server
{

    public interface IQuery {
        void test();
    }

    public class Query : IQuery
    {
        private readonly MonConciergeContext _context;

        public Query(MonConciergeContext context)
        {
            _context = context;

        }

        public void test()
        {
          
        }
    }
}
