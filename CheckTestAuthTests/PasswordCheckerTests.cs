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
    public class PasswordCheckerTests
    {
    
        [TestMethod()]
        public void CorrectPassword_ReturnsTrue()
        {
            // Arange
            string Password = "Pasha2004!#";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void CountSymbols8_ReturnsTrue()
        {
            // Arange
            string Password = "Pasha1337@";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void CountSymbolsFrom30_ReturnsFalse()
        {
            // Arange
            string Password = "Pasha2004Pasha2004Pasha2004###@!";
            bool Expected = true;
            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void Check_PasswordWithSpecSymbols_ReturnsFalse()
        {
            // Arange
            string Password = "Pasha19072004#";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void Check_PassowordWithOutSpecSymbols_ReturnsTrue()
        {
            // Arange
            string Password = "Pasha19072004";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void Check_PasswordWithBigLetter_ReturnsTrue()
        {
            // Arange
            string Password = "Sushkov143!";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void Check_PasswordWithoutBigLetter_ReturnsFalse()
        {
            // Arange
            string Password = "sushkov143!";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void Check_PasswordWithSmallLetter_ReturnsTrue()
        {
            // Arange
            string Password = "PASHa2004#";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod()]
        public void Check_PasswordWithoutSmallLetter_ReturnsFalse()
        {
            // Arange
            string Password = "PASHA2004#";
            bool Expected = true;

            // Act
            bool Actual = PasswordChecker.ValidatePassword(Password);

            // Assert
            Assert.AreEqual(Expected, Actual);
        }
    
    }
}