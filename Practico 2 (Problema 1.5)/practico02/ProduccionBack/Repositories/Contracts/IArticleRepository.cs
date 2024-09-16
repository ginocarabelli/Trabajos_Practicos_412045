using ProduccionBack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Repositories.Contracts
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        Article GetArticleById(int id);
        bool Save(Article oArticle);
        bool Delete(int id);
        bool Update(Article oArticle);
        bool Validate(int id);
    }
}
