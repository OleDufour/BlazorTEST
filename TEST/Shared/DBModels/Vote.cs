using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Server
{
    public partial class Vote
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(450)]
        public string Content { get; set; }
        [Required]
        [Column("UserID")]
        [StringLength(450)]
        public string UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ClosedDate { get; set; }
        [Column("DossierID")]
        public int DossierId { get; set; }

        [ForeignKey(nameof(DossierId))]
        [InverseProperty("Vote")]
        public virtual Dossier Dossier { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUsers.Vote))]
        public virtual AspNetUsers User { get; set; }
    }
}
