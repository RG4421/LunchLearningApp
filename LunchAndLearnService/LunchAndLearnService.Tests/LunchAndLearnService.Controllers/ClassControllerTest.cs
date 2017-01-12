﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using LunchAndLearn.Management;
using LunchAndLearn.Management.Interfaces;
using LunchAndLearn.Model;
using LunchAndLearn.Model.DB_Models;
using LunchAndLearnService.Controllers;
using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.AutoMock.Ninject.Infrastructure.Language;

namespace LunchAndLearnService.Tests.LunchAndLearnService.Controllers
{
  [TestFixture]
  internal class ClassControllerTest
  {
    private List<Class> _mockClassList;
    private IManagerClass<Class> _classManager;

    [SetUp]
    public void Init()
    {
      _mockClassList = new List<Class>()
      {
        new Class() {ClassDescription = "test class description 1", ClassId = 1, ClassName = "Test Class 1"},
        new Class() {ClassDescription = "test class description 2", ClassId = 2, ClassName = "Test Class 2"}
      };

      _classManager = Mock.Create<IManagerClass<Class>>();
    }

    [TearDown]
    public void CleanUp()
    {
      _mockClassList = null;
      _classManager = null;
    }

    [Test]
    public void GetAll_UnderNormalConditions_DoesNotReturnNull()
    {
      // Arrange
      var classManager = Mock.Create<IManagerClass<Class>>();
      Mock.Arrange(() => classManager.GetAll()).Returns(_mockClassList).OccursOnce();
      var classController = new ClassController(classManager);

      //// Act
      var result = (OkNegotiatedContentResult<List<Class>>)classController.GetAll();

      var actualResult = result.Content;

      //// Assert
      Assert.IsNotNull(actualResult);
      Assert.That(actualResult.Count, Is.GreaterThan(1));
      Mock.Assert(classManager);
    }

    [Test]
    public void GetOneById_WhereIdExists_DoesNotReturnNull([Values(1, 2)]int idToRetrieve)
    {
      // Arrange
      var classManager = Mock.Create<IManagerClass<Class>>();
      var expected = _mockClassList.FirstOrDefault(x => x.ClassId == idToRetrieve);
      Mock.Arrange(() => classManager.Get(idToRetrieve))
        .Returns(_mockClassList.FirstOrDefault(x => x.ClassId == idToRetrieve))
        .OccursOnce();
      var classController = new ClassController(classManager);

      //// Act
      var result = (OkNegotiatedContentResult<Class>)classController.Get(idToRetrieve);

      var actualResult = result.Content;


      //Assert
      Mock.Assert(classManager);
      Assert.That(actualResult, Is.EqualTo(expected));
    }

    [Test]
    public void CreateClass_UnderNormalConditions_ReturnsOkResponse()
    {
      //Arrange
      var classToCreate = new Class()
      {
        ClassDescription = "This is a test class.",
        ClassId = 5,
        ClassName = "This is a test class name"
      };

      Mock.Arrange(() => _classManager.Create(classToCreate)).OccursOnce();
      var classController = new ClassController(_classManager);

      //Act

      var actual = classController.Post(classToCreate) as OkResult;

      //Assert
      Mock.Assert(_classManager);
      Assert.That(actual, Is.TypeOf<OkResult>());
    }

    [Test]
    public void UpdateClass_WhereClassExists_ReturnsOkResponse([Values(1,2,3)] int idOfClassToUpdate)
    {
      //Arrange
      var classToBeUpdated = _mockClassList.FirstOrDefault(x => x.ClassId == idOfClassToUpdate);

      Mock.Arrange(() => _classManager.Update(classToBeUpdated)).OccursOnce();

      var classController = new ClassController(_classManager);

      //Act
      var actual = classController.Put(classToBeUpdated) as OkResult;

      //Assert
      Mock.Assert(_classManager);
      Assert.That(actual, Is.TypeOf<OkResult>());
    }

    [Test]
    public void DeleteClass_WhereClassExists_ReturnsOkResponse([Values(1, 2, 3)] int idOfClassToBeDeleted)
    {
      //Arrange
      Mock.Arrange(() => _classManager.Delete(idOfClassToBeDeleted)).OccursOnce();
      var classController = new ClassController(_classManager);

      //Act
      var actual = classController.Delete(idOfClassToBeDeleted) as OkResult;

      //Assert
      Mock.Assert(_classManager);
      Assert.That(actual, Is.TypeOf<OkResult>());
    }
  }
}
