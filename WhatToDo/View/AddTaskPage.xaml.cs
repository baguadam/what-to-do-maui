using WhatToDo.ViewModel;

namespace WhatToDo;

public partial class AddTaskPage : ContentPage
{
	public AddTaskPage(AddTaskViewModel _viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel;
	}
}