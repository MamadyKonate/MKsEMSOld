
using Microsoft.EntityFrameworkCore;
using MKsEMS;
using MKsEMS.Data;
using MKsEMS.Models;
using System.ComponentModel.DataAnnotations;

namespace EMSTestProject
{
    public class LeavesTests
    {
        [Fact]
        public void UserEmail_ShouldBeRequiredAndValidEmailAddress()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var userEmailProperty = typeof(Leave).GetProperty(nameof(Leave.UserEmail));
            var requiredAttribute = userEmailProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var emailAttribute = userEmailProperty.GetCustomAttributes(typeof(EmailAddressAttribute), true)[0] as EmailAddressAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(emailAttribute);
        }

        [Fact]
        public void ManagerEmail_ShouldBeRequiredAndValidEmailAddress()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var managerEmailProperty = typeof(Leave).GetProperty(nameof(Leave.ManagerEmail));
            var requiredAttribute = managerEmailProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var emailAttribute = managerEmailProperty.GetCustomAttributes(typeof(EmailAddressAttribute), true)[0] as EmailAddressAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(emailAttribute);
        }

        [Fact]
        public void DateFrom_ShouldBeRequiredAndHaveDataTypeDate()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var dateFromProperty = typeof(Leave).GetProperty(nameof(Leave.DateFrom));
            var requiredAttribute = dateFromProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var dataTypeAttribute = dateFromProperty.GetCustomAttributes(typeof(DataTypeAttribute), true)[0] as DataTypeAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(dataTypeAttribute);
            Assert.Equal(DataType.Date, dataTypeAttribute.DataType);
        }

        [Fact]
        public void DateTo_ShouldBeRequiredAndHaveDataTypeDate()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var dateToProperty = typeof(Leave).GetProperty(nameof(Leave.DateTo));
            var requiredAttribute = dateToProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var dataTypeAttribute = dateToProperty.GetCustomAttributes(typeof(DataTypeAttribute), true)[0] as DataTypeAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(dataTypeAttribute);
            Assert.Equal(DataType.Date, dataTypeAttribute.DataType);
        }

        [Fact]
        public void NumberOfDays_CanBeNull()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var numberOfDaysProperty = typeof(Leave).GetProperty(nameof(Leave.numberOfDays));

            // Assert
            Assert.True(numberOfDaysProperty.PropertyType == typeof(int?));
        }

        [Fact]
        public void LeaveType_ShouldBeSet()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var leaveTypeProperty = typeof(Leave).GetProperty(nameof(Leave.LeaveType));

            // Assert
            Assert.NotNull(leaveTypeProperty);
        }

        [Fact]
        public void Status_ShouldBeSet()
        {
            // Arrange
            var leave = new Leave();

            // Act
            var statusProperty = typeof(Leave).GetProperty(nameof(Leave.Status));

            // Assert
            Assert.NotNull(statusProperty);
        }

    }
}