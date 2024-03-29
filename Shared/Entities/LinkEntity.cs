﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
[Table("link")]
public class LinkEntity : BaseEntity
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
    } = "";

    public LinkEntity()
    {
    }

    public LinkEntity(string url)
    {
        Url = url;
    }
}
}
