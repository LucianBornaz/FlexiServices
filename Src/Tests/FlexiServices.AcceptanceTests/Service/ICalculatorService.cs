using System.ServiceModel;

namespace FlexiServices.AcceptanceTests.Service
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Sum(int x, int y);
    }
}
