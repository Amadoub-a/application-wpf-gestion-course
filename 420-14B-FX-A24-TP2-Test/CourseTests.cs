
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using System;

using Xunit;



namespace _420_14B_FX_TP2_Test
{

    public class CourseTests
    {

        private Coureur CreerCoureur()
        {
            return new Coureur(1, "Vézina", "Martin", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 20, 0));
        }

        private Course CreerCourse()
        {

            string nom = "Une course";
            string ville = "Une ville";
            Province province = Province.Quebec;
            TypeCourse type = TypeCourse.Sentier;
            ushort distance = 5;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);


            return new Course(Guid.NewGuid(),nom, date, ville, province, type, distance);
        }

        #region Id
        [Fact]
        public void SetId_Devrait_Lancer_ArgumentException_Quand_Value_Vide()
        {
            //Arrange
            string nom = "Une course";
            string ville = "Une ville";
            Province province = Province.Quebec;
            TypeCourse type = TypeCourse.Sentier;
            ushort distance = 5;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);


         
            //Act and Assert
            Assert.Throws<ArgumentException>(() => new Course(Guid.Empty, nom, date, ville, province, type, distance));
      
        }
        #endregion

        #region Nom

        [Fact]
        public void SetNom_Devrait_Lancer_ArgumentNulException_Quand_Value_Nulle_Ou_Vide()
        {
            //Arrange
            Course course = CreerCourse();
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => course.Nom = null);
            Assert.Throws<ArgumentNullException>(() => course.Nom = "");
            Assert.Throws<ArgumentNullException>(() => course.Nom = " ");
        }

        [Fact]
        public void SetNom_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieure_A_NOM_NB_CAR_MIN()
        {
            //Arrange
            Course course = CreerCourse();
            string nom = new string('x', Course.NOM_NB_CAR_MIN - 1);


            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => course.Nom = nom);

        }

        [Fact]
        public void SetNom_Devrait_Modifier_Nom_En_Majuscule_Quand_Value_Superieur_Ou_Egale_A_NOM_NB_CAR_MIN()
        {
            //Arrange
            Course course = CreerCourse();
            string nom = new string('x', Course.NOM_NB_CAR_MIN).ToUpper();

            //Act
            course.Nom = nom;

            //Assert
            Assert.Equal(nom, course.Nom);

        }



        [Fact]
        public void SetNom_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange
            Course course = CreerCourse();
            course.Nom = "Une course";
            string resultatAttendu = course.Nom.ToUpper();

            //Act
            course.Nom = " " + course.Nom + " "; ;

            //Assert
            Assert.Equal(resultatAttendu, course.Nom);

        }


        #endregion

        #region Ville

        [Fact]
        public void SetVille_Devrait_Lancer_ArgumentNullException_Quand_Value_Nulle_Ou_Vide()
        {
            //Arrange
            Course course = CreerCourse();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => course.Ville = null);
            Assert.Throws<ArgumentNullException>(() => course.Ville = "");
            Assert.Throws<ArgumentNullException>(() => course.Ville = " ");


        }

        [Fact]
        public void SetVille_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieure_A_VILLE_NB_CAR_MIN()
        {
            //Arrange
            Course course = CreerCourse();
            string ville1 = new string('x', Course.VILLE_NB_CAR_MIN - 1);
            string ville2 = new string('x', Course.VILLE_NB_CAR_MIN);

            //remplace le premier carctères par une chaîne vide
            char[] vectChar = ville2.ToCharArray();
            vectChar[0] = ' ';
            ville2 = new string(vectChar);


            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => course.Ville = ville1);
            Assert.Throws<ArgumentOutOfRangeException>(() => course.Ville = ville2);
        }

        [Fact]
        public void SetVille_Devrait_Modifier_Ville_Quand_Value_Superieure_Ou_Egale_A_VILLE_NB_CAR_MIN()
        {
            //Arrange
            Course course = CreerCourse();
            string ville = new string('x', Course.VILLE_NB_CAR_MIN);


            //Act
            course.Ville = ville;

            //Assert
            Assert.Equal(ville, course.Ville);

        }


        [Fact]
        public void SetVille_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange
            Course course = CreerCourse();
            course.Ville = "Québec";
            string resultatAttendu = course.Ville;

            //Act
            course.Ville = " " + course.Ville + " ";

            //Assert
            Assert.Equal(resultatAttendu, course.Ville);

        }


        #endregion

        #region Province
        [Fact]
        public void SetProvince_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inexistante()
        {

            //Arrange
            Course course = CreerCourse();
            Province province = (Province)(-1);

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => course.Province = province);

        }

        [Fact]
        public void SetProvince_Devrait_Modifier_Province_Quand_Value_Existe()
        {

            //Arrange
            Course course = CreerCourse();

            //Act 
            course.Province = Province.Quebec;

            //Assert
            Assert.Equal(Province.Quebec, course.Province);

        }

        #endregion

        #region TypeCourse
        [Fact]
        public void SetTypeCourse_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inexistante()
        {

            //Arrange
            Course course = CreerCourse();
            TypeCourse type = (TypeCourse)(-1);

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => course.TypeCourse = type);

        }

        [Fact]
        public void SetTypeCourse_Devrait_Modifier_TypeCourse_Quand_Value_Existe()
        {

            //Arrange
            Course course = CreerCourse();

            //Act 
            course.TypeCourse = TypeCourse.Sentier;

            //Assert
            Assert.Equal(TypeCourse.Sentier, course.TypeCourse);

        }

        #endregion

        #region Distance

        [Fact]
        public void SetDistance_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_Inferieure_A_DISTANCE_MIN_VAL()
        {
            //Arrange
            Course course = CreerCourse();
            ushort distance = Course.DISTANCE_VAL_MIN - 1;

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => course.Distance = distance);
        }
        #endregion

        #region NbParticipants

        [Fact]
        public void GetNbParticipants_Devrait_Retourner_Nb_Coureurs()
        {

            //Arrange
            Course course = CreerCourse();
            Coureur coureur = CreerCoureur();

            //Act
            course.AjouterCoureur(coureur);

            //Assert
            Assert.Equal(1, course.NbParticipants);

        }



        #endregion

        #region TempsCourseMoyen

        [Fact]
        public void TempsCourseMoyen_Devrait_Retourner_Le_Temps_De_Course_Moyen()
        {
            //Arrange
            Course course = CreerCourse();

            Coureur coureur1 = CreerCoureur();
            coureur1.Temps = new TimeSpan(0, 10, 0);
           
            Coureur coureur2 = new Coureur(2, "Boucher", "Jean-Philippe", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 20, 0));
            coureur2.Abandon = true;
          
            Coureur coureur3 = new Coureur(3, "Pépin", "Jean-François", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 0, 0));
            coureur3.Temps = new TimeSpan(0, 5, 0);
        
            Coureur coureur4 = new Coureur(4, "Hardy", "Martin", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 0, 0));

    

            double resulatAttendu = 450000;

            //Act
            course.AjouterCoureur(coureur1);
            course.AjouterCoureur(coureur2);
            course.AjouterCoureur(coureur3);
            course.AjouterCoureur(coureur4);

            //Assert
            Assert.Equal(resulatAttendu, course.TempCourseMoyen.TotalMilliseconds);

        }
        #endregion

        #region Constructeur

        [Fact]
        public void Constructeur_Devrait_Creer_Course_Quand_Proprietes_Valides()
        {
            //Arrange 
            string nom = "Une course";
            string ville = "Une ville";
            Province province = Province.Quebec;
            TypeCourse type = TypeCourse.Sentier;
            ushort distance = 5;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            //Arct
            Course course = new Course(Guid.NewGuid(),nom, date, ville, province, type, distance);

            //Assert
            Assert.Equal(nom.ToUpper(), course.Nom);
            Assert.Equal(date, course.Date);
            Assert.Equal(ville, course.Ville);
            Assert.Equal(province, course.Province);
            Assert.Equal(type, course.TypeCourse);
            Assert.Equal(distance, course.Distance);
            Assert.NotNull(course.Coureurs);
            Assert.Empty(course.Coureurs);

        }

        [Fact]
        public void Constructeur_Devrait_Creer_Course_Avec_Liste_De_Coureurs_Vide()
        {
            //Arrange & Act
            Course course = CreerCourse();
            //Assert
            Assert.NotNull(course.Coureurs);
            Assert.Empty(course.Coureurs);


        }
        #endregion

        #region AjouterCoureur

        [Fact]
        public void AjouterCoureur_Devrait_Lancer_ArgumentNullException_Quand_Coureur_Nul()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur = null;


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => course.AjouterCoureur(coureur));

        }

        [Fact]
        public void AjouterCoureur_Devrait_Ajouter_Coureur_Quand_Valide()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur = CreerCoureur();

            course.AjouterCoureur(coureur);


            //Act & Assert
            Assert.Contains(coureur, course.Coureurs);


        }

        [Fact]
        public void AjouterCoureur_Devrait_Lancer_InvalidOperationException_Dossard_Existe_Deja()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur1 = CreerCoureur();

            course.AjouterCoureur(coureur1);

            Coureur coureur2 = CreerCoureur();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => course.AjouterCoureur(coureur2));

        }

        [Fact]
        public void AjouterCoureur_Trier_Coureurs()
        {
            //Arrange
            Course course = CreerCourse();

            Coureur coureur1 = CreerCoureur();
            coureur1.Temps = new TimeSpan(0, 10, 0);
            course.AjouterCoureur(coureur1);

            Coureur coureur2 = new Coureur(2, "Boucher", "Jean-Philippe", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 0, 0));
            coureur2.Temps = new TimeSpan(0, 5, 0);
            course.AjouterCoureur(coureur2);


            //Act & Assert
            Assert.Equal(coureur2, course.Coureurs[0]);


        }


        #endregion

        #region ObtenirCoureurParNoDossard Tests

        [Fact] 
        public void ObtenirCoureurParNoDossard_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Dossard_Inferieur_A_DOSSARD_VAL_MIN()
        {
            //Arrange
            Course course = CreerCourse();
            ushort noDossard = Coureur.DOSSARD_VAL_MIN -1;

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => course.ObtenirCoureurParNoDossard(noDossard));
        }

        [Fact]
        public void ObtenirCoureurParNoDossard_Devrait_Retourner_Null_Quand_Dossard_Non_Trouve()
        {
            //Arrange
            Course course = CreerCourse();
            ushort noDossard = Coureur.DOSSARD_VAL_MIN;

            //Act & Assert
            Assert.Null(course.ObtenirCoureurParNoDossard(noDossard));
        }


        [Fact]
        public void ObtenirCoureurParNoDossard_Devrait_Retourner_Courreur_Quand_Dossard_Trouve()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur = CreerCoureur();

            course.AjouterCoureur(coureur);

            //Act & Assert
            Assert.Equal(coureur, course.ObtenirCoureurParNoDossard(coureur.Dossard));
        }



        #endregion

        #region SupprimerCoureur Test

        [Fact]
        public void SupprimerCoureur_Devrait_Lancer_ArgumentNullException_Quand_Coureur_Null()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur = null;


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => course.SupprimerCoureur(coureur));
        }

        [Fact]
        public void SupprimerCoureur_Devrait_Lancer_InvalidOperationException_Quand_Coureur_Inexistant()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur = CreerCoureur();


            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => course.SupprimerCoureur(coureur));
        }

        [Fact]
        public void SupprimerCoureur_Devrait_Retirer_Coureur_Quand_Coureur_Existe()
        {
            //Arrange
            Course course = CreerCourse();
            Coureur coureur = CreerCoureur();
            course.AjouterCoureur(coureur);

            //Act
            course.SupprimerCoureur(coureur);

            // Assert
            Assert.DoesNotContain(coureur, course.Coureurs);
        }


        #endregion

        #region TrierCoureurs Test


        [Fact]
        public void TrierCoureur_Devrait_Trier_Les_Coureurs()
        {
                //Arrange
                Course course = CreerCourse();

                Coureur coureur1 = CreerCoureur();
                coureur1.Nom = "coureur 1";
                coureur1.Temps = new TimeSpan(0, 10, 0);
                course.AjouterCoureur(coureur1);

                Coureur coureur2 = new Coureur(2, "Boucher", "Jean-Philippe", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 0, 0));
                coureur2.Abandon = true;
                course.AjouterCoureur(coureur2);

           
                Coureur coureur3 = new Coureur(3, "Pépin", "Jean-François", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 0, 0));
                coureur3.Temps = new TimeSpan(0, 5, 0);
                course.AjouterCoureur(coureur3);

                Coureur coureur4 = new Coureur(4, "Hardy", "Martin", Categorie.M4049, "Québec", Province.Quebec, new TimeSpan(0, 0, 0));
                course.AjouterCoureur(coureur4);

                course.TrierCoureurs();

                //Act & Assert
                Assert.Equal(1, course.Coureurs.IndexOf(coureur1));

                Assert.True(course.Coureurs.IndexOf(coureur2) == 2 || course.Coureurs.IndexOf(coureur2) == 3);

                Assert.Equal(0, course.Coureurs.IndexOf(coureur3));

                Assert.True(course.Coureurs.IndexOf(coureur4) == 2 || course.Coureurs.IndexOf(coureur4) == 3);

        }
        #endregion




    }
}
