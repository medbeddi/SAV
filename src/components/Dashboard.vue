<template>
  <div class="container">
    <div class="card">
      <h1 class="card_title">Dashboard</h1>

      <div class="stats-container">
        <!-- Affichage des statistiques sous forme de texte -->
        <div class="stat-box">
          <h3>Total Réclamations</h3>
          <p class="stat-value">{{ totalReclamations }}</p>
        </div>

        <div class="stat-box" v-if="totalFacturesHorsGarantie !== null">
          <h3>Factures Hors Garantie</h3>
          <p class="stat-value">{{ totalFacturesHorsGarantie }} €</p>
        </div>
      </div>

      <!-- Section Graphique des Réclamations par statut -->
      <div class="charts-container" v-if="reclamationPercentageByStatus">
        <h3>Réclamations par Statut</h3>
        <br>
        <div class="chart-list">
          <PieChart :data="reclamationPercentageData" />
        </div>
      </div>

      <!-- Section Articles les plus réclamés -->
      <div class="list-container" v-if="mostReclaimedArticles.length > 0">
        <h3>Liste des Réclamations</h3>
        <br>
        <div class="reclamation-cards">
          <div v-for="(article, index) in mostReclaimedArticles" :key="index" class="reclamation-card">
            <h4>{{ article }}</h4>
          </div>
        </div>
      </div>
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
import { Pie } from 'vue-chartjs';
import { Pie as PieChart } from 'vue-chartjs';
import { Chart as ChartJS, Title, Tooltip, Legend, ArcElement } from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, ArcElement);

export default {
  name: "DashboardAdmin",
  components: {
    PieChart,
  },
  data() {
    return {
      totalReclamations: '',
      customerSatisfactionRate: null,
      totalFacturesHorsGarantie: null,
      reclamationPercentageByStatus: null,
      mostReclaimedArticles: [],
    };
  },
  created() {
    this.fetchStatistics();
  },
  methods: {
    async fetchStatistics() {
      try {
        const reclamationsResponse = await StatisticsService.getTotalReclamations();
        this.totalReclamations = reclamationsResponse.data.totalReclamations; // Extraire la valeur

        const satisfactionResponse = await StatisticsService.getCustomerSatisfactionRate();
        this.customerSatisfactionRate = satisfactionResponse.data.satisfactionRate; // Extraire la valeur

        const facturesResponse = await StatisticsService.getTotalFacturesHorsGarantie();
        this.totalFacturesHorsGarantie = facturesResponse.data.totalFacturesHorsGarantie; // Extraire la valeur

        const statusResponse = await StatisticsService.getReclamationPercentageByStatus();
        this.reclamationPercentageByStatus = statusResponse.data;

        const articlesResponse = await StatisticsService.getMostReclaimedArticles();
        this.mostReclaimedArticles = articlesResponse.data;
      } catch (error) {
        console.error("Erreur lors de la récupération des statistiques :", error);
      }
    },
  },
  computed: {
    // Préparation des données pour le graphique circulaire
    reclamationPercentageData() {
      if (this.reclamationPercentageByStatus) {
        const labels = Object.keys(this.reclamationPercentageByStatus);
        const data = Object.values(this.reclamationPercentageByStatus);
        return {
          labels: labels,
          datasets: [
            {
              data: data,
              backgroundColor: ['#FF9999', '#66B2FF', '#99FF99', '#FFCC99'],
              hoverOffset: 4,
            },
          ],
        };
      }
      return {};
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
  width: 90%; /* Increase width of the card */
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

/* Stats container for the cards */
.stats-container {
  margin-top: 90px;
  display: flex;
  gap: 30px;
  margin-bottom: 30px;
  justify-content: space-between;
}

.stat-box {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  width: 30%;
  text-align: center;
  transition: all 0.3s ease;
  border: 1px solid black;
}

.stat-box:hover {
  box-shadow: 0 6px 15px rgba(0, 0, 0, 0.15);
  transform: translateY(-5px);
}

.stat-box h3 {
  color: #4e5b6e;
  font-size: 1.3rem;
  margin-bottom: 10px;
}

.stat-value {
  font-size: 1.8rem; /* Increased font size */
  font-weight: bold;
  color: #1ea809;
  text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.1); /* Added text shadow */
}

/* Chart container */
.charts-container {
  margin-top: 40px;
  padding: 20px; /* Added padding */
  background-color: #f9f9f9; /* Light background color */
  border-radius: 8px; /* Rounded corners */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Subtle shadow */
}

.charts-container h3 {
  font-size: 1.6rem; /* Increased font size */
  color: #333; /* Darker text color */
  margin-bottom: 20px; /* Added margin */
}

.chart-list {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 20px;
}

/* List container for most reclaimed articles */
.list-container {
  margin-top: 20px;
  padding: 20px; /* Added padding */
  background-color: #fff; /* White background */
  border-radius: 8px; /* Rounded corners */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Subtle shadow */
}

/* Additional styles for better visual appeal */
.card {
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
}

.card h4 {
  font-size: 1.4rem;
  color: #333;
  margin-bottom: 10px;
}

.card p {
  font-size: 1rem;
  color: #666;
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