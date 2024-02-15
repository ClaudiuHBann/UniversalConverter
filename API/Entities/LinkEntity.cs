using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
[Table("link")]
public class LinkEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; } = -1;

    [Column("link_long", TypeName = "text")]
    public string LinkLong {
        get; set;
    } = "";

    public LinkEntity()
    {
    }

    public LinkEntity(string linkLong)
    {
        LinkLong = linkLong;
    }
}
}
