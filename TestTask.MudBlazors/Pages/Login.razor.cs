﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using TestTask.Core.Models.Users;
using TestTask.MudBlazors.Authenticate;
using TestTask.MudBlazors.Model;

namespace TestTask.MudBlazors.Pages
{
    public partial class Login
    {
        [Inject] private UserService UserService { get; set; } = null!;
        [Inject] private UserValidator UserValidator { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private BlazorAppLoginService BlazorAppLoginService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        [CascadingParameter] protected Task<AuthenticationState> AuthState { get; set; }

        private UserModel userModel { get; set; } = new UserModel();
        private string matchPassword = string.Empty;
        private string[] errors = { };

        [Parameter] public bool IsSignIn { get; set; } = false;

        private void SignInPage() => Navigation.NavigateTo($"/login/{true}");

        private void RegistrationPage() => Navigation.NavigateTo($"/login/{false}");

        private void ClearField() => userModel.Password = userModel.Username = matchPassword = string.Empty;

        private async Task SignIn()
        {
            var loginResult = await BlazorAppLoginService.LoginAsync(userModel.ToUser());

            if (!loginResult)
            {
                Snackbar.Add($"Account login failed.", Severity.Warning);
                return;
            }

            Snackbar.Add($"Sign in account : {userModel.Username}", Severity.Success);
            Navigation.NavigateTo("/table", true);
        }

        private void AddUser()
        {
            if (errors.Length != 0)
            {
                return;
            }

            var user = userModel.ToUser();

            UserService.Add(user);
            Snackbar.Add($"Account registered", Severity.Success);
            SignInPage();
        }

        private IEnumerable<string> ValidFormatPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                yield return "Password is required!";
                yield break;
            }

            if (!UserValidator.ValidFormatPassword(password, out var message))
            {
                yield return message;
                yield break;
            }
        }

        private IEnumerable<string> ValidFormatUsername(string username)
        {
            if (!IsSignIn && !UserService.IsFreeUsername(userModel.Username))
            {
                yield return string.Format($"{userModel.Username} username is taken.");
                yield break;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                yield return "Username is required!";
                yield break;
            }

            if (!UserValidator.ValidFormatUsername(username, out var message))
            {
                yield return message;
                yield break;
            }
        }

        private string? MatchPassword(string arg)
        {
            if (userModel.Password != arg)
            {
                return "Passwords don't match";
            }

            return null;
        }
    }
}
