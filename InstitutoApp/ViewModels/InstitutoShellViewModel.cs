﻿using InstitutoApp.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstitutoApp.ViewModels
{
    public class InstitutoShellViewModel : NotificationObject
    {
        private bool userIsLogout = true;

		public bool UserIsLogout
		{
			get { return userIsLogout; }
			set { userIsLogout = value;
                OnPropertyChanged();
            }
		}

		public Command LogoutCommand { get;  }

        public InstitutoShellViewModel()
		{
            LogoutCommand = new Command(OnLogout);
        }

        private void OnLogout(object obj)
        {
            UserIsLogout = true;
            var institutoShell = (InstitutoShell)App.Current.MainPage;
            institutoShell.DisableAppAfterLogin();
        }
    }
}
