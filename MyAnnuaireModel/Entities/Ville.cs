using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnnuaireModel.Entities;

public class Ville
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    //relation  avec l'entité Pays
    [ForeignKey(nameof(Pays))]
    public int PaysId { get; set; }
    public Pays? Pays { get; set; }
}
