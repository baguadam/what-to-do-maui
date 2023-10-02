using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Model;
using WhatToDo.Services;

namespace WhatToDo.ViewModel
{
    public partial class AddTaskViewModel : ObservableObject
    {

        private TaskService _taskService;

        [ObservableProperty]
        string taskName;

        [ObservableProperty]
        string taskDescription;

        public AddTaskViewModel (TaskService taskService)
        {
            _taskService = taskService;
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task AddTask()
        {
            if (TaskName is null || TaskName.Length == 0)
            {
                await Shell.Current.DisplayAlert("Figyelmeztetés", "Kérem, adjon nevet a rögzítendő feladatnak", "OK");
                return;
            }

            Tasks _task = new Tasks();
            _task.TaskName = TaskName;
            _task.TaskDescription = TaskDescription;
            _task.IsCompleted = false;

            await _taskService.AddItem(_task);
            TaskName = "";
            TaskDescription = "";
            WeakReferenceMessenger.Default.Send(new ItemAddSuccessfully(true));

            await Shell.Current.GoToAsync("..");
        }
    }
}
