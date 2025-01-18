<template>
  <div class="container">
    <div class="card">
      <h1 class="card_title">Liste des Interventions</h1>
      <div class="action-bar">
        <button class="card_button" @click="goToCreateIntervention">
          <i class="fas fa-plus-circle"></i> Ajouter une Intervention
        </button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Date</th>
            <th>Sous Garantie</th>
            <th>Montant Facture</th>
            <th>Réclamation</th>
            <th>Technicien</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="intervention in interventions" :key="intervention.id">
            <td>{{ intervention.id }}</td>
            <td>{{ new Date(intervention.dateIntervention).toLocaleDateString() }}</td>
            <td>{{ intervention.sousGarantie ? 'Oui' : 'Non' }}</td>
            <td>{{ intervention.montantFacture ? intervention.montantFacture + ' €' : intervention.montantFacture }}</td>
            <td>
              <span v-if="intervention.reclamation">
                {{ intervention.reclamation.description }}
              </span>
              <span v-else>Non assignée</span>
            </td>
            <td>{{ getTechnicianName(intervention.technicienId) || 'Non assigné' }}</td>
            <td>
              <button @click="goToEditIntervention(intervention.id)" class="action-button">
                <i class="fas fa-edit"></i> Modifier
              </button>
              <button @click="deleteIntervention(intervention.id)" class="action-button-delete">
                <i class="fas fa-trash-alt"></i> Supprimer
              </button>
              <button @click="calculateInvoice(intervention.id)" class="action-button-calculate">
                <i class="fas fa-calculator"></i> Facture Réglée
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="fish-shadow-con">
      <svg class="fish-shadow" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" preserveAspectRatio="xMidYMin slice" viewBox="0 0 743 645">
        <!-- SVG content here -->
      </svg>
    </div>
    <svg class="fish" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" preserveAspectRatio="xMidYMin slice" viewBox="0 0 743 645">
      <!-- SVG content here -->
    </svg>
  </div>
</template>

<script>
import InterventionService from '@/services/InterventionService';
import ReclamationService from '@/services/ReclamationService';
import TechnicienService from '@/services/TechnicienService';  

export default {
  name: 'InterventionList',
  data() {
    return {
      interventions: [],
      techniciens: [], 
    };
  },
  created() {
    this.fetchInterventions();
    this.fetchTechnicians();  
  },
  methods: {
    async fetchInterventions() {
      try {
        const response = await InterventionService.getInterventions();
        const interventions = response.data;

        const reclamationIds = [...new Set(interventions.map(i => i.reclamationId))].filter(Boolean);
        const reclamationResponses = await Promise.all(
          reclamationIds.map(id => ReclamationService.getReclamationById(id))
        );

        const reclamationsMap = reclamationResponses.reduce((map, res) => {
          map[res.data.id] = res.data;
          return map;
        }, {});

        this.interventions = interventions.map(intervention => ({
          ...intervention,
          reclamation: reclamationsMap[intervention.reclamationId] || null,
        }));
      } catch (error) {
        console.error('Erreur lors de la récupération des interventions:', error);
      }
    },

    async fetchTechnicians() {
      try {
        const response = await TechnicienService.getTechniciens();
        this.techniciens = response.data; 
      } catch (error) {
        console.error('Erreur lors de la récupération des techniciens:', error);
      }
    },

    getTechnicianName(technicienId) {
      const technicien = this.techniciens.find(t => t.id === technicienId);
      return technicien ? `${technicien.nom}` : null; 
    },

    goToCreateIntervention() {
      this.$router.push({ name: 'CreateIntervention' });
    },

    goToEditIntervention(id) {
      this.$router.push({ name: 'EditIntervention', params: { id } });
    },

    async deleteIntervention(id) {
      try {
        const confirmed = window.confirm('Êtes-vous sûr de vouloir supprimer cette intervention ?');
        if (!confirmed) return;

        await InterventionService.deleteIntervention(id);
        this.fetchInterventions();
        alert('Intervention supprimée avec succès.');
      } catch (error) {
        console.error("Erreur lors de la suppression de l'intervention:", error);
        alert('Une erreur est survenue lors de la suppression.');
      }
    },

    async calculateInvoice(id) {
      try {
        const response = await InterventionService.calculateInvoice(id);
        alert(`Montant de la facture : ${response.data} €`);
        this.fetchInterventions();
      } catch (error) {
        console.error('Erreur lors du calcul de la facture:', error);
      }
    },
  },
};
</script>

<style scoped>
/* Global container styling */
.container {
  position: relative;
  background-color: #fafafe;
  border-radius: 10px;
  margin: 80px auto; /* Move the form down more */
  padding: 50px 40px; /* Increase padding for larger card */
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.2);
  width: 100%; /* Increase width of the card */
  max-width: 1200px; /* Ensure responsiveness */
  box-sizing: border-box;
  text-align: center; /* Center the content inside */
}

/* Card styling */
.card {
  position: relative;
  z-index: 2;
}

/* Title styling */
.card_title {
  font-size: 36px; /* Larger font size for the title */
  margin-bottom: 30px; /* Add more space below the title */
}

/* Button styling */
.card_button {
  border-radius: 4px;
  border: none;
  outline: none;
  padding: 14px 18px; /* Increase padding for the button */
  font-size: 22px; /* Larger font size for the button */
  line-height: 40px;
  font-weight: 500;
  margin: 30px 0 15px;
  color: #fff;
  background: linear-gradient(#ff6100, rgba(255, 80, 80, 1));
  box-shadow: 0 2px 12px -3px rgba(255, 80, 80, 1);
  transition: all 0.3s;
  opacity: 0.9;
}

.card_button:hover {
  opacity: 1;
  box-shadow: 0 2px 2px -3px rgba(255, 80, 80, 1);
}

/* Table styling */
.table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

th, td {
  padding: 20px;
  text-align: left;
  border-bottom: 1px solid #eee;
}

th {
  background-color: #f9f9f9;
  color: #333;
  font-weight: bold;
}

td {
  color: #666;
  font-size: 16px;
}

/* Action buttons */
.action-button {
  background-color: #e3c204e2;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 10px 16px; /* Augmenter le padding pour agrandir les boutons */
  font-size: 16px; /* Augmenter la taille de la police */
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s;
  margin-right: 10px; /* Ajouter un espace entre les boutons */
}

.action-button-delete {
  background-color: #fb0000;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 10px 16px; /* Augmenter le padding pour agrandir les boutons */
  font-size: 16px; /* Augmenter la taille de la police */
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s;
}

.action-button-calculate {
  background-color: #00A9A9;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 10px 16px; /* Augmenter le padding pour agrandir les boutons */
  font-size: 16px; /* Augmenter la taille de la police */
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s;
}

.action-button:hover,
.action-button-delete:hover,
.action-button-calculate:hover {
  opacity: 0.9;
  transform: translateY(-2px);
}

/* Fish SVG Styling */
.fish, .fish-shadow {
  width: 740px; /* Increase size */
  height: auto;
  position: absolute;
  top: -145px;
  left: -177px;
  animation: fish 3s forwards ease-in-out;
}

.fish-shadow-con {
  opacity: 0.2;
  filter: blur(10px);
  position: absolute;
  top: 0;
  left: 0;
}

.fish-shadow {
  top: -125px;
  left: -157px;
  opacity: 0.4;
  filter: url("#goo");
}

@keyframes fish {
  0%, 70% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
</style>