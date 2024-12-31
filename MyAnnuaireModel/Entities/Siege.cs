using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnnuaireModel.Entities;

public class Siege
{
    public int Id { get; set; }

    //relation  avec l'entité Ville
    [ForeignKey(nameof(Ville))]
    public int VilleId { get; set; }
    public Ville Ville { get; set; } = new Ville();

    //relation  avec l'entité  TypeSiege
    [ForeignKey(nameof(TypeSiege))]
    public int TypeSiegeId { get; set; }
    public TypeSiege? TypeSiege { get; set; }
}
