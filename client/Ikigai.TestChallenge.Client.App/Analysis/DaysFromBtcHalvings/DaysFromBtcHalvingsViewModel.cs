using GalaSoft.MvvmLight.Command;
using Ikigai.TestChallenge.Client.App.Common;
using Ikigai.TestChallenge.Client.App.ServerApi;
using System;
using System.Threading.Tasks;

namespace Ikigai.TestChallenge.Client.App.Analysis.DaysFromBtcHalvings
{
    public class DaysFromBtcHalvingsViewModel : AnalysisViewModelBase
    {
        private string _input;
        public string Input
        {
            get => _input;
            set => Set(ref _input, value);
        }

        public RelayCommand ExecuteCommand { get; }

        public DaysFromBtcHalvingsViewModel()
        {
            ExecuteCommand = new RelayCommand(async () => await ExecuteAsync());
        }

        public async Task InitializeAsync()
        {
            DataModel = await TestChallengeApiConnector.GetDaysFromBtcHalvingsAnalysisModel();
        }


        private Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
