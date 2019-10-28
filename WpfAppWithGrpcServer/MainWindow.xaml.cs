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
using gRPCLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfAppWithGrpcServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Stop this some where...
        private IHost _host;

        public MainWindow()
        {
            _host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddStandaloneGrpcServer(o =>
                    {
                        // Change the port
                        o.ListenLocalhost(5007);
                    });
                })
                .Build();

            _host.Start();

            InitializeComponent();
        }
    }
}
