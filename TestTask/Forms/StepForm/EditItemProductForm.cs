﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestTask.BindingItem.UserBinding.ProductBinding;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;

namespace TestTask.Forms.StepForm
{
    public class EditItemProductForm : ProductFormBase
    {
        private Product _oldItem;
        private Product _editItem;

        public EditItemProductForm(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Text = "Edit Product";
        }

        public void Initialize(List<Company> company, Product oldItem)
        {
            _companies = new SelectCompany(company);
            _oldItem = oldItem;
        }

        protected override void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!IsDataFilled(out var message))
            {
                _messageBox.ShowInfo(message);
                return;
            }

            _editItem = GetStepModel().ToStep(_oldItem.Id);
            if (_oldItem.Equals(_editItem))
            {
                _messageBox.ShowInfo("The product has not been modified.");
                DialogResult = DialogResult.Cancel;
            }

            DialogResult = DialogResult.OK;
        }

        protected override void AddStepForm_Load(object sender, EventArgs e)
        {
            selectCompanyBindingSource.DataSource = _companies.Items;
            _companies.SetValueMode(_oldItem.CompanyId);
            SetDefaultValueData();
        }

        protected override void SetDefaultValueData()
        {
            cmbCompanyValue.SelectedItem = _companies.Company;
            tbCategory.Text = _oldItem.Category;
            tbType.Text = _oldItem.Type;
            tbPrice.Text = _oldItem.Price.ToString();
            tbDestination.Text = _oldItem.Destination;
        }

        public Product GetEditProduct() => _editItem;
    }
}
