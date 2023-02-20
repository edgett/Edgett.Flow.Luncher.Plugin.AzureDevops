using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flow.Launcher.Plugin.AzureDevOps
{
    /// <summary>
    /// Interaction logic for AzureDevOpsPluginSettings.xaml
    /// </summary>
    public partial class AzureDevOpsPluginSettings : UserControl
    {
        private AzureDevOpsPluginSettingsViewModel ViewModel { get; set; }

     

        public AzureDevOpsPluginSettings()
        {
      
            InitializeComponent();
            ViewModel = (AzureDevOpsPluginSettingsViewModel)dpSettings.DataContext;
    
        }

        private async void btnCheckToken_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel?.CheckPat();
        }


    }
}
