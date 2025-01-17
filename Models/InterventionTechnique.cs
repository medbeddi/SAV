using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InterventionTechnique
    {
        public int InterventionTechniqueId { get; set; }
        public DateTime DateIntervention { get; set; }
        public string Description { get; set; }
        public bool EstGratuite { get; set; }
        public decimal MontantFacture { get; set; }

        public int ReclamationId { get; set; }
        public int TechnicienId { get; set; }

        public Reclamation Reclamation { get; set; }
        public Technicien Technicien { get; set; }
        public ICollection<PieceDeRechange> PiecesDeRechange { get; set; }
    }
}
