
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using System;
using System.Web;
using Xunit;


namespace _420_14B_FX_TP2_Test
{

    public class GestionCourseTests
    {

        /// <summary>
        /// Nom du fichier texte CSV contenant les informations sur la course pour les tests.
        /// </summary>
        public const String CHEMIN_FICHIER_COURSE = @"C:\data-420-14B-FX\TP2\Tests\courses.csv";

        /// <summary>
        /// Nom du fichier texte CSV contenant les informations sur les coureurs.
        /// </summary>
        public const String CHEMIN_FICHIER_COUREURS = @"C:\data-420-14B-FX\TP2\Tests\coureurs.csv";






        /// <summary>
        /// Permet la créationd'une course
        /// </summary>
        /// <returns>Une course</returns>
        private Course CreerCourse()
        {

            string nom = "Une course";
            string ville = "Une ville";
            Province province = Province.Quebec;
            TypeCourse type = TypeCourse.Sentier;
            ushort distance = 5;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);


            return new Course(Guid.NewGuid(), nom, date, ville, province, type, distance);
        }


        #region Constructeur
        [Fact]
        public void Constructeur_Devrait_ChargerCourses()
        {
            //Arrange
            GestionCourse gestion;

            //Act
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);

            //Assert
            Assert.NotNull(gestion.Courses);
            Assert.NotEmpty(gestion.Courses);

        }

        [Fact]
        public void ChargerCourses_Devrait_ChargerCoureurs()
        {
            //Arrange
            GestionCourse gestion;

            //Act
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);

            //Assert
            Assert.NotNull(gestion.Courses);
            Assert.NotEmpty(gestion.Courses);
            Assert.NotNull(gestion.Courses[0].Coureurs);
            Assert.NotEmpty(gestion.Courses[0].Coureurs);

        }

        #endregion

        #region AjouterCourse

        [Fact]
        public void AjouterCourse_Devrait_Lancer_ArgumentNullException_Quand_Cours_Null()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            Course course = null;


            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => gestion.AjouterCourse(null));

        }

        [Fact]
        public void AjouterCourse_Devrait_Lancer_InvalidOperationException_Quand_Cours_existe_deja()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            Course course = CreerCourse();
            gestion.AjouterCourse(course);
            Course courseDoublon = CreerCourse();



            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => gestion.AjouterCourse(courseDoublon));

        }


        [Fact]
        public void AjouterCourse_Devrait_Ajouter_Course()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            int valeurAttendue = gestion.Courses.Count + 1;

            //Act
            Course course = CreerCourse();
            gestion.AjouterCourse(course);
          
            //Assert
            Assert.Equal(valeurAttendue, gestion.Courses.Count);

        }


        [Fact]
        public void AjouterCourse_Devrait_Trier_Course()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            Course course1 = new Course(Guid.NewGuid(), "AAA", new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), "AAAA", Province.Alberta, TypeCourse.Sentier, 1);
            Course course2 = new Course(Guid.NewGuid(), "BBB", new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), "AAAA", Province.Alberta, TypeCourse.Sentier, 1);
            Course course3 = new Course(Guid.NewGuid(), "AAA", new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1), "AAAA", Province.Alberta, TypeCourse.Sentier, 1);



            //Act
            gestion.AjouterCourse(course1);
            gestion.AjouterCourse(course2);
            gestion.AjouterCourse(course3);

            //Assert
            Assert.Same(gestion.Courses[0], course3);
            Assert.Same(gestion.Courses[1], course1);
            Assert.Same(gestion.Courses[2], course2);

        }

        #endregion

        #region SupprimerCourse

        [Fact]
        public void SupprimerCourse_Devrait_Lancer_ArgumentNullException_Quand_Cours_Null()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            Course course = null;


            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => gestion.SupprimerCourse(null));

        }

        [Fact]
        public void SupprimerCourse_Devrait_Lancer_InvalidOperationException_Quand_Cours_existe_pas()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            Course course = CreerCourse();
            



            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => gestion.SupprimerCourse(course));

        }


        [Fact]
        public void SupprimerCourse_Devrait_Supprimer_Course()
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            int valeurAttendue = gestion.Courses.Count - 1;
            Course course = gestion.Courses[0];

            //Act

            gestion.SupprimerCourse(course);

            //Assert
            Assert.Equal(valeurAttendue, gestion.Courses.Count);

        }



        #endregion

        #region Enregistrer
        [Theory]
        [InlineData ("", CHEMIN_FICHIER_COUREURS)]
        [InlineData(null, CHEMIN_FICHIER_COUREURS)]
        [InlineData(" ", CHEMIN_FICHIER_COUREURS)]
        [InlineData (CHEMIN_FICHIER_COURSE, null)]
        [InlineData(CHEMIN_FICHIER_COURSE, "")]
        [InlineData(CHEMIN_FICHIER_COURSE, " ")]
        public void EnregistrerCourses_Devrait_Lancer_ArgumentNullException_Quand_Chemins_Fichier_Nul_Vide_Ou_Espaces(string cheminFichierCourse, string cheminFichierCoureurs)
        {
            //Arrange
            GestionCourse gestion;
            gestion = new GestionCourse(CHEMIN_FICHIER_COURSE, CHEMIN_FICHIER_COUREURS);
            


            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => gestion.EnregistrerCourses(cheminFichierCourse,cheminFichierCoureurs));

        }

        #endregion


    }
}
