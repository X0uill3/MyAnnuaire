using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnnuaireModel.Dto;

public class SalarieDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TelephoneFixe { get; set; } = string.Empty;
    public string TelephonePortable { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
}
