using System;
using System.Collections.Generic;

namespace TEST.Server
{
    public partial class UserVote
    {
        public UserVote()
        {
            Discussion = new HashSet<Discussion>();
            Dossier = new HashSet<Dossier>();
            Vote = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public int VoteId { get; set; }
        public bool VotedFor { get; set; }
        public DateTime VotedDate { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Discussion> Discussion { get; set; }
        public virtual ICollection<Dossier> Dossier { get; set; }
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
