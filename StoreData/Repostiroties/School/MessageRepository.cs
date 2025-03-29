using Microsoft.EntityFrameworkCore;
using StoreData.Models;

namespace StoreData.Repostiroties.School;

public class MessageRepository : BaseSchoolRepository<MessageData>
{
    public MessageRepository(SchoolDbContext dbContext) : base(dbContext) { }

    public List<MessageData> GetAllWithUser()
    {
        return _dbSet
            .Include(m => m.User)
            .ToList();
    }
    
}