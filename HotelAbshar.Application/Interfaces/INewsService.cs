using HotelAbshar.Domain.Entities.News;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
    public interface INewsService
    {
        Tuple<List<HotelAbshar.Domain.Entities.News.News>, int> GetNewsForAdmin(int pageId=1,string filterNewsTitle="",string filterAuthor="",string order="",string searchItem="");
        int AddNews(News news,IFormFile Image) ;
        bool ExistNews(int id);
        News GetNewsByID(int id);
        void EditNews(News news,IFormFile Image);

        bool DeleteNews(int id);

        bool ChangeStatus(int id);

        Tuple<List<HotelAbshar.Domain.Entities.News.News>, int> GetNewsForSite(int pageId=1,string filterTitle="");


        void UpdateNews(News news);
    }
}
