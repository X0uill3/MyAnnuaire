using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnnuaireModel.Entities;

public class Utilisateur
{
    public int Id { get; set; }
    public string login { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public bool EstAdmin { get; set; }
}
