﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectMatchBack.Models;

public partial class Breed
{
    public int IdBreed { get; set; }

    public string BreedName { get; set; } = null!;

    public int IdAnimalType { get; set; }
    public virtual AnimalType IdAnimalTypeNavigation { get; set; } = null!;
    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
