using System.Linq;

namespace DEAIC6_HFT_2023241.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        void Create(T element);
        void Update(T Element);
        void Delete(int id);
    }
}