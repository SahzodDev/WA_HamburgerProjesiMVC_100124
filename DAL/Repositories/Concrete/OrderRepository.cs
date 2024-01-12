﻿using DAL.Context;
using DAL.Repositories.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext dbContext;
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Order> GetAllByUserId(string id)
        {
            return dbContext.Orders.Where(x => x.AppUser.Id == id);
        }

        public IEnumerable<Order> GetAllOrderByCreatedDate()
        {
            return dbContext.Orders.OrderBy(o => o.CreatedDate);
        }

        public Order GetByIdIncludeMenus(int id)
        {
           return dbContext.Orders.Include(x => x.Menus).FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Order> GetAllIncludeMenusIncludeUsers()
        {
            return dbContext.Orders.Include(x => x.Menus).Include(x => x.AppUser);
        }

        public Order GetByUserIdIncludeMenus(string id)
        {
            return dbContext.Orders.Include(x => x.Menus).FirstOrDefault(x => x.AppUser.Id == id);
        }

        public IEnumerable<Order> GetByUserIdIncludeMenusOrderByCreatedDate(string id)
        {
            return dbContext.Orders.Include(x => x.Menus).Where(x => x.AppUser.Id == id).OrderBy(o => o.CreatedDate);
        }

        public IEnumerable<Order> GetByUserIdOrderByCreatedDate(string id)
        {
            return dbContext.Orders.Where(x => x.AppUser.Id == id).OrderBy(o => o.CreatedDate);
        }

        public decimal GetTotalPaymentFromProducts()
        {
            var total = dbContext.Orders.SelectMany(o => o.Menus.SelectMany(x => x.Products)).Sum(p => p.Price);

            return (decimal)total;
        }

        public int GetTotalOrderCount()
        {
            return dbContext.Orders.Count();
        }

        public decimal GetTotalPayment()
        {
            var total = dbContext.Orders.SelectMany(o => o.Menus).Sum(x => x.Price);

            return (decimal)total;
        }
    }
}
