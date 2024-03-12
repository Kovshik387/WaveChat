using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Context.Entities.Commons;

[Index("Uid",IsUnique =true)]
public abstract class EntityBase
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual Guid Uid { get; set; } = Guid.NewGuid();
}
