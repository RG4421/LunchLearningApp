﻿using LunchAndLearn.Data;
using LunchAndLearn.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunchAndLearn.Data.Interfaces;
using LunchAndLearn.Data.Repositories;
using LunchAndLearn.Management.Interfaces;
using LunchAndLearn.Model.DB_Models;
using LunchAndLearn.Model.DTOs;

namespace LunchAndLearn.Management
{
  public class TrackSessionService : ITrackSessionService
  {
    private readonly ITrackSessionRepository _trackSessionRepository;

    public TrackSessionService(ITrackSessionRepository trackSessionRepository)
    {
      _trackSessionRepository = trackSessionRepository;
    }

    public TrackSessionDto Get(int id)
    {
      using (_trackSessionRepository)
      {
        return _trackSessionRepository.Get(id).ConvertToTrackSessionDto(); 
      }
    }

    public List<TrackSessionDto> GetAll()
    {
      using (_trackSessionRepository)
      {
        var trackSessionList = _trackSessionRepository.GetAll().ToList();
        return trackSessionList.Select(x => x.ConvertToTrackSessionDto()).ToList();
      }
    }

    public TrackSessionDto Create(TrackSessionDto entity)
    {
      using (_trackSessionRepository)
      {
        var entityToCreate = entity.ConvertToTrackSessionDbModel();

        _trackSessionRepository.Create(entityToCreate);
        _trackSessionRepository.SaveChanges();

        return entityToCreate.ConvertToTrackSessionDto();
      }
    }

    public TrackSessionDto Update(TrackSessionDto entity)
    {
      using (_trackSessionRepository)
      {
        var entityToUpdate = entity.ConvertToTrackSessionDbModel();

        _trackSessionRepository.Update(entityToUpdate);
        _trackSessionRepository.SaveChanges();

        return entityToUpdate.ConvertToTrackSessionDto();
      }
    }

    public void Delete(int id)
    {
      using (_trackSessionRepository)
      {
        _trackSessionRepository.Delete(id);
        _trackSessionRepository.SaveChanges(); 
      }
    }

    #region Disposal
    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this._disposed)
      {
        if (disposing)
        {
          _trackSessionRepository.Dispose();
        }
      }
      this._disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}
