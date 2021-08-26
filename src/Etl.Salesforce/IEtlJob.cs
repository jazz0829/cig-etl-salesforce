using System.Threading.Tasks;

namespace Eol.Cig.Etl.Salesforce
{
    public interface IEtlJob
    {
        string GetName();
        Task Execute();
    }
}
