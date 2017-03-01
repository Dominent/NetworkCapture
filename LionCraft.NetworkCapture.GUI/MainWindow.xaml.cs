using LionCraft.NetworkCapture.Core.Infrastructure.Constants;
using LionCraft.NetworkCapture.Core.Models.Helpers;
using LionCraft.NetworkCapture.Core.Models.Managers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LionCraft.NetworkCapture.GUI
{
    public partial class MainWindow : Window
    {
        private CaptureManager captureManager;
        private CancellationTokenSource tokenSource;

        public MainWindow()
        {
            InitializeComponent();

            this.tokenSource = new CancellationTokenSource();
            var endpoint = new IPEndPoint(IPAddressHelper.GetLocalIpAddress(), GeneralConstants.DefaultSocketPort);
            this.captureManager = new CaptureManager();

            this.ResizeMode = ResizeMode.NoResize;

            DataGrid.DataContext = captureManager;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => this.captureManager.Capture(null), this.tokenSource.Token);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.tokenSource.Cancel();
        }
    }
}
