using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POSUNO.Api.Data.Entities;

namespace POSUNO.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckUserAsync();
            await CheckCustomersAsync();
            await CheckProductAsync();

        }

        private async Task CheckUserAsync()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User { Email = "camilo@gmail.com", FirstName = "Camilo", LastName = "Carvajal", Password = "123456" });
                _context.Users.Add(new User { Email = "cristian@gmail.com", FirstName = "Cristian", LastName = "Carvajal", Password = "123456" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductAsync()
        {
            if (!_context.Products.Any())
            {
                Random random = new Random();
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i <= 200; i++)
                {
                    _context.Products.Add(new Product { Name = $"Producto {i}", Description = $"Este es el producto {i}", Price = random.Next(5, 1000), Stock = random.Next(0, 500), IsActive = true, User = user });
                    await _context.SaveChangesAsync();

                }
            }
        }

        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i <= 200; i++)
                {
                    _context.Customers.Add(new Customer { FirstName = $"Cliente {i}", LastName = $"Apellido {i}", PhoneNumber = "3122210110", Email = $"cliente{ i }@yopmail.com", Address = "calle Luna", IsActive = true, User = user });
                    await _context.SaveChangesAsync();
                }
            }
        }


    }
}
