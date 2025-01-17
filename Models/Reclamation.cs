using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Reclamation
    {
        public int ReclamationId { get; set; }
        public DateTime DateReclamation { get; set; }
        public string Description { get; set; }
        public string Statut { get; set; }

        // Foreign key
        public int ClientId { get; set; }

        // Navigation properties
        public Client Client { get; set; }
        public InterventionTechnique InterventionTechnique { get; set; }
    }
}
