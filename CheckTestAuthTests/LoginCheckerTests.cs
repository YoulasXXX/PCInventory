using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckTestAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTestAuth.Tests
{
    [TestClass()]
    public class LoginCheckerTests
    {
        [TestMethod()]
        public void Check4Symbols_ReturnsTrue()
        {
            // Arrange
            string login = "Pasha";
            bool expected = true;
            // Act
            bool actual = LoginChecker.ValidateLogin(login);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Check_LoginWithSpecSymbols_ReturnsFalse()
        {
            // Arrange
            string login = "Sushkov#";
            bool expected = false;

            // Act
            bool actual = LoginChecker.ValidateLogin(login);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Check_LoginWithOutSpecSymbols_ReturnsTrue()
        {
            // Arrange
            string login = "Sushkov";
            bool expected = true;

            // Act
            bool actual = LoginChecker.ValidateLogin(login);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}