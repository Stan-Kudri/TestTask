﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestTask.Core.Models.Companies;
using TestTask.MudBlazors.Extension;
using TestTask.MudBlazors.Model;
using TestTask.MudBlazors.Model.TableComponent;

namespace TestTask.MudBlazors.Pages.Table.Companies
{
    public partial class CompanyItemPage
    {
        [Inject] private CompanyService CompanyService { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;

        private CompanyModel companyModel { get; set; } = new CompanyModel();
        private string[] errors = { };
        private bool IsAddItem = true;

        private Company? oldCompany;

        [Parameter] public int? Id { get; set; } = null;

        protected override void OnInitialized()
        {
            if (Id == null)
            {
                IsAddItem = true;
                return;
            }

            if (Id <= 0)
            {
                NavigationInCompanyTable();
            }

            IsAddItem = false;
            oldCompany = CompanyService.GetCompany((int)Id);
            companyModel = oldCompany.GetCompanyModel();
        }

        private void Close() => NavigationInCompanyTable();

        //Methods for add item company
        private async Task Add()
        {
            if (errors.Length != 0)
            {
                return;
            }

            if (!ValidateFields(out var message))
            {
                ShowMessageWarning(message);
                return;
            }

            if (!CompanyService.IsFreeName(companyModel.Name))
            {
                ShowMessageWarning("Name is not free.");
                return;
            }

            var company = companyModel.GetCompany();
            CompanyService.Add(company);
            NavigationInCompanyTable();
        }

        private void ClearData() => companyModel.ClearData();

        //Methods for edit item company
        private async Task Updata()
        {
            if (errors.Length != 0)
            {
                return;
            }

            if (!ValidateFields(out var message))
            {
                ShowMessageWarning(message);
                return;
            }

            var company = companyModel.GetModifyCompany(oldCompany.Id);

            if (!oldCompany.Equals(company))
            {
                CompanyService.Updata(company);
            }

            NavigationInCompanyTable();
        }

        private void RecoverPastData() => companyModel = oldCompany.GetCompanyModel();

        private IEnumerable<string> ValidFormatText(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                yield return "Field is required.";
            }
        }

        private IEnumerable<string> ValidFormatDate(DateTime date)
        {
            if (date == null)
            {
                yield return "Date is required.";
                yield break;
            }

            if (date > DateTime.Now)
            {
                yield return "The creation date is not within the valid range.";
            }
        }

        private void NavigationInCompanyTable() => Navigation.NavigateTo($"/table/{TabTable.Company.ActiveTabIndex}");

        private async Task ShowMessageWarning(string message)
            => await DialogService.ShowMessageBox("Warning", message, yesText: "Ok");

        private bool ValidateFields(out string message)
        {
            message = string.Empty;

            if (companyModel.Name == null || companyModel.Name == string.Empty)
            {
                message = "Name is required.";
                return false;
            }

            if (companyModel.DateCreation == null)
            {
                message = "The company creation date has not been selected.";
                return false;
            }

            return true;
        }
    }
}
