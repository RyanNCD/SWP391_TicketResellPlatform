using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly TicketResellPlatformContext _context;
        public CategoryRepository(TicketResellPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Tickets)
                .ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(sc => sc.CategoryId.Equals(id));
        }
    }
}
