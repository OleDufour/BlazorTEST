using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TEST.Server
{
    public partial class Proposition
    {
        [NotMapped]
        public bool CurrentUserHasVoted { get; set; }
        [NotMapped]
        public int VotesFor { get { return VoteCasted.Where(x => x.VotedFor).Count(); } }
        [NotMapped]
        public int VotesAgainst { get { return VoteCasted.Where(x => !x.VotedFor).Count(); } }

    }
}
