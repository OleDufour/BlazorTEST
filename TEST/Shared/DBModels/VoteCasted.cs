using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Server
{
    public partial class VoteCasted
    {
        public VoteCasted()
        {
            Discussion = new HashSet<Discussion>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PropositionID")]
        public int PropositionId { get; set; }
        public bool VotedFor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VotedDate { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }
        [Column("UserID")]
        [StringLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(PropositionId))]
        [InverseProperty("VoteCasted")]
        public virtual Proposition Proposition { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUsers.VoteCasted))]
        public virtual AspNetUsers User { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Discussion> Discussion { get; set; }
    }
}
