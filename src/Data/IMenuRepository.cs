using CoreCodeCamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
  public interface IMenuRepository
  {
    // General 
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveChangesAsync();

        // Customer
    Task<Menu[]> GetAllCampsAsync();

  }
}