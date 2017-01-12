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

namespace LunchAndLearnService.Tests.LunchAndLearnService.Controllers
{
  [TestFixture]
  internal class RatingControllerTest
  {
    private IManagerClass<Rating> _ratingManager;
    private List<Rating> _ratingsList;

    [SetUp]
    public void Init()
    {
      _ratingManager = Mock.Create<IManagerClass<Rating>>();
      _ratingsList = new List<Rating>()
      {
        new Rating()
        {
          ClassId = 1,
          RatingId = 1,
          ClassRating = 1,
          Comment = "blah",
          InstructorId = 1,
          InstructorRating = 1
        },
        new Rating()
        {
          ClassId = 2,
          RatingId = 2,
          ClassRating = 2,
          Comment = "blah blah",
          InstructorId = 2,
          InstructorRating = 2
        },
        new Rating()
        {
          ClassId = 3,
          RatingId = 3,
          ClassRating = 3,
          Comment = "blah blah blah",
          InstructorId = 3,
          InstructorRating = 3
        }
      };
    }

    [TearDown]
    public void Cleanup()
    {
      _ratingManager = null;
      _ratingsList = null;
    }

    [Test]
    public void GetAllRatings_UnderNormalConditions_ReturnsMultipleRatings()
    {
      //Arrange
      Mock.Arrange(() => _ratingManager.GetAll()).Returns(_ratingsList).OccursOnce();

      var expected = _ratingsList;

      var ratingController = new RatingController(_ratingManager);

      //Act
      var actual = ratingController.GetAll() as OkNegotiatedContentResult<List<Rating>>;
      var actualContent = actual.Content;


      //Assert
      Mock.Assert(_ratingManager);
      Assert.That(actualContent, Is.EqualTo(expected));
    }

    [Test]
    public void GetRatingById_WhereRatingExists_ReturnsRatingMatchingIdArgument([Values(1,2,3)]int idOfRatingToBeFound)
    {
      //Arrange
      var expected = _ratingsList.FirstOrDefault(x => x.RatingId == idOfRatingToBeFound);
      Mock.Arrange(() => _ratingManager.Get(idOfRatingToBeFound))
        .Returns(_ratingsList.FirstOrDefault(x => x.RatingId == idOfRatingToBeFound))
        .OccursOnce();

      
      var ratingController = new RatingController(_ratingManager);

      //Act
      var actual = ratingController.Get(idOfRatingToBeFound) as OkNegotiatedContentResult<Rating>;
      var actualContent = actual.Content;


      //Assert
      Mock.Assert(_ratingManager);
      Assert.That(actualContent, Is.EqualTo(expected));
    }

    [Test]
    public void CreateRating_UnderNormalConditions_ReturnsOkResponse()
    {
      //Arrange
      var rating = new Rating()
      {
        InstructorId = 5,
        ClassId = 5,
        ClassRating = 5,
        Comment = "Creating a new rating comment..",
        InstructorRating = 5,
        RatingId = 5
      };
      Mock.Arrange(() => _ratingManager.Create(rating)).OccursOnce();
      var ratingController = new RatingController(_ratingManager);

      //Act
      var actual = ratingController.Post(rating) as OkResult;
      //Assert
      Mock.Assert(_ratingManager);
      Assert.IsNotNull(actual);
      Assert.That(actual, Is.TypeOf<OkResult>());
    }

    [Test]
    public void UpdateRating_WhereRatingExists_ReturnsOkResponse([Values(1,2,3)] int ratingIdToUpdate)
    {
      //arrange
      var rating = _ratingsList.FirstOrDefault(x => x.RatingId == ratingIdToUpdate);

      Mock.Arrange(() => _ratingManager.Update(rating)).OccursOnce();
      var ratingController = new RatingController(_ratingManager);

      //act
      var actual = ratingController.Put(rating) as OkResult;

      //assert
      Mock.Assert(_ratingManager);
      Assert.That(actual, Is.TypeOf<OkResult>());
    }

    [Test]
    public void DeleteRating_WhereRatingExists_ReturnsOkResponse([Values(1,2,3)] int ratingIdToDelete)
    {
      //arrange
      Mock.Arrange(() => _ratingManager.Delete(ratingIdToDelete)).OccursOnce();

      var ratingController = new RatingController(_ratingManager);

      //act
      var actual = ratingController.Delete(ratingIdToDelete) as OkResult;

      //assert
      Mock.Assert(_ratingManager);
      Assert.That(actual, Is.TypeOf<OkResult>());
    }
  }
}
