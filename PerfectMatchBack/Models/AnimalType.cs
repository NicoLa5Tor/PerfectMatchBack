using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectMatchBack.Models;

public partial class AnimalType
{
    [Key]
    public int IdAnimalType { get; set; }

    public string AnimalTypeName { get; set; } = null!;
    public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();
    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
