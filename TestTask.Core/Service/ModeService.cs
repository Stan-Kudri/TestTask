﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestTask.Core.Components;
using TestTask.Core.Db;

namespace TestTask.Core.Service
{
    public class ModeService
    {
        private readonly AppDbContext _dbContext;

        public ModeService(AppDbContext appDbContext) => _dbContext = appDbContext;

        public void Add(Mode mode)
        {
            if (mode == null)
            {
                throw new ArgumentException("The received parameters are not correct.", nameof(mode));
            }

            if (_dbContext.Modes.Any(e => e.Id == mode.Id))
            {
                throw new ArgumentException("This mode exists.");
            }

            _dbContext.Modes.Add(mode);
            _dbContext.SaveChanges();
        }

        public void Update(Mode mode)
        {
            if (mode == null)
            {
                throw new ArgumentNullException("The format of the transmitted data is incorrect.", nameof(mode));
            }

            var item = _dbContext.Modes.FirstOrDefault(e => e.Id == mode.Id) ?? throw new InvalidOperationException("Interaction element not found.");

            item.Name = mode.Name;
            item.MaxBottleNumber = mode.MaxBottleNumber;
            item.MaxUsedTips = mode.MaxUsedTips;

            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var mode = _dbContext.Modes.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException("Interaction element not found.");
            _dbContext.Modes.Remove(mode);
            _dbContext.SaveChanges();
        }

        public void AddImportData(Mode mode)
        {
            var duplicateId = _dbContext.Modes.FirstOrDefault(e => e.Id == mode.Id);
            if (duplicateId == null)
            {
                Add(mode);
            }

            Update(mode);
        }

        public bool IsIDExists(int id) => _dbContext.Modes.FirstOrDefault(e => e.Id == id) != null;

        public List<Mode> GetAllMode() => _dbContext.Modes.Count() > 0 ? _dbContext.Modes.ToList() : null;

        public IQueryable<Mode> GetModes() => _dbContext.Modes;
    }
}
