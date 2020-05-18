using System;
using System.Collections.Generic;

namespace TEST.Server
{
    public partial class Vote
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int DossierId { get; set; }

        public virtual UserVote CreatedByUser { get; set; }
        public virtual Dossier Dossier { get; set; }
    }
}
