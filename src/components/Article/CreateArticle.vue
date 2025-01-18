<template>
  <div class="container">
    <div class="card">
      <h1 class="card_title">{{ isEditMode ? 'Modifier' : 'Ajouter' }} un Article</h1>
      <form class="card_form" @submit.prevent="saveArticle">
        <div class="input">
          <input type="text" class="input_field" v-model="article.nom" required />
          <label class="input_label">Nom</label>
        </div>

        <div class="input">
          <textarea class="input_field" v-model="article.description" required></textarea>
          <label class="input_label">Description</label>
        </div>

        <div class="input">
          <input type="number" class="input_field" v-model="article.prix" required />
          <label class="input_label">Prix</label>
        </div>

        <div class="input">
          <label class="checkbox-label">
            <input type="checkbox" v-model="article.sousGarantie" class="checkbox" />
            Sous Garantie
          </label>
        </div>

        <button type="submit" class="card_button">{{ isEditMode ? 'Modifier' : 'Ajouter' }}</button>
      </form>
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
  name: 'CreateArticle',
  data() {
    return {
      article: {
        nom: '',
        description: '',
        prix: 0,
        sousGarantie: false,
      },
      isEditMode: false,
    };
  },
  async created() {
    const articleId = this.$route.params.id;
    if (articleId) {
      this.isEditMode = true;
      const response = await ArticleService.getArticleById(articleId);
      this.article = response.data;
    }
  },
  methods: {
    async saveArticle() {
      try {
        if (this.isEditMode) {
          await ArticleService.updateArticle(this.article.id, this.article);
        } else {
          await ArticleService.createArticle(this.article);
        }
        this.$router.push({ name: 'ArticleList' });
      } catch (error) {
        console.error('Erreur lors de l\'enregistrement de l\'article:', error);
      }
    },
  },
};
</script>

<style scoped>
/* Global form styling */
.container {
  position: relative;
  background-color: #fafafe;
  border-radius: 10px;
  margin: 80px auto; /* Move the form down more */
  padding: 50px 40px; /* Increase padding for larger card */
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.2);
  width: 600px; /* Increase width of the card */
  max-width: 100%; /* Ensure responsiveness */
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

/* Form styling */
.card_form {
  display: flex;
  flex-direction: column;
  gap: 25px; /* Increase spacing between form elements */
}

/* Input styling */
.input {
  display: flex;
  flex-direction: column-reverse;
  position: relative;
  margin-top: 15px; /* Add more space between inputs */
}

.input_label {
  color: #8597a3;
  position: absolute;
  top: 20px;
  transition: all 0.3s;
  font-size: 18px; /* Larger font size for labels */
}

.input_field {
  border: 0;
  padding: 12px 0; /* Increase padding for larger input fields */
  z-index: 1;
  background-color: transparent;
  border-bottom: 2px solid #eee;
  font-size: 18px; /* Larger font size for input fields */
  line-height: 36px;
}

.input_field:focus,
.input_field:valid {
  outline: 0;
  border-bottom-color: #665856;
}

.input_field:focus + .input_label,
.input_field:valid + .input_label {
  color: #665856;
  transform: translateY(-25px);
}

textarea.input_field {
  min-height: 120px;
  resize: vertical;
}

/* Checkbox styling */
.checkbox-label {
  display: flex;
  align-items: center;
  font-size: 18px;
  color: #8597a3;
}

.checkbox {
  margin-right: 10px;
  transform: scale(1.2);
}

/* Button styling */
.card_button {
  border-radius: 4px;
  border: none;
  outline: none;
  width: 100%;
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