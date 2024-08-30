using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
[Table("link")]
public class LinkEntity
(string url = "") : BaseEntity
{
    [Key]
    [Required]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(2048)]
    [Column("url")]
    public string Url {
        get; set;
    } = url;
}
}
