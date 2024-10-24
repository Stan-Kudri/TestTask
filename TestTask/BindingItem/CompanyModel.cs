﻿using System;
using TestTask.Core.Models.Companies;

namespace TestTask.BindingItem
{
    public class CompanyModel : ModelBase
    {
        private string _name = string.Empty;
        private DateTime _dateCreation = DateTime.MinValue;
        private string _country = string.Empty;

        public CompanyModel(string name, DateTime dateCreation, string country)
        {
            _name = name;
            _country = country;
            _dateCreation = dateCreation == null ? throw new ArgumentException("The company creation date cannot be zero.", nameof(dateCreation)) : _dateCreation = dateCreation;
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public DateTime DateCreation
        {
            get => _dateCreation;
            set => SetField(ref _dateCreation, value);
        }

        public string Country
        {
            get => _country;
            set => SetField(ref _country, value);
        }

        public Company ToCompany() => new Company(_name, _dateCreation, _country);

        public Company ToCompany(int id) => new Company(_name, _dateCreation, _country, id);
    }
}
