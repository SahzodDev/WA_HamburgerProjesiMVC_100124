﻿using DAL.Context;
using DAL.Repositories.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Concrete
{
    public class MenuRepository : GenericRepository<Menu>,IMenuRepository
    {
        private readonly AppDbContext dbContext;

        public MenuRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Menu> GetAllIncludeOrder()
        {
            return dbContext.Menu.Include(o=>o.Order);
        }

        public IEnumerable<Menu> GetAllIncludeProducts()
        {
            return dbContext.Menu.Include(p=>p.Products);   
        }

        public IEnumerable<Menu> GetAllIncludeProductsAndOrder()
        {
            return dbContext.Menu.Include(p=>p.Products).Include(o=>o.Order);
        }

        public IEnumerable<Menu> GetAllIncludeProductsAndOrderThenCustomer()
        {
            return dbContext.Menu.Include(p => p.Products).Include(o => o.Order).ThenInclude(c => c.AppUser);
        }

        public Menu GetByIdIncludeOrder(int id)
        {
            return dbContext.Menu.Include(o => o.Order).FirstOrDefault(m => m.Id == id);
        }

        public Menu GetByIdIncludeProducts(int id)
        {
            return dbContext.Menu.Include(o => o.Products).FirstOrDefault(m => m.Id == id);
        }

        public Menu GetByIdIncludeProductsAndOrder(int id)
        {
            return dbContext.Menu.Include(o => o.Order).Include(o => o.Products).FirstOrDefault(m => m.Id == id);
        }

        public Menu GetByIdIncludeProductsAndOrderThenCustomer(int id)
        {
            return dbContext.Menu.Include(p => p.Products).Include(o => o.Order).ThenInclude(o => o.AppUser).FirstOrDefault(m=>m.Id==id);
        }
    }
}
