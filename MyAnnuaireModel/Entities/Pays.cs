using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAnnuaireModel.Entities;

public class Pays
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    // Relation avec l'entité Ville
    public List<Ville>? Villes { get; set; }
}
