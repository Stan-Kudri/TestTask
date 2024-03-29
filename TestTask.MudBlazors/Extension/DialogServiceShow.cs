﻿using MudBlazor;
using TestTask.MudBlazors.Dialog;

namespace TestTask.MudBlazors.Extension
{
    public static class DialogServiceShow
    {
        public static async Task<bool> DialogYesNoShow(this IDialogService dialogService, string title, string content)
        {
            var parameters = new DialogParameters<DialogYesNo> { { x => x.Content, content } };
            return await DialogShowAsync(parameters, dialogService, title, content);
        }

        private static async Task<bool> DialogShowAsync(DialogParameters<DialogYesNo> dialogParameters, IDialogService dialogService, string title, string content)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await dialogService.ShowAsync<DialogYesNo>(title, dialogParameters, options);
            var result = await dialog.Result;
            return result == null || !result.Cancelled;
        }
    }
}
