using Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IActivityCollectorService
    {
        Task<List<ActivityDTO>> Collect(int limit);
        Task<List<ActivityDTO>> GetAllActivities(int pageIndex, int itemSize);

    }
}
