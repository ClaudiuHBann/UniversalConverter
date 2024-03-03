using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
[Table("rank")]
public class RankEntity : BaseEntity
{
    [Key]
    [Required]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(18)]
    [Column("converter")]
    public string Converter {
        get; set;
    } = "";

    [Required]
    [Column("conversions")]
    public long Conversions {
        get; set;
    } = 0;

    public RankEntity(string converter)
    {
        Converter = converter;
    }
}
}
