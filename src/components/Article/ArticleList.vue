<template>
  <div class="container">
    <div class="card">
      <h1 class="card_title">Liste des Articles</h1>
      <div class="action-bar">
        <button class="card_button" @click="goToCreateArticle">
          <i class="fas fa-plus-circle"></i> Ajouter un Article
        </button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nom</th>
            <th>Description</th>
            <th>Prix</th>
            <th>Sous Garantie</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="article in articles" :key="article.id">
            <td>{{ article.id }}</td>
            <td>{{ article.nom }}</td>
            <td>{{ article.description }}</td>
            <td>{{ article.prix }}</td>
            <td>{{ article.sousGarantie ? 'Oui' : 'Non' }}</td>
            <td>
              <button @click="goToEditArticle(article.id)" class="action-button">
                <i class="fas fa-edit"></i> Modifier
              </button>
              <button @click="deleteArticle(article.id)" class="action-button-delete">
                <i class="fas fa-trash-alt"></i> Supprimer
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
import ArticleService from '@/services/ArticleService';

export default {
  name: 'ArticleList',
  data() {
    return {
      articles: [],
    };
  },
  created() {
    this.fetchArticles();
  },
  methods: {
    async fetchArticles() {
      try {
        const response = await ArticleService.getArticles();
        this.articles = response.data;
      } catch (error) {
        console.error('Erreur lors de la récupération des articles:', error);
      }
    },
    goToCreateArticle() {
      this.$router.push({ name: 'CreateArticle' });
    },
    goToEditArticle(articleId) {
      this.$router.push({ name: 'EditArticle', params: { id: articleId } });
    },
    async deleteArticle(articleId) {
      try {
        const confirmed = window.confirm('Êtes-vous sûr de vouloir supprimer cet article ?');
        if (!confirmed) {
          return;
        }
        await ArticleService.deleteArticle(articleId);
        this.fetchArticles();
        alert('Article supprimé avec succès.');
      } catch (error) {
        console.error("Erreur lors de la suppression de l'article:", error);
        alert('Une erreur est survenue lors de la suppression.');
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

.action-button:hover,
.action-button-delete:hover {
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