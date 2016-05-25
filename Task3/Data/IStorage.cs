using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> LoadSession();
        T GetById(int id);
        void UpdateSession(T entity);
        void InsertSession(T entity);
        void DeleteSession(int id);
    }
}
