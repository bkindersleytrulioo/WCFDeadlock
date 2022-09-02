using System.Threading.Tasks;

namespace Shared.Ports
{
    [System.ServiceModel.ServiceContract]
    [CoreWCF.ServiceContract]
    public interface IMyContract
    {
        [System.ServiceModel.OperationContract(Action = "http://tempuri.org/IMyContract/Operation1", ReplyAction = "http://tempuri.org/IVerification/IMyContract/Operation1")]
        [CoreWCF.OperationContract(Action = "http://tempuri.org/IMyContract/Operation1", ReplyAction = "http://tempuri.org/IVerification/IMyContract/Operation1")]
        Task<string> Operation1Async(string name);

        [System.ServiceModel.OperationContract(Action = "http://tempuri.org/IMyContract/Operation2", ReplyAction = "http://tempuri.org/IVerification/IMyContract/Operation2")]
        [CoreWCF.OperationContract(Action = "http://tempuri.org/IMyContract/Operation2", ReplyAction = "http://tempuri.org/IVerification/IMyContract/Operation2")]
        string Operation2(string name);

    }
}
