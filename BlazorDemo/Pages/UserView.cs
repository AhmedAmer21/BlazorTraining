using Blazor.Data;
using BlazorDemo.Components;
using BlazorDemo.Sevices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.JSInterop;

namespace BlazorDemo.Pages
{
    public partial class UserView
    {
        public IEnumerable<User> Users { get; set; }
        public User User { get; set; }

        protected AddUserDialog AddUserDialog { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Users = (await UserDataService.GetAllUsers()).ToList();
            User = Users.FirstOrDefault();
        }
        protected void QuickAddUser(User user)
        {
             AddUserDialog.Show(user);
        }
        public async void AddUserDialog_OnDialogClose()
        {
            Users = (await UserDataService.GetAllUsers()).ToList();
            StateHasChanged();
        }
        public async void DeleteUser(long id)
        {
            if (!await JSRuntime.InvokeAsync<bool>("confirm","Are you sure you want to delete this user"))
                return;
            await UserDataService.DeleteUser(id);
            Users = (await UserDataService.GetAllUsers()).ToList();
            StateHasChanged();
            ToastService.ShowSuccess("User Deleted Successfuly", "Confirm");
        }
    }
}
