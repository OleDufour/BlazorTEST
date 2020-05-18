using System;
using System.Collections.Generic;

namespace TEST.Server
{
    public partial class Dossier
    {
        public Dossier()
        {
            Vote = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }

        public virtual UserVote User { get; set; }
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
