using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToDo.ViewModel
{
    public class ItemAddSuccessfully : ValueChangedMessage<bool>
    {
        public ItemAddSuccessfully(bool value) : base(value) { }
    }
}
