using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IErrorHandlingService
    {
        Task SaveErrorToDB(string errorMessage);
    }
}
