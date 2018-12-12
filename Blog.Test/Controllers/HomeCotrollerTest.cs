using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebUI.Controllers;

namespace Blog.Test.Controllers
{
    [TestClass]
    public class HomeCotrollerTest
    {
        [TestMethod]
        public void TestIndexView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Asserts
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestChatView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Chat() as ViewResult;

            // Asserts
            Assert.IsNotNull(result);
        }
    }
}
