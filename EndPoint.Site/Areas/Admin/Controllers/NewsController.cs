using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IPermissionService permissionService;
        public NewsController(INewsService newsService,IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            _newsService = newsService;
        }
        [PermissionChecker(21)]
        public IActionResult Index(int pageId=1,string filterNewsTitle = "",string filterAuthorName = "",string order="",string searchItem="")
        {
            List<SelectListItem> ListSearchItems = new List<SelectListItem> { 
            new SelectListItem{ Value="0",Text="  انتخاب کنید"},
            new SelectListItem{ Value="haveImage",Text="عکس دار ها"},
            new SelectListItem{ Value="haventImage",Text="بدون عکس ها"},
            new SelectListItem{ Value="DisplaySite",Text="  نمایش در سایت"},
            new SelectListItem{ Value="NoDisplaySite",Text="  نمایش نشده در سایت"},
            };

            ViewBag.SearchItems = new SelectList(ListSearchItems,"Value","Text",((searchItem!=""?searchItem:"")));


            List<SelectListItem> OrderListItems = new List<SelectListItem> {
            new SelectListItem{ Value="newest",Text="جدیدترین"},
            new SelectListItem{ Value="oldest",Text= "قدیمی ترین" },
            new SelectListItem{ Value="maxView",Text= "پربازدید ترین" },
            new SelectListItem{ Value="minView",Text= "کم بازدید ترین" },
            };

            ViewBag.OrderList = new SelectList(OrderListItems ,"Value","Text", ((order != "" ? order : "")));


            ViewBag.pageId = pageId;
            ViewBag.FilterNewsTitle = filterNewsTitle;
            ViewBag.FilterAuthorTitle = filterAuthorName;
            ViewBag.order = order;
            ViewBag.searchItem = searchItem;

            return View(_newsService.GetNewsForAdmin(pageId,filterNewsTitle,filterAuthorName,order,searchItem));
        }

        #region Add
        [PermissionChecker(24)]
        public IActionResult Add()
        {
            return View();
        }
        [PermissionChecker(24)]
        [HttpPost]
        public IActionResult Add(News news,IFormFile newImage)
        {
            if (ModelState.IsValid)
            {
                _newsService.AddNews(news, newImage);
            return Redirect("/Admin/News");
            }
            ViewBag.NoIsValid = true;
            return View(news);
        }
        #endregion


        #region Edit
        [PermissionChecker(23)]
        public IActionResult Edit(int id=0)
        {
            if (id==0)
            {
                return NotFound();
            }
            if (_newsService.ExistNews(id))
            {
                return View(_newsService.GetNewsByID(id));

            }
            return NotFound();
        } 

        [HttpPost]
        [PermissionChecker(23)]
        public IActionResult Edit(News news,IFormFile newImage)
        {
            if (ModelState.IsValid)
            {
                _newsService.EditNews(news,newImage);
                return Redirect("/Admin/News");
            }
            ViewBag.NoIsValid = true;
            return View(news);
        }
        #endregion

        #region Delete
        [PermissionChecker(22)]
        public IActionResult Delete(int NewsID=0)
        {
            if (NewsID == 0 || !permissionService.CheckPermission(User.Identity.Name,22))
            {
                return NotFound();
            }
            if (_newsService.ExistNews(NewsID))
            {
                return Json(_newsService.DeleteNews(NewsID));

            }
            return NotFound();
        }
        #endregion

        #region ChangeStatus
        public IActionResult ChangeStatus(int NewsID = 0)
        {
            if (NewsID == 0)
            {
                return NotFound();
            }
            if (_newsService.ExistNews(NewsID))
            {
                return Json(_newsService.ChangeStatus(NewsID));

            }
            return NotFound();
        }
        #endregion

        #region CkEditor
        [HttpPost]
        [Route("file-upload-NewsText")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/News/TextFiles",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/News/TextFiles/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

        #endregion
    }
}
