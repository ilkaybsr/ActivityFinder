using System;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserActivityService
    {
        Task<bool> Bookmark(Guid userId, int activityId);
        Task<bool> Remove(int Id);
        Task<List<ActivityDTO>> List(Guid userId);
    }
}
