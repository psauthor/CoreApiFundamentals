using CoreCodeCamp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MenuContext _context;
        private readonly ILogger<MenuRepository> _logger;

        public MenuRepository(MenuContext context, ILogger<MenuRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }


        public async Task<Menu[]> GetAllCampsAsync()
        {
            
            IQueryable<Menu> query = _context.Menu
                .Include(c => c.Id);



            // Order It
            //query = query.OrderByDescending(c => c.EventDate);

            return await query.ToArrayAsync();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
