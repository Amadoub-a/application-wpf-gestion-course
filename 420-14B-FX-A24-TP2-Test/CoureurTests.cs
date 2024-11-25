
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using System;

using Xunit;

namespace _420_14B_FX_TP2_Test
{
    public class CoureurTests
    {

        private Coureur CreerCoureur()
        {
            return new Coureur(1, "Vézina", "Martin", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 20, 0));
        }

        #region Dossard

        [Fact]
        public void SetDossard_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieur_A_DOSSARD_VAL_MIN()
        {
         
            //Arrange, Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coureur(Coureur.DOSSARD_VAL_MIN - 1, "Vézina", "Martin", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 20, 0)));


        }

        #endregion

        #region Nom

        [Fact]
        public void SetNom_Devrait_Lancer_ArgumentNulException_Quand_Value_Nulle_Ou_Vide()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
           
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => coureur.Nom = null);
            Assert.Throws<ArgumentNullException>(() => coureur.Nom = "");
            Assert.Throws<ArgumentNullException>(() => coureur.Nom = " ");
        }

        [Fact]
        public void SetNom_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieure_A_NOM_NB_CARC_MIN()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string nom1 = new string('x', Coureur.NOM_NB_CARC_MIN - 1);
            string nom2 = new string('x', Coureur.NOM_NB_CARC_MIN);

            //remplace le premier carctères par une chaîne vide
            char[] vectChar = nom2.ToCharArray();
            vectChar[0] = ' ';
            nom2 = new string(vectChar);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Nom = nom1);
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Nom = nom2);

        }

        [Fact]
        public void SetNom_Devrait_Modifier_Nom_Quand_Value_Superieur_Ou_Egale_A_NOM_NB_CARC_MIN()
        {
            //Arrange
            Coureur coureur = CreerCoureur();     
            string nom = new string('x', Coureur.NOM_NB_CARC_MIN);

            //Act
            coureur.Nom = nom;

            //Assert
            Assert.Equal(nom, coureur.Nom);
           
        }

      

        [Fact]
        public void SetNom_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string resultatAttendu = coureur.Nom;

            //Act
            coureur.Nom = " " +  coureur.Nom + " "; ;

            //Assert
            Assert.Equal(resultatAttendu, coureur.Nom
                );

        }


        #endregion

        #region Prenom

        [Fact]
        public void SetPrenom_Devrait_Lancer_ArgumentNulException_Quand_Value_Nulle_Ou_Vide()
        {
            //Arrange
            Coureur coureur = CreerCoureur();

            //Act and Assert           
            Assert.Throws<ArgumentNullException>(() => coureur.Prenom = null);
            Assert.Throws<ArgumentNullException>(() => coureur.Prenom = "");
            Assert.Throws<ArgumentNullException>(() => coureur.Prenom = " ");

        }

        [Fact]
        public void SetPrenom_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieure_A_PRENOM_NB_CARC_MIN()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string prenom1 = new string('x', Coureur.PRENOM_NB_CARC_MIN - 1);
            string prenom2 = new string('x', Coureur.PRENOM_NB_CARC_MIN);

            //remplace le premier carctères par une chaîne vide
            char[] vectChar = prenom2.ToCharArray();
            vectChar[0] = ' ';
            prenom2 = new string(vectChar);


            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Prenom = prenom1);
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Prenom = prenom2);

        }

        [Fact]
        public void SetPrenom_Devrait_Modifier_Nom_Quand_Value_Superieur_Ou_Egale_A_PRENOM_NB_CARC_MIN()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string prenom = new string('x', Coureur.PRENOM_NB_CARC_MIN);

            //Act
            coureur.Prenom = prenom;

            //Assert
            Assert.Equal(prenom, coureur.Prenom);

        }



        [Fact]
        public void SetPrenom_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string resultatAttendu = coureur.Prenom;

            //Act
            coureur.Prenom = " " + coureur.Prenom + " "; 

            //Assert
            Assert.Equal(resultatAttendu, coureur.Prenom);            

        }


        #endregion

        #region Categorie

        [Fact]
        public void SetCategorie_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inexistante()
        {

            //Arrange
            Coureur coureur = CreerCoureur();
            Categorie categorie = (Categorie) (-1);
          
            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Categorie = categorie);

        }

        [Fact]
        public void SetCategorie_Devrait_Modifier_Categorie_Quand_Value_Existe()
        {

            //Arrange
            Coureur coureur = CreerCoureur();

            //Act 
            coureur.Categorie = Categorie.M2029;

            //Assert
            Assert.Equal(Categorie.M2029, coureur.Categorie);

        }
        #endregion

        #region Ville

        [Fact]
        public void SetVille_Devrait_Lancer_ArgumentNullException_Quand_Value_Nulle_Ou_Vide()
        {
            //Arrange
            Coureur coureur = CreerCoureur();

            //Act & Assert
            Assert.Throws <ArgumentNullException>(() => coureur.Ville = null);
            Assert.Throws<ArgumentNullException>(() => coureur.Ville = "");
            Assert.Throws<ArgumentNullException>(() => coureur.Ville = " ");


        }

        [Fact]
        public void SetVille_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieure_A_VILLE_NB_CARC_MIN()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string ville1 = new string('x', Coureur.VILLE_NB_CARC_MIN - 1);
            string ville2 = new string('x', Coureur.VILLE_NB_CARC_MIN);

            //remplace le premier carctères par une chaîne vide
            char[] vectChar = ville2.ToCharArray();
            vectChar[0] = ' ';
            ville2 = new string(vectChar);


            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Ville = ville1);
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Ville = ville2);
        }

        [Fact]
        public void SetVille_Devrait_Modifier_Ville_Quand_Value_Superieure_Ou_Egale_A_VILLE_NB_CARC_MIN()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string ville = new string('x', Coureur.VILLE_NB_CARC_MIN);


            //Act
            coureur.Ville = ville;

            //Assert
            Assert.Equal(ville, coureur.Ville);

        }


        [Fact]
        public void SetVille_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange
            Coureur coureur = CreerCoureur();
            string resultatAttendu = coureur.Ville;

            //Act
            coureur.Ville = " " + coureur.Ville + " ";

            //Assert
            Assert.Equal(resultatAttendu, coureur.Ville);

        }


        #endregion

        #region Province
        [Fact]
        public void SetProvince_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inexistante()
        {

            //Arrange
            Coureur coureur = CreerCoureur();
            Province province = (Province)(-1);

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => coureur.Province = province);

        }

        [Fact]
        public void SetProvince_Devrait_Modifier_Province_Quand_Value_Existe()
        {

            //Arrange
            Coureur coureur = CreerCoureur();

            //Act 
            coureur.Province = Province.Quebec;

            //Assert
            Assert.Equal(Province.Quebec, coureur.Province);

        }

        #endregion

        #region constructeur
        [Fact]
        public void Constructeur_Devrait_Creer_Coureur_Quand_Proprietes_Valides()
        {
            //Arrange
            ushort dossard = 1;
            string nom = "Vézina";
            string prenom = "Martin";
            Categorie categorie = Categorie.M2029;
            Province province = Province.Quebec;
            string ville = "Québec";
            TimeSpan temps = new TimeSpan();


            //Act
            Coureur coureur = new Coureur(dossard,nom, prenom, categorie, ville, province, temps);

            //Assert
            Assert.Equal(coureur.Dossard, dossard);
            Assert.Equal(coureur.Nom, nom);
            Assert.Equal(coureur.Prenom, prenom);
            Assert.Equal(coureur.Categorie, categorie);
            Assert.Equal(coureur.Ville, ville);
            Assert.Equal(coureur.Province, province);
            Assert.True(coureur.Temps.Equals(temps));
            Assert.False(coureur.Abandon);

        }

            #endregion
    }
}
