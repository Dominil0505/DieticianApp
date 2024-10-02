using DieticianApp.Data;
using DieticianApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class AdminFunctionBaseController : Controller
    {
        protected readonly AppDbContext _context;

        public AdminFunctionBaseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        protected async Task<List<T>> GetAllEntites<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        protected async Task<bool> EntityExist<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        protected async Task AddEntity<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        protected async Task UpdateEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        protected async Task DeleteEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        protected void SetTempDataMessage(string messageType, string message)
        {
            TempData[messageType] = message;
        }

        public async Task<PaginatedResult<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
