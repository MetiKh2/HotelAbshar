using HotelAbshar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IActionResult Index(int pageId = 1, string filterTitle = "")
        {
            ViewBag.pageId = pageId;
            ViewBag.filterTitle = filterTitle;
            return View(_newsService.GetNewsForSite(pageId, filterTitle));
        }


        public IActionResult ShowNews(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (_newsService.ExistNews(id))
            {
                var news = _newsService.GetNewsByID(id);
                if (news.DisPlayed)
                {
                    news.ViewCount++;
                    _newsService.UpdateNews(news);
                    return View(news);

                }
            }
            return NotFound();
        }
    }
}
