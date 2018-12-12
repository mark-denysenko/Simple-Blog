using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebUI.Validation.Attributes;
using WebUI.Validation.Filters;

namespace Blog.Test.Controllers
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void ValidateUserNickname()
        {
            // Arrange
            string userNicknameAdmin = "administrator";
            string userNicknameNotAdmin = "someUser";
            NameNotAdminValidateAttribute validator = new NameNotAdminValidateAttribute();

            // Act
            bool isValidNicknameAdmin = validator.IsValid(userNicknameAdmin);
            bool isValidNicknameNotAdmin = validator.IsValid(userNicknameNotAdmin);

            // Asserts
            Assert.IsFalse(isValidNicknameAdmin);
            Assert.IsTrue(isValidNicknameNotAdmin);
        }

        [TestMethod]
        public void ValidatePageNumber()
        {
            // Arrange
            var contextPositivePage = new ActionExecutingContext();
            contextPositivePage.ActionParameters["page"] = 10;
            var contextZeroPage = new ActionExecutingContext();
            contextZeroPage.ActionParameters["page"] = 0;
            var contextNegativePage = new ActionExecutingContext();
            contextNegativePage.ActionParameters["page"] = -5;
            ValidatePageParameterAttribute filter = new ValidatePageParameterAttribute();

            // Act
            try
            {
                filter.OnActionExecuting(contextPositivePage);
            }
            catch(ArgumentException)
            {
                Assert.Fail();
            }

            try
            {
                filter.OnActionExecuting(contextZeroPage);
                Assert.Fail();
            }
            catch (ArgumentException) { }

            try
            {
                filter.OnActionExecuting(contextNegativePage);
                Assert.Fail();
            }
            catch (ArgumentException) { }
        }
    }
}
