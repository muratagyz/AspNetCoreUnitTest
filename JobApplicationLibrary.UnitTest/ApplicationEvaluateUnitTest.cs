using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using Moq;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {
        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            // Arrange
            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };
            // Action
            var appResult = evaluator.Evaluate(form);

            // Assert

            Assert.AreEqual(appResult, ApplicationResult.AutoRejected);
        }

        [Test]
        public void Application_WithTechStackOver75_TransferredToAutoAccepted()
        {
            // Arrange
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                    IdentityNumber = ""
                },
                TechStackList = new List<string>() { "C#", "RabbitMQ", "Microservice", "Visual Studio" },
                YearsOfExperience = 16
            };
            // Action
            var appResult = evaluator.Evaluate(form);

            // Assert

            Assert.AreEqual(appResult, ApplicationResult.TransferredToHR);
        }

        [Test]
        public void Application_WithInValidIdentityNumber_TransferredToHR()
        {
            // Arrange
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                    IdentityNumber = ""
                },
                TechStackList = new List<string>() { "C#", "RabbitMQ", "Microservice", "Visual Studio" },
                YearsOfExperience = 16
            };
            // Action
            var appResult = evaluator.Evaluate(form);

            // Assert
            Assert.AreEqual(appResult, ApplicationResult.TransferredToHR);
        }

        [Test]
        public void Application_WithOficceLocation_TransferredToCTO()
        {
            // Arrange
            var mockValidator = new Mock<IIdentityValidator>();

            mockValidator.Setup(i => i.CountryDataProvider.countryData.Country).Returns("SPAIN");

            //var mockCountryData= new Mock<ICountryData>();
            //mockCountryData.Setup(i => i.Country).Returns("SPAIN");

            //var mockProvider = new Mock<ICountryDataProvider>();
            //mockProvider.Setup(i => i.countryData).Returns(mockCountryData.Object);

            //mockValidator.Setup(i => i.CountryDataProvider).Returns(mockProvider.Object);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                }
            };
            // Action
            var appResult = evaluator.Evaluate(form);

            // Assert
            Assert.AreEqual(appResult, ApplicationResult.TransferredToCTO);
        }
    }
}