using Pgvector;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VectorSemanticSearchPoc.Data.Models
{
    [Table("courses")]
    public class Course
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("sku")]
        public string Sku { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column(TypeName = "vector(256)")]
        public Vector Embedding { get; set; }
    }
}
