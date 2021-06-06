using Ikigai.TestChallenge.Client.App.Analysis.DaysFromBtcHalvings;
using System.Windows;

namespace Ikigai.TestChallenge.Client.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DaysFromBtcHalvingsViewModel vm = new DaysFromBtcHalvingsViewModel();
        }
    }
}
