﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunchAndLearnMobile.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LunchAndLearnMobile.Services
{
  public class ClassService : BaseService
  {
    private static HttpClient _client;

    public static async Task<IEnumerable<DbClass>> GetClasses()
    {     
      List<DbClass> classes = new List<DbClass>();
      _client = CreateHttpClient();
      HttpResponseMessage response = await _client.GetAsync("api/class/all");
      if (response.IsSuccessStatusCode)
      {
        classes = await response.Content.ReadAsAsync<List<DbClass>>();
      }
      return classes;
    }
  }
}
