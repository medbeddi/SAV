<template>
    <nav class="navbar">
      <ul class="navbar-list">
        <li class="navbar-item" v-for="parent in navbarStructure" :key="parent.name">
          <router-link
            v-if="parent.path"
            class="navbar-link"
            :class="{'dashboard-link': parent.label === 'Tableau de bord' }"
            :to="parent.path"
          >{{ parent.label }}</router-link>

          <ul v-if="parent.children && parent.children.length" class="dropdown">
            <li class="dropdown-item" v-for="child in parent.children" :key="child.name">
              <router-link
                v-if="child.path"
                class="dropdown-link"
                :to="child.path"
              >{{ child.label }}</router-link>
            </li>
          </ul>
        </li>
      </ul>
    </nav>
  </template>
  
  <script>
  export default {
    name: "Navbar",
    data() {
      return {
        navbarStructure: [
        {
            label: "Tableau de bord",
            path: "/dashboard",
            name: "Dashboard",
            children: [],
          },
          {
            label: "Clients",
            path: "/clients",
            name: "Client",
            children: [
            { label: "Liste des clients", path: "/clients", name: "Client" },
              { label: "Créer un client", path: "/create-client", name: "CreateClient" },
            ],
          },
          {
          label: "Techniciens", 
          path: "/techniciens", 
          name: "Technicien",
          children: [
            { label: "Liste des techniciens", path: "/techniciens", name: "TechnicienList" },
            { label: "Créer un technicien", path: "/technicien/create", name: "CreateTechnicien" },
          ],
        },

          {
            label: "Articles",
            path: "/articles",
            name: "ArticleList" ,
            children: [
            { label: "Lists des articles", path: "/articles", name: "ArticleList" },
              { label: "Créer un article", path: "/create-article", name: "CreateArticle" },
            ],
          },
          {
            label: "Interventions",
            path: "/interventions",
            name: "InterventionList",
            children: [
            { label: "Liste des interventions", path: "/interventions", name: "InterventionList" },
              { label: "Créer une intervention", path: "/create-intervention", name: "CreateIntervention" },
            ],
          },
          {
            label: "Réclamations",
            path: "/reclamations",
            name: "ReclamationList",
            children: [
            { label: "Liste des réclamations", path: "/reclamations", name: "ReclamationList" },
              { label: "Créer une réclamation", path: "/create-reclamation", name: "CreateReclamation" },
            ],
          },
          {
            label: "Pièces",
            path: "/pieces",
            name: "Piece",
            children: [
            { label: "Liste des pièces", path: "/pieces", name: "Piece" },
              { label: "Créer une pièce", path: "/create-piece", name: "CreatePiece" },
            ],
          },
          {
            label: "Authentification",
            path: "/login", 
            name: "Login",
            children: [
            { label: "Ajouter un rôle", path: "/add-role", name: "Role" },
            { label: "Se déconnecter", path: "/login", name: "Login" },
            ],
          },
        ],
      };
    },
  };
  </script>
  
  <style scoped>
/* Navbar Container */
.navbar {
  background-color: #4A148C; /* Using the same color from your previous navbar style */
  color: white;
  padding: 20px 0;
  font-family: 'Open Sans Condensed', sans-serif;
}

/* Navbar List */
.navbar-list {
  list-style: none;
  margin: 0;
  padding: 0;
  text-align: center;
}

/* Navbar Item */
.navbar-item {
  display: inline-block;
  margin: 0 15px;
  position: relative;
  transition: transform 0.3s ease;
}

/* Hover effect on navbar item */
.navbar-item:hover {
  transform: scale(1.05);
}

/* Navbar Link */
.navbar-link {
  font-size: 22px;
  text-transform: uppercase;
  text-decoration: none;
  color: #fff;
  display: block;
  position: relative;
  padding: 4px 0;
  transition: color 0.3s ease, transform 0.3s ease;
}

/* Underline effect for the navbar link */
.navbar-link::before {
  content: "";
  width: 100%;
  height: 4px;
  position: absolute;
  left: 0;
  bottom: 0;
  background: #fff;
  transition: 0.5s transform ease;
  transform: scale3d(0,1,1);
  transform-origin: 0 50%;
}

.navbar-link:hover::before {
  transform: scale3d(1,1,1);
}

/* Specific style for dropdown links */
.dropdown-link {
  font-size: 0.9rem;
  padding: 8px 16px;
  display: block;
  transition: background-color 0.3s ease, padding-left 0.3s ease;
}

/* Hover effect for dropdown links */
.dropdown-link:hover {
  background-color: #E91E63; /* Pink color from previous navbar style */
  padding-left: 20px; /* Add indentation for depth */
}

/* Dropdown Menu */
.dropdown {
  position: absolute;
  top: 100%;
  left: 0;
  background-color:rgb(255, 255, 255);
  list-style: none;
  margin: 0;
  padding: 0;
  display: none;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  border-radius: 4px;
  min-width: 100%;
  opacity: 0;
  visibility: hidden;
  transform: translateY(10px);
  transition: opacity 0.3s ease, visibility 0s 0.3s, transform 0.3s ease;
}

/* Show the dropdown when hovering over the parent item */
.navbar-item:hover .dropdown {
  display: block;
  opacity: 1;
  visibility: visible;
  transform: translateY(0);
  transition: opacity 0.3s ease, visibility 0s, transform 0.3s ease;
}

/* Specific style for Dashboard link */
.dashboard-link {
  color: #3498db; /* Blue color for dashboard */
}

.dashboard-link:hover {
  color: #1abc9c; /* Color change on hover */
}

  </style>
  