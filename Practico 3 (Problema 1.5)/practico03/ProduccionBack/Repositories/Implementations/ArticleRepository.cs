using ProduccionBack.Domain;
using ProduccionBack.Repositories.Contracts;
using ProduccionBack.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Repositories.Implementations
{
    public class ArticleRepository : IArticleRepository
    {
        public List<Article> GetAll()
        {
            List<Article> lst = new List<Article>();
            var helper = DataHelper.GetInstance();
            DataTable table = helper.ExecuteSPQuery("SP_GetAllArticles");
            foreach (DataRow dr in table.Rows)
            {
                Article oArticle = new Article()
                {
                    ArticleID = Convert.ToInt32(dr["ARTICLE_ID"]),
                    ArticleName = dr["ARTICLE_NAME"].ToString(),
                    UnitPrice = Convert.ToDouble(dr["UNIT_PRICE"])
                };
                lst.Add(oArticle);
            }
            return lst;
        }
        public Article GetArticleById(int id)
        {
            Article oArticle = new Article();
            var helper = DataHelper.GetInstance();
            DataTable table = helper.ExecuteSPQuery("SP_GetArticleById", new SqlParameter("@ID", id));
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                int articleId = Convert.ToInt32(row["ARTICLE_ID"]);
                string name = Convert.ToString(row["ARTICLE_NAME"]);
                double unitPrice = Convert.ToDouble(row["UNIT_PRICE"]);

                oArticle.ArticleID = articleId;
                oArticle.ArticleName = name;
                oArticle.UnitPrice = unitPrice;
            }
            return oArticle;
        }
        public bool Validate(int id)
        {
            var helper = DataHelper.GetInstance();
            DataTable table = helper.ExecuteSPQuery("SP_GetArticleById", new SqlParameter("@ID", id));
            if (table.Rows.Count > 0)
            {
                Console.WriteLine("Este artículo ya existe, ingrese otro");
                return true;
            }
            return false;
        }
        public bool Save(Article oArticle)
        {
            bool result = false;
            if (!Validate(oArticle.ArticleID))
            {
                var helper = DataHelper.GetInstance();
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@ID", oArticle.ArticleID),
                new SqlParameter("@NAME", oArticle.ArticleName),
                new SqlParameter("@UNIT_PRICE", oArticle.UnitPrice)
                };
                result = helper.ExecuteCrudSPQuery("SP_SaveArticle", parameters);
            }
            return result;
        }

        public bool Update(Article oArticle)
        {
            bool result = false;
            if (Validate(oArticle.ArticleID))
            {
                var helper = DataHelper.GetInstance();
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@ID", oArticle.ArticleID),
                new SqlParameter("@NAME", oArticle.ArticleName),
                new SqlParameter("@UNIT_PRICE", oArticle.UnitPrice)
                };
                result = helper.ExecuteCrudSPQuery("SP_UpdateArticle", parameters);
            }
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            if (Validate(id))
            {
                var helper = DataHelper.GetInstance();
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@ID", id)
                };
                result = helper.ExecuteCrudSPQuery("SP_DeleteArticle", parameters);
            }
            return result;
        }
    }
}
