
using Microsoft.EntityFrameworkCore;
using MKsEMS;
using MKsEMS.Data;
using MKsEMS.Models;
using System.ComponentModel.DataAnnotations;

namespace EMSTestProject
{
    public class UsersTests 
    {
        //EMSDbContext _contex;

        //public UsersControllerTests(EMSDbContext? context )
        //{
        //    _contex = context;
             
        //        //.UseInMemoryDatabase(databaseName: "TestDatabase")
        //        //.Options ;        
        //}

        [Fact]
        public void FirstName_ShouldHaveStringLengthBetween2And40()
        {
            // Arrange
            var user = new User();

            // Act
            var firstNameProperty = typeof(User).GetProperty(nameof(User.FirstName));
            var minLength = firstNameProperty.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;
            var maxLength = firstNameProperty.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;

            // Assert
            Assert.True(minLength.MinimumLength == 2);
            Assert.True(maxLength.MaximumLength == 40);
        }

        [Fact]
        public void Surname_ShouldHaveStringLengthBetween2And40()
        {
            // Arrange
            var user = new User();

            // Act
            var surnameProperty = typeof(User).GetProperty(nameof(User.Surname));
            var minLength = surnameProperty.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;
            var maxLength = surnameProperty.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;

            // Assert
            Assert.True(minLength.MinimumLength == 2);
            Assert.True(maxLength.MaximumLength == 40);
        }

        [Fact]
        public void Email_ShouldBeRequiredAndValidEmailAddress()
        {
            // Arrange
            var user = new User();

            // Act
            var emailProperty = typeof(User).GetProperty(nameof(User.Email));
            var requiredAttribute = emailProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var emailAttribute = emailProperty.GetCustomAttributes(typeof(EmailAddressAttribute), true)[0] as EmailAddressAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(emailAttribute);
        }
        [Fact]
        public void JobTitle_ShouldBeRequired()
        {
            // Arrange
            var user = new User();

            // Act
            var jobTitleProperty = typeof(User).GetProperty(nameof(User.JobTitle));
            var requiredAttribute = jobTitleProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void ManagerEmail_ShouldBeRequiredAndValidEmailAddress()
        {
            // Arrange
            var user = new User();

            // Act
            var managerEmailProperty = typeof(User).GetProperty(nameof(User.ManagerEmail));
            var requiredAttribute = managerEmailProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var emailAttribute = managerEmailProperty.GetCustomAttributes(typeof(EmailAddressAttribute), true)[0] as EmailAddressAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(emailAttribute);
        }

        [Fact]
        public void DOB_ShouldBeRequiredAndHaveDataTypeDate()
        {
            // Arrange
            var user = new User();

            // Act
            var dobProperty = typeof(User).GetProperty(nameof(User.DOB));
            var requiredAttribute = dobProperty.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;
            var dataTypeAttribute = dobProperty.GetCustomAttributes(typeof(DataTypeAttribute), true)[0] as DataTypeAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.NotNull(dataTypeAttribute);
            Assert.Equal(DataType.Date, dataTypeAttribute.DataType);
        }


    }
}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MKsEMS.Controllers;
//using MKsEMS.Data;
//using MKsEMS.Models;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace EMSTestProject
//{
//    public class UsersControllerTests
//    {
//        [Fact]
//        public async Task Index_ReturnsViewResultWithListOfUsers()
//        {
//            // Arrange
//            var options = new DbContextOptionsBuilder<EMSDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            using (var context = new EMSDbContext(options))
//            {
//                var users = new List<User>
//                {
//                    new User { Id = 1, FirstName = "User 1" },
//                    new User { Id = 2, FirstName = "User 2" },
//                    new User { Id = 3, FirstName = "User 3" }
//                };

//                context.Users.AddRange(users);
//                context.SaveChanges();
//            }

//            using (var context = new EMSDbContext(options))
//            {
//                var controller = new UsersController(context, null);

//                // Act
//                var result = await controller.Index();

//                // Assert
//                var viewResult = Assert.IsType<ViewResult>(result);
//                var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
//                Assert.Equal(3, model.Count());
//            }
//        }
//    }
//}



////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;
////using MKsEMS.Controllers;
////using MKsEMS.Data;
////using MKsEMS.Models;
////using MKsEMS.ViewModels;
////using Moq;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Xunit;




//////public class DbSetWrapper<T> : IDbSetWrapper<T> where T : class
//////{
//////    private readonly DbSet<T> _dbSet;

//////    public DbSetWrapper(DbSet<T> dbSet)
//////    {
//////        _dbSet = dbSet;
//////    }

//////    public IQueryable<T> Queryable => _dbSet;

//////    public T Add(T entity) => _dbSet.Add(entity).Entity;

//////    public T Remove(T entity) => _dbSet.Remove(entity).Entity;
//////}

////public class UsersControllerTests
////{
////    // Update the UsersController constructor to accept IEMSDbContext
////    public UsersControllerTests(IEMSDbContext context, CurrentUser2 currentUser)
////    {
////        _context = context;
////        _currentUser = currentUser;
////        _filteredObjects = new AllDropDownListData(context);
////    }


////    // Update the test code to mock IEMSDbContext instead of EMSDbContext
////    [Fact]
////    public async Task Index_ReturnsViewResultWithListOfUsers()
////    {
////        // Arrange
////        var users = new List<User>
////    {
////        new User { Id = 1, FirstName = "User 1" },
////        new User { Id = 2, FirstName = "User 2" },
////        new User { Id = 3, FirstName = "User 3" }
////    };

////        var mockDbSet = new Mock<DbSet<User>>();
////        mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
////        mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
////        mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
////        mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

////        var mockContext = new Mock<IEMSDbContext>();
////        mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

////        var controller = new UsersController(mockContext.Object, null);

////        // Act
////        var result = await controller.Index();

////        // Assert
////        var viewResult = Assert.IsType<ViewResult>(result);
////        var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
////        Assert.Equal(users.Count, model.Count());
////    }

////    //[Fact]
////    //public async Task Index_ReturnsViewResultWithListOfUsers()
////    //{
////    //    // Arrange
////    //    var users = new List<User>
////    //{
////    //    new User { Id = 1, FirstName = "User 1" },
////    //    new User { Id = 2, FirstName = "User 2" },
////    //    new User { Id = 3, FirstName = "User 3" }
////    //};

////    //    var mockSet = new Mock<DbSet<User>>();

////    //    mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
////    //    mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
////    //    mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
////    //    mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

////    //    var mockContext = new Mock<EMSDbContext>();
////    //    mockContext.Setup(c => c.Users).Returns(mockSet.Object);

////    //    var controller = new UsersController(mockContext.Object, null);

////    //    // Act
////    //    var result = await controller.Index();

////    //    // Assert
////    //    var viewResult = Assert.IsType<ViewResult>(result);
////    //    var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
////    //    Assert.Equal(users.Count, model.Count());
////    //}



////    //    private static DbSet<T> CreateMockDbSet<T>(IEnumerable<T> data) where T : class
////    //    {
////    //        var queryable = data.AsQueryable();

////    //        var mockSet = new Mock<DbSet<T>>();
////    //        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
////    //        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
////    //        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
////    //        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

////    //        return mockSet.Object;
////    //    }


////    //    [Fact]
////    //    async Task Index_ReturnsViewResultWithListOfUsers()
////    //    {
////    //        // Arrange
////    //        var users = new List<User>
////    //    {
////    //        new User { Id = 1, FirstName = "User 1" },
////    //        new User { Id = 2, FirstName = "User 2" },
////    //        new User { Id = 3, FirstName = "User 3" }
////    //    };

////    //        var mockContext = new Mock<EMSDbContext>();
////    //        mockContext.Setup(c => c.Users).Returns(CreateMockDbSet(users));

////    //        var controller = new UsersController(mockContext.Object, null);

////    //        // Act
////    //        var result = await controller.Index();

////    //        // Assert
////    //        var viewResult = Assert.IsType<ViewResult>(result);
////    //        var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
////    //        Assert.Equal(users.Count, model.Count());
////    //    }




////}
////}