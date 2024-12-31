using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnnuaireModel.Entities;

public class Salarie
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Nom { get; set; } = string.Empty;
    [StringLength(100)]
    public string Prenom { get; set; } = string.Empty;
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    [StringLength(15)]
    public string TelephoneFixe { get; set; } = string.Empty;
    [StringLength(15)]
    public string TelephonePortable { get; set; } = string.Empty;

    //relation  avec l'entité Service
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    public Service? Service { get; set; }    
    
    //relation  avec l'entité Siege
    [ForeignKey(nameof(Siege))]
    public int SiegeId { get; set; }
    public Siege? Siege { get; set; }
}
