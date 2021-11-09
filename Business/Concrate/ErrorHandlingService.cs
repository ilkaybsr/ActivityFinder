using Business.Abstracts;
using DataAccess.Abstracts;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly IGenericRepository<Error> _errorRepository;
        public ErrorHandlingService(IGenericRepository<Error> errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public async Task SaveErrorToDB(string errorMessage)
        {
            var newError = Error.Create(errorMessage);

            await _errorRepository.AddAsync(newError);

            await _errorRepository.SaveChangesAsync();
        }
    }
}
