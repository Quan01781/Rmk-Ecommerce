﻿using API_web.Data;
using API_web.Interfaces;
using API_web.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedCommonModel.Admin;
using SharedCommonModel.Category;
using System.Data.Common;

namespace API_web.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly WebDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(WebDbContext WebDbContext, IMapper mapper)
        {
            _context = WebDbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _context.Categories.OrderBy(c => c.Id).ToListAsync();
            var categoriesDto = _mapper.Map<List<Category>,List<CategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<List<CategoryAdmin>> GetCategoriesAdminAsync()
        {
            var categories= await _context.Categories.OrderBy(c=>c.Id).ToListAsync();
            var categoriesAdmin = _mapper.Map<List<Category>,List<CategoryAdmin>>(categories);
            return categoriesAdmin;
        }

        public async Task<CategoryAdmin> GetCategoryByIdAdminAsync(int Id) 
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            var categoryAdmin = _mapper.Map<Category, CategoryAdmin>(category);
            return categoryAdmin;   
        }

        public async Task<Category> PostCategoryAsync(CategoryAdmin categoryAdmin)
        {
            try
            {
                categoryAdmin.Created_at = DateTime.Now;
                categoryAdmin.Created_by = "Admin";

                var category = _mapper.Map<CategoryAdmin,Category>(categoryAdmin);
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return category;
            }
            catch (DbException e)
            {
                string ErrorString = e.Message;
                throw;
            }
        }

        public async Task<Boolean> PutCategoryAsync(CategoryAdmin categoryAdmin)
        {
            try
            {
                categoryAdmin.Updated_at = DateTime.Now;
                categoryAdmin.Updated_by = "Admin";

                var category = _mapper.Map<CategoryAdmin, Category>(categoryAdmin);
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                return false;
            }
        }

        public async Task<Boolean> DeletedCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                //_context.Products.RemoveRange(_context.Products.Include(p => p.CategoryProduct.Where(cp => cp.CategoryId == id)));
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            catch (DbException)
            {
                return false;
            }
            

        }
    }
}
