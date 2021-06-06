using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ikigai.TestChallenge.Client.App.ServerApi.Entites;
using Ikigai.TestChallenge.Client.App.ServerApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ikigai.TestChallenge.Client.App.Common;

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
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            DaysFromBtcHalvingsAnalysisModel model = await TestChallengeApiConnector.GetDaysFromBtcHalvingsAnalysisModel();
        }


        private Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
