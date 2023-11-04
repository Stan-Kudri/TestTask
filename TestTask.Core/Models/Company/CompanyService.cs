﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Core.Models.Company
{
    public class CompanyService : IService<Company>
    {
        private readonly AppDbContext _dbContext;

        public CompanyService(AppDbContext appDbContext) => _dbContext = appDbContext;

        public void Add(Company item)
        {
            if (item == null)
            {
                throw new ArgumentException("The received parameters are not correct.", nameof(item));
            }

            if (_dbContext.Company.Any(e => e.Id == item.Id))
            {
                throw new ArgumentException("This company exists.");
            }

            _dbContext.Company.Add(item);
            _dbContext.SaveChanges();
        }

        public void Update(Company item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("The format of the transmitted data is incorrect.", nameof(item));
            }

            var oldItem = _dbContext.Company.FirstOrDefault(e => e.Id == item.Id) ?? throw new InvalidOperationException("Interaction element not found.");

            oldItem.Name = item.Name;
            oldItem.DateCreation = item.DateCreation;
            oldItem.Country = item.Country;

            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var company = _dbContext.Company.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException("Interaction element not found.");
            _dbContext.Company.Remove(company);
            _dbContext.SaveChanges();
        }

        public void AddImportData(Company company)
        {
            var duplicateId = _dbContext.Company.FirstOrDefault(e => e.Id == company.Id);
            if (duplicateId == null)
            {
                Add(company);
            }

            Update(company);
        }

        public string NameMode(int id)
            => _dbContext.Company.FirstOrDefault(e => e.Id == id).Name ?? throw new ArgumentException("Interaction element not found.");

        public List<Company> GetAll() => _dbContext.Company.Count() > 0 ? _dbContext.Company.ToList() : null;

        public IQueryable<Company> GetQueryableAll() => _dbContext.Company.Select(e => e);
    }
}
