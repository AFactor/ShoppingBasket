using System.Collections.Generic;
using Basket.Entities;

namespace Basket.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey identifier);
        List<TEntity> GetAll();
        
    }
    public interface IPromotionRepository : IRepository<Promotion, int>
    {

        //IEnumerable<Promotion> GetPromosByType(PromotionType type);
    }
    public interface IPriceRepository : IRepository<Price, string>
    {
        //Optional repository methods here. Scaffholding for future expansion
        //an example
        //IEnumerable<Price> GetPriceGreaterThan();

    }
}