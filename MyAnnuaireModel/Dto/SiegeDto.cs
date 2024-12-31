using MyAnnuaireModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnnuaireModel.Dto;

public class SiegeDto
{
    public int Id { get; set; }
    public int VilleId { get; set; }
    public string Ville { get; set; } = string.Empty;
    public int TypeSiegeId { get; set; }
    public string TypeSiege { get; set; } = string.Empty;
}
