using StoreData.Models;

namespace StoreData.Repostiroties.School;

public interface IBaseSchoolRepository<TDbModel> where TDbModel : BaseModel
{
    public TDbModel Get(int id);
    public List<TDbModel> GetAll();
    public void Add(TDbModel item);
    public void Remove(int id);

    public void Update(TDbModel model);
}