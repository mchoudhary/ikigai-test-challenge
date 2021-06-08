using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ikigai.TestChallenge.Client.App.Common
{
    public interface IAnalysisDataModel
    {
        public string GetChartDataForJson();
        public Dictionary<string, List<dynamic>> GetChartDataForExcel();
    }
}
