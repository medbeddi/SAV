using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Technicien
    {
        public int TechnicienId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Specialite { get; set; }

        // Navigation properties
        public ICollection<InterventionTechnique> InterventionsTechniques { get; set; }
    }
}
