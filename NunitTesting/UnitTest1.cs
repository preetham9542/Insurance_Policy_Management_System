using UILayer.Models;
using UILayer.Controllers;
using System.ComponentModel.DataAnnotations;

namespace NunitTesting
{
    public class Tests
    {
        [Test]
        public void Email_Required()
        {
            // Arrange
            var model = new ForgotPasswordModel
            {
                Username = "kavya",
                NewPassword = "Kavya@123",
                ConfirmPassword = "Kavya@123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => Validate(model));
        }
        private void Validate(ForgotPasswordModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }

        [Test]
        public void Password_Required()
        {
            // Arrange
            var model = new ForgotPasswordModel
            {
                Username = "ramya",
                EmailAddress = "ramya@mail.com",
                ConfirmPassword = "Ramya@123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => Validate(model));
        }
        [Test]
        public void ConfirmPassword_Required()
        {
            // Arrange
            var model = new ForgotPasswordModel
            {
                Username = "ramya",
                EmailAddress = "ramya@mail.com",
                NewPassword = "Ramya@123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => Validate(model));
        }
        [Test]
        public void RoleName_Required()
        {
            var rolemodel = new RoleModel
            {
                RoleId = 1
            };
            Assert.Throws<ValidationException>(() => ValidateModel(rolemodel));
        }
        private void ValidateModel(RoleModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }
        [Test]
        public void PolicyNumber_Required()
        {

            var policyModel = new PolicyModel
            {
                DateOfCreation = DateTime.Now,
                Category = "Life"
            };


            Assert.Throws<ValidationException>(() => ValidateModel(policyModel));
        }

        [Test]
        public void AppliedDate_Required()
        {

            var policyModel = new PolicyModel
            {
                PolicyNumber = "ABC123",
                Category = "Travel",
                DateOfCreation = DateTime.Now
            };


            Assert.DoesNotThrow(() => ValidateModel(policyModel));
        }
        private void ValidateModel(PolicyModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }
        [Test]
        public void QuestionId_NotRequired()
        {
            var questionModel = new QuestionModel
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            Assert.DoesNotThrow(() => ValidateModel2(questionModel));
        }

        [Test]
        public void Question_LengthWithinLimit()
        {

            var questionModel = new QuestionModel
            {
                Question = new string('A', 255),
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            Assert.DoesNotThrow(() => ValidateModel2(questionModel));
        }
        private void ValidateModel2(QuestionModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }


    }
}