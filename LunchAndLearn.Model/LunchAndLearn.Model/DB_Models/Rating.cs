﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LunchAndLearn.Model.DTOs;

namespace LunchAndLearn.Model.DB_Models
{
  [DataContract]
  public class Rating
  {
    [DataMember]
    [Key]
    public int RatingId { get; set; }
    [DataMember]
    public int ClassId { get; set; }
    [DataMember]
    public int ClassRating { get; set; }
    [DataMember]
    public int InstructorId { get; set; }
    [DataMember]
    public int InstructorRating { get; set; }
    [DataMember]
    public string Comment { get; set; }

    public virtual Class Class { get; set; }
    public virtual Instructor Instructor { get; set; }

    public RatingDto ConvertToRatingDto()
    {
      return new RatingDto()
      {
        RatingId = this.RatingId,
        InstructorId = this.InstructorId,
        ClassId = this.ClassId,
        ClassRating = this.ClassRating,
        InstructorRating = this.InstructorRating,
        Comment = this.Comment
      };
    }
  }
}
