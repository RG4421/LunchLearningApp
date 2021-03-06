﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using LunchAndLearn.Management;
using LunchAndLearn.Management.Interfaces;
using LunchAndLearn.Model;
using LunchAndLearn.Model.DB_Models;
using LunchAndLearn.Model.DTOs;

namespace LunchAndLearnService.Controllers
{
  [RoutePrefix("api/track")]
  public class TrackController : ApiController
  {
    private readonly ITrackService _trackService;

    public TrackController(ITrackService trackService)
    {
      _trackService = trackService;
    }

    [HttpGet]
    [Route("all")]
    [ResponseType(typeof(List<TrackDto>))]
    public IHttpActionResult GetAll()
    {
      List<TrackDto> tracks;
      using (_trackService)
      {
        tracks = _trackService.GetAll(); 
      }
      return Ok(tracks);
    }

    [HttpGet]
    [Route("{id}")]
    [ResponseType(typeof(TrackDto))]
    public IHttpActionResult Get(int id)
    {
      TrackDto track;
      using (_trackService)
      {
        track = _trackService.Get(id); 
      }
      return Ok(track);
    }

    [HttpPost]
    [Route("create")]
    [ResponseType(typeof(TrackDto))]
    public IHttpActionResult Post(TrackDto track)
    {
      using (_trackService)
      {
        var response = _trackService.Create(track);
        return Created(new Uri(Request.RequestUri, $"{response.TrackId}"), response);
      }
    }

    [HttpPut]
    [Route("update")]
    [ResponseType(typeof(TrackDto))]
    public IHttpActionResult Put(TrackDto track)
    {
      using (_trackService)
      {
        var response = _trackService.Update(track); 
        return Ok(response);
      }
    }

    [HttpDelete]
    [Route("delete")]
    [ResponseType(typeof(OkResult))]
    public IHttpActionResult Delete(int id)
    {
      using (_trackService)
      {
        _trackService.Delete(id); 
      }
      return Ok();
    }
  }
}
