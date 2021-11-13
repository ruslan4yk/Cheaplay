using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Models
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        void Create(TModel item);
        TModel GetById(int id);
        List<TModel> GetAll();
        void Remove(TModel item);
        void Update(TModel item);
    }
}
