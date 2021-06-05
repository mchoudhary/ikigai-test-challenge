using Ikigai.TestChallenge.Client.App.DataService;
using System.Threading.Tasks;
using System.Windows;

namespace Ikigai.TestChallenge.Client.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TimeseriesApiConnector connector = new TimeseriesApiConnector();
            Task task = connector.GetHistoricalDailyOhlcvDataAsync("btcusd", "bitstamp");
        }
    }
}
