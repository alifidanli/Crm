using Crm.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public UserDto user { get; set; }
    }
}