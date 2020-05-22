using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Server
{
    public partial class Discussion
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(250)]
        public string Content { get; set; }
        [Column("VoteID")]
        public int VoteId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserVote.Discussion))]
        public virtual UserVote User { get; set; }
    }
}
