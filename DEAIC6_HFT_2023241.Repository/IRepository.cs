using System.Linq;

namespace DEAIC6_HFT_2023241.Repository
{
    internal interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        void Create(T element);
        void Update(T element);
        void Delete(int id);
    }
}