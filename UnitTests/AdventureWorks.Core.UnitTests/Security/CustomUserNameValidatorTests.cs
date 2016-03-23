using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureWorks.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;

namespace AdventureWorks.Core.Security.Tests
{
    [TestClass()]
    public class CustomUserNameValidatorTests
    {
        [TestMethod()]
        [TestCategory("Security")]
        public void ValidateCorrectUserNameAndPasswordShouldSuccess()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("Team_Project", "K@r@m@z0v");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateEmptyUserNameShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("", "K@r@m@z0v");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateNullUserNameShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate(null, "K@r@m@z0v");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateWhitespaceUserNameShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("   ", "K@r@m@z0v");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateEmptyPasswordShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("Team_Project", "");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateNullPasswordShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("Team_Project", null);
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateWhitespacePasswordShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("Team_Project", "     ");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(SecurityTokenException))]
        public void ValidateWrongUserNameShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("team_project", "K@r@m@z0v");
        }

        [TestMethod]
        [TestCategory("Security")]
        [ExpectedException(typeof(SecurityTokenException))]
        public void ValidateWrongPasswordShouldThrowError()
        {
            CustomUserNameValidator validator = new CustomUserNameValidator();
            validator.Validate("Team_Project", "Karamazov");
        }
    }
}