using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Model;
using WhatToDo.Services;

namespace WhatToDo.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private TaskService _taskService;
        private bool _isLoading = false;

        public ObservableCollection<Tasks> TasksCollection { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string taskName;

        [ObservableProperty]
        string taskDescription;

        [ObservableProperty]
        bool isCompleted;

        [ObservableProperty]
        int taskNumber;

        public MainPageViewModel(TaskService taskService) 
        {
            _taskService = taskService;
            WeakReferenceMessenger.Default.Register<ItemAddSuccessfully>(this, HandleItemAdd);
            
            Task.Run(() => this.GetItems()).Wait();
        }


        [RelayCommand]
        public async Task NavigateAdd()
        {
            await Shell.Current.GoToAsync(nameof(AddTaskPage));
        }

        [RelayCommand]
        async Task GetItems()
        {
            if (_isLoading) return;

            try
            {
                _isLoading = true;

                if (TasksCollection.Any())
                {
                    TasksCollection.Clear();
                }

                var tasks = await _taskService.GetItems();
                foreach (Tasks task in tasks)
                {
                    TasksCollection.Add(task);
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Nem sikerült lekérni az adatokat: {ex}", "OK");
            }
            finally
            {
                IsRefreshing = false;
                _isLoading = false;
                TaskNumber = TasksCollection.Count;
            }

        }


        [RelayCommand]
        async Task DeleteItem(Tasks task)
        {
            await _taskService.DeleteItem(task);
            await GetItems();
        }

        private async void HandleItemAdd(object recipent, ItemAddSuccessfully message)
        {
            await GetItems();
        }

        [RelayCommand]
        async void ChangeTaskComplete(Tasks task)
        {
            task.IsCompleted = true;
            await _taskService.UpdateItem(task);
            await GetItems();
        }
    }
}
