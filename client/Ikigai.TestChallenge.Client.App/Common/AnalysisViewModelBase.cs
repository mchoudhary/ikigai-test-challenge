using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Data;

namespace Ikigai.TestChallenge.Client.App.Common
{
    public abstract class AnalysisViewModelBase: ViewModelBase
    {
        public abstract string Id { get; }
        public IAnalysisDataModel DataModel { get; set; }
        public RelayCommand<string> ExportChartDataCommand { get; }

        protected AnalysisViewModelBase()
        {
            ExportChartDataCommand = new RelayCommand<string>(ExportChartDataAsync);
        }

        private void ExportChartDataAsync(string exportType)
        {
            Dictionary<string, List<dynamic>> chartData = DataModel.GetChartDataForExcel();
            DataSet xlSheetsData = Utilities.ConvertToDataTableSet(chartData);
            Utilities.WriteDataToFile(Id, xlSheetsData, exportType);
        }

    }
}
