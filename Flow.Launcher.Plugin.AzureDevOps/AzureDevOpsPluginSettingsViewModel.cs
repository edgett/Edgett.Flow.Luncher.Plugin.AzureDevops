using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin.AzureDevOps
{
    internal class AzureDevOpsPluginSettingsViewModel : INotifyPropertyChanged
    {
        private string? devOpsPat;
        private bool enableControls = true;
        private bool? isAuthenticated = null;
        private string? devOpsUrl;
        private string? message;

        public string? DevOpsUrl { get => devOpsUrl; set { devOpsUrl = value; OnPropertyChanged(); } }
        public string? DevOpsPat { get => devOpsPat; set { devOpsPat = value; OnPropertyChanged(); } }
        public bool EnableControls { get => enableControls; set { enableControls = value; OnPropertyChanged(); } }
        public bool? IsAuthenticated { get => isAuthenticated; set { isAuthenticated = value; OnPropertyChanged(); } }
        public string? Message { get => message; set { message = value; OnPropertyChanged(); } }
        public string CheckConfigButtonText
        {
            get
            {
                if (EnableControls)
                {
                    return "Check Configuration";
                }
                else
                {
                    return "Checking Configuration...";
                }
            }
        }



        public async Task CheckPat()
        {
            EnableControls = false;
          

            Uri? devOpsUri = null;
            if (!Uri.TryCreate(DevOpsUrl, new UriCreationOptions(), out devOpsUri) || devOpsUri == null)
            {
                Message = "Invalid Azure DevOps Url.";
                IsAuthenticated = false;
                EnableControls = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(DevOpsPat))
            {
                Message = "Invalid Azure DevOps Personal Access Token.";
                IsAuthenticated = false;
                EnableControls = true;
                return;
            }
            

            try
            {
                var creds = new VssBasicCredential(string.Empty, DevOpsPat);
                VssConnection vssConnection = new VssConnection(devOpsUri, creds);
                await vssConnection.ConnectAsync();

                IsAuthenticated = true;
                Message = "Successfully connected to Azure DevOps.";
            }
            catch (Exception ex)
            {
                IsAuthenticated = false;
                Message = ex.ToString();
            }

            EnableControls = true;

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

     

        
    }
}
