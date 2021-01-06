using Blazor.Data;
using BlazorDemo.Sevices;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Components
{
    public partial class AddUserDialog
    {

        public User User { get; set; } = new User
        {
        };
        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        public long UserId { get; set; }

        public bool ShowDialog { get; set; }
        public bool Saved;
        public string StatusClass = string.Empty;
        public string Message = string.Empty;

        public void Show(User user)
        {
            if (user != null)
                UserId = user.Id;
            ResetDialog(user);
            ShowDialog = true;
           StateHasChanged();
        }
        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
        private void ResetDialog(User user)
        {
            if (user != null)
            {
                User = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                };
            }
            else
            {
                User = new User {
                };
            }
        }
    
        [Parameter]
        public EventCallback<bool> CloseEventCallback
        {
            get;
            set;
        }
        protected async Task HandleValidSubmit()
        {
            Saved = false;
            var savedUser = new User();
            savedUser = await UserDataService.AddUser(User);
            if (savedUser != null && UserId==0)
            {
                StatusClass = "alert-success";
                Message = "New User Added Successfuly";
                Saved = true;
            }
            else if(savedUser != null && UserId !=0)
            {
                StatusClass = "alert-success";
                Message = "User Updated Successfuly";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "somthing went wrong adding the new user .please try again";
                Saved = false;
            }
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
            ToastService.ShowSuccess(Message,"Confirm");
        }
    }
}
