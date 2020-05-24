 using System.Linq;
 
using Microsoft.EntityFrameworkCore;

namespace TEST.Server
{

    public interface IQuery
    {
        IQueryable<VoteCasted> GetVotesCastedByPropositionId();
    }

    public class Query : IQuery
    {
        private readonly MonConciergeContext _context;

        public Query(MonConciergeContext context)
        {
            _context = context;

        }

        public IQueryable<VoteCasted> GetVotesCastedByPropositionId()
        {
            return _context.VoteCasted.Include(c => c.Proposition).ThenInclude(c => c.VoteCasted).ThenInclude(c => c.User).ThenInclude(c => c.Dossier).AsQueryable();
        }
    }
}
