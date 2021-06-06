using System.Threading.Tasks;

namespace Ikigai.TestChallenge.Client.App.Common
{
    public interface IAnalysisDataModelBase
    {
        public string GetChartDataForJson();
        public string GetChartDataForExcel();
    }
}
