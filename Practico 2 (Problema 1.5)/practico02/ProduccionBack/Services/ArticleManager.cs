using ProduccionBack.Domain;
using ProduccionBack.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Services
{
    public class ArticleManager
    {
        ArticleRepository aRepo;
        public ArticleManager()
        {
            aRepo = new ArticleRepository();
        }
        public List<Article> GetAll()
        {
            return aRepo.GetAll();
        }
        public Article GetArticleById(int id)
        {
            return aRepo.GetArticleById(id);
        }
        public bool SaveArticle(Article article)
        {
            return aRepo.Save(article);
        }
        public bool UpdateArticle(Article article)
        {
            return aRepo.Update(article);
        }
        public bool DeleteArticle(int id)
        {
            return aRepo.Delete(id);
        }
    }
}
