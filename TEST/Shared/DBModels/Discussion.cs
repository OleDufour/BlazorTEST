using System;
using System.Collections.Generic;

namespace TEST.Server
{
    public partial class Discussion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int VoteId { get; set; }

        public virtual UserVote User { get; set; }
    }
}
