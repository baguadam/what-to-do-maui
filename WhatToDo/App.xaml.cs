﻿using WhatToDo.ViewModel;

namespace WhatToDo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}
