using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;

namespace Ikigai.TestChallenge.Client.App.Common
{
    public abstract class AnalysisViewModelBase: ViewModelBase
    {
        public IAnalysisDataModelBase DataModel { get; set; }
        public RelayCommand<string> ExportChartDataCommand { get; }

        protected AnalysisViewModelBase()
        {
            ExportChartDataCommand = new RelayCommand<string>(ExportChartDataAsync);
        }

        private void ExportChartDataAsync(string exportType)
        {
            switch (exportType)
            {
                case "EXCEL":
                    break;
                case "JSON":
                    break;
                default:
                    break;
            }
        }
    }
}
