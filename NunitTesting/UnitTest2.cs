using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UILayer.Models;

namespace NunitTesting
{
    [TestFixture]
    public class AdminLoginNunitTest
    {

        [Test]
        public void Checkcredentials()
        {
            var loginView = new LoginModel { UserName = "pree09", Password = "Ram@1234" };
            Assert.DoesNotThrow(() => ValidateModel(loginView));
        }

        private void ValidateModel(LoginModel model)
        {
            // Perform model validation using DataAnnotations
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = ValidateModel(model, validationContext);

            if (validationResults.Length > 0)
            {
                var errorMessage = FormatValidationResults(validationResults);
                throw new ValidationException(errorMessage);
            }

            // Now check the credentials against the database using your data access logic
            if (!AdminExistsInDatabase(model))
            {
                throw new ValidationException("Admin does not exist in the database");
            }
        }
        private ValidationResult[] ValidateModel(object model, ValidationContext validationContext)
        {
            // Helper method for model validation
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults.ToArray();
        }
        private string FormatValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            // Helper method to format validation results as a string
            var messages = validationResults.Select(result => result.ErrorMessage);
            return string.Join(Environment.NewLine, messages);
        }
        private bool AdminExistsInDatabase(LoginModel model)
        {

            var connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=InsurancePolicyDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\" providerName=\"System.Data.SqlClient";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand($"SELECT COUNT(*) FROM Admins WHERE UserName = '{model.UserName}' AND Password = '{model.Password}'", connection))
                {
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }
    }
}
