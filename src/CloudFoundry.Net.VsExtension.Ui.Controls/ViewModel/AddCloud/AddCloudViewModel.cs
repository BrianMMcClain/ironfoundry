﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudFoundry.Net.VsExtension.Ui.Controls.Mvvm;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CloudFoundry.Net.VsExtension.Ui.Controls.Utilities;
using GalaSoft.MvvmLight.Messaging;
using CloudFoundry.Net.VsExtension.Ui.Controls.Model;
using System.Collections.ObjectModel;
using CloudFoundry.Net.Types;

namespace CloudFoundry.Net.VsExtension.Ui.Controls.ViewModel
{
    [ExportViewModel("AddCloud", false)]
    public class AddCloudViewModel : ViewModelBase
    {
        public RelayCommand ConfirmedCommand { get; private set; }
        public RelayCommand CancelledCommand { get; private set; }
        public RelayCommand ManageCloudUrlsCommand { get; private set; }
        public RelayCommand ValidateAccountCommand { get; private set; }
        public RelayCommand RegisterAccountCommand { get; private set; }

        public Cloud Cloud { get; private set; }  
        private ObservableCollection<CloudUrl> cloudUrls;

        public AddCloudViewModel()
        {
            this.Cloud = new Cloud();            
            ValidateAccountCommand = new RelayCommand(ValidateAccount, CanValidate);
            RegisterAccountCommand = new RelayCommand(RegisterAccount, CanRegister);
            ManageCloudUrlsCommand = new RelayCommand(ManageCloudUrls);
            ConfirmedCommand = new RelayCommand(Confirmed);
            CancelledCommand = new RelayCommand(Cancelled);

            InitializeData();
            RegisterGetData();
        }

        private void InitializeData()
        {
            Messenger.Default.Send(new NotificationMessageAction<ObservableCollection<CloudUrl>>(Messages.SetAddCloudData,
                (cloudUrls) =>
                {
                    this.CloudUrls = cloudUrls;
                    this.SelectedCloudUrl = cloudUrls.SingleOrDefault((i) => i.IsDefault);
                }));
        }

        private void RegisterGetData()
        {
            Messenger.Default.Register<NotificationMessageAction<AddCloudViewModel>>(this,
                message =>
                {
                    if (message.Notification.Equals(Messages.GetAddCloudData))
                    {
                        message.Execute(this);
                        Messenger.Default.Unregister(this);
                    }
                });
        }

        private void Confirmed()
        {           
            Messenger.Default.Send(new NotificationMessage<bool>(this, true, Messages.AddCloudDialogResult));
        }

        private void Cancelled()
        {
            Messenger.Default.Send(new NotificationMessage<bool>(this, false, Messages.AddCloudDialogResult));
        }

        private void ValidateAccount()
        {

        }

        private void RegisterAccount()
        {
            Messenger.Default.Register<NotificationMessageAction<Cloud>>(this,
                message =>
                {
                    if (message.Notification.Equals(Messages.SetRegisterAccountData))
                        message.Execute(this.Cloud);
                });

            Messenger.Default.Send(new NotificationMessageAction<bool>(Messages.RegisterAccount,
                (confirmed) =>
                {
                    if (confirmed)
                    {
                        Messenger.Default.Send(new NotificationMessageAction<RegisterAccountViewModel>(Messages.GetRegisterAccountData,
                            (viewModel) =>
                            {
                                this.EMail = viewModel.EMail;
                                this.Password = viewModel.NewPassword;
                            }));
                    }
                }));
        }

        private bool CanValidate()
        {
            return !String.IsNullOrEmpty(this.EMail) && !String.IsNullOrEmpty(this.Password);
        }

        private bool CanRegister()
        {
            return SelectedCloudUrl.ServerType.Equals("Local cloud",StringComparison.InvariantCultureIgnoreCase);
        }

        private void ManageCloudUrls()
        {
            Messenger.Default.Register<NotificationMessageAction<ObservableCollection<CloudUrl>>>(this,
                message =>
                {
                    if (message.Notification.Equals(Messages.SetManageCloudUrlsData))
                        message.Execute(this.CloudUrls);
                });

            Messenger.Default.Send(new NotificationMessageAction<bool>(Messages.ManageCloudUrls,
                (confirmed) =>
                {
                    if (confirmed)
                    {
                        Messenger.Default.Send(new NotificationMessageAction<ManageCloudUrlsViewModel>(Messages.GetManageCloudUrlsData,
                            (viewModel) =>
                            {
                                this.CloudUrls.Synchronize(viewModel.CloudUrls, new CloudUrlEqualityComparer());
                                Messenger.Default.Send<NotificationMessage<ObservableCollection<CloudUrl>>>(new NotificationMessage<ObservableCollection<CloudUrl>>(this.CloudUrls,Messages.SaveCloudUrls));                             
                            }));
                    }
                }));
        }

        private CloudUrl selectedCloudUrl = new CloudUrl();
        public CloudUrl SelectedCloudUrl
        {
            get { return this.selectedCloudUrl; }
            set
            {
                this.selectedCloudUrl = value;
                if (this.selectedCloudUrl != null)
                    this.Cloud.Url = selectedCloudUrl.Url;
                else
                    this.Cloud.Url = string.Empty;
                RaisePropertyChanged("SelectedCloudUrl");
            }
        }

        public ObservableCollection<CloudUrl> CloudUrls
        {
            get { return this.cloudUrls; }
            set
            {
                this.cloudUrls = value;
                RaisePropertyChanged("CloudUrls");
            }
        }

        public string ServerName
        {
            get { return this.Cloud.ServerName; }
            set
            {
                this.Cloud.ServerName = value;
                RaisePropertyChanged("ServerName");
            }
        }

        public string HostName
        {
            get { return this.Cloud.HostName; }
            set
            {
                this.Cloud.HostName = value;
                RaisePropertyChanged("HostName");
            }
        }

        public string EMail
        {
            get { return this.Cloud.Email; }
            set
            {
                this.Cloud.Email = value;
                RaisePropertyChanged("EMail");
            }
        }

        public string Password
        {
            get { return this.Cloud.Password; }
            set
            {
                this.Cloud.Password = value;
                RaisePropertyChanged("Password");
            }
        }
    }
}
