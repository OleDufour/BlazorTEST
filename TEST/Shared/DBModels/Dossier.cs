using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Server
{
    public partial class Dossier
    {
        public Dossier()
        {
            Vote = new HashSet<Vote>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }
        [Required]
        [Column("UserID")]
        [StringLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUsers.Dossier))]
        public virtual AspNetUsers User { get; set; }
        [InverseProperty("Dossier")]
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
