using WhatToDo.ViewModel;

namespace WhatToDo;

public partial class MainPage : ContentPage
{

	public MainPage(MainPageViewModel _mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = _mainPageViewModel;
	}

}

