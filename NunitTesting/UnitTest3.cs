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
    public class CustomerLoginNunitTest
    {
        [Test]
        public void CheckCredentilas()
        {
            var customerModel = new CustomerModel { UserName = "Kavya09", Password = "Kavya@123" };
            Assert.DoesNotThrow(() => ValidateModel(customerModel));
        }
        private void ValidateModel(CustomerModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = ValidateForCredentials(model, validationContext);
            if (!CustomerExistsInDB(model))
            {
                throw new ValidationException("Customer does not exist in the database");
            }
        }
        private ValidationResult[] ValidateForCredentials(object model, ValidationContext validationContext)
        {
            // Helper method for model validation, checking only UserName and Password
            var validationResults = new List<ValidationResult>();
            var propertiesToCheck = new[] { "UserName", "Password" };

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Filter validation results to include only UserName and Password errors
            validationResults = validationResults
                .Where(result => propertiesToCheck.Contains(result.MemberNames.FirstOrDefault()))
                .ToList();

            return validationResults.ToArray();
        }

        private bool CustomerExistsInDB(CustomerModel model)
        {
            var connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=InsurancePolicyDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\" providerName=\"System.Data.SqlClient";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if there is a Customer with the specified credentials in the database
                using (var command = new SqlCommand($"SELECT COUNT(*) FROM Customers WHERE UserName = '{model.UserName}' AND Password = '{model.Password}'", connection))
                {
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }
    }
}