using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.News;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Generators;

namespace HotelAbshar.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly IAbsharContext _context;
        public NewsService(IAbsharContext context)
        {
            _context = context;
        }
        public int AddNews(News news, IFormFile Image)
        {
            news.CreateDate = DateTime.Now;
            news.DisPlayed = false;
            news.ViewCount = 0;

            if (Image != null)
            {
                news.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(Image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/Images", news.ImageSrc);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                ImageResizer resizer = new ImageResizer();
                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/ThumbImages", news.ImageSrc);
                resizer.Image_resize(imagePath, imageThumbPath, 150);

                var imageShowSitePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/ShowSiteImage", news.ImageSrc);
                resizer.Image_resize(imagePath, imageShowSitePath, 370);

            }
            _context.News.Add(news);
            _context.SaveChanges();


            return news.NewsID;
        }

        public bool ChangeStatus(int id)
        {
            try
            {
                var news = GetNewsByID(id);
                news.DisPlayed = !news.DisPlayed;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteNews(int id)
        {
            try
            {

                var news = GetNewsByID(id);

                news.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

                throw;
            }
        }

        public void EditNews(News news, IFormFile Image)
        {
            if (Image != null)
            {
                if (!string.IsNullOrEmpty(news.ImageSrc))
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/Images", news.ImageSrc);
                    string deleteImageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/ThumbImages", news.ImageSrc);
                    string deleteImageShowSitePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/ShowSiteImage", news.ImageSrc);

                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                    if (File.Exists(deleteImageThumbPath))
                    {
                        File.Delete(deleteImageThumbPath);
                    }
                    if (File.Exists(deleteImageShowSitePath))
                    {
                        File.Delete(deleteImageShowSitePath);
                    }
                }


                news.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(Image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/Images", news.ImageSrc);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                ImageResizer resizer = new ImageResizer();
                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/ThumbImages", news.ImageSrc);
                resizer.Image_resize(imagePath, imageThumbPath, 150);

                var imageShowSitePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/News/ShowSiteImage", news.ImageSrc);
                resizer.Image_resize(imagePath, imageShowSitePath, 370);



            }
            _context.News.Update(news);
            _context.SaveChanges();
        }

        public bool ExistNews(int id)
        {
            return _context.News.Any(p => p.NewsID == id);
        }

        public News GetNewsByID(int id)
        {
            return _context.News.Find(id);
        }

        public Tuple<List<News>, int> GetNewsForAdmin(int pageId = 1, string filterNewsTitle = "", string filterAuthor = "", string order = "", string searchItem = "")
        {
            IQueryable<News> news = _context.News;
            if (!string.IsNullOrEmpty(filterNewsTitle))
            {
                news = news.Where(p => p.Title.Contains(filterNewsTitle));
            }
            if (!string.IsNullOrEmpty(filterAuthor))
            {
                news = news.Where(p => p.Author.Contains(filterAuthor));
            }

            if (!string.IsNullOrEmpty(order))
            {
                switch (order)
                {
                    case "newest":
                        {
                            news = news.OrderByDescending(p => p.NewsID);
                            break;
                        }
                    case "oldest":
                        {
                            news = news.OrderBy(p => p.NewsID);
                            break;
                        }
                    case "maxView":
                        {
                            news = news.OrderByDescending(p => p.ViewCount);
                            break;
                        }
                    case "minView":
                        {
                            news = news.OrderBy(p => p.ViewCount);
                            break;
                        }
                }
            }
            else
            {
                news = news.OrderByDescending(p => p.NewsID);
            }

            if (!string.IsNullOrEmpty(searchItem))
            {
                switch (searchItem)
                {
                    case "haveImage":
                        {
                            news = news.Where(p => p.ImageSrc != null);
                            break;
                        }
                    case "haventImage":
                        {
                            news = news.Where(p => p.ImageSrc == null);
                            break;
                        }
                    case "DisplaySite":
                        {
                            news = news.Where(p => p.DisPlayed ==true);
                            break;
                        }
                    case "NoDisplaySite":
                        {
                            news = news.Where(p => p.DisPlayed == false);
                            break;
                        }
                }
            }

            int take = 3;
            int skip = (pageId-1) * take;

            var pageCount = news.Count() / take;
            return Tuple.Create(news.Skip(skip).Take(take).ToList(),pageCount);
        }

        public Tuple<List<News>, int> GetNewsForSite(int pageId = 1, string filterTitle = "")
        {
            IQueryable<News> news = _context.News.Where(p=>p.DisPlayed==true);
            if (!string.IsNullOrEmpty(filterTitle))
            {
                news = news.Where(p=>p.Title.Contains(filterTitle));
            }

            int take =20;
            int skip = (pageId - 1) * take;

            var pageCount = news.Count() / take;
            return Tuple.Create(news.Skip(skip).Take(take).ToList(), pageCount);
        }

        public void UpdateNews(News news)
        {
            _context.News.Update(news);
            _context.SaveChanges();
        }
    }
}
