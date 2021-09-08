using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IUserService _userService;
        public HotelsController(IHotelService hotelService,IUserService userService)
        {
            _userService = userService;
            _hotelService = hotelService;
        }
        public IActionResult Index(bool layoutSearch=false,int pageId = 1, List<int> filterStarCount = null, string filterName = "", string filterPrice = "", List<int> filterFeatures = null, int cityID = 0, int provinceID = 0,string order="",string startDate="",string endDate="",int roomCapacity=0)
        {
            ViewBag.features = _hotelService.GetFeatures();
            ViewBag.filterName=filterName;
            ViewBag.filterPrice = filterPrice;
            ViewBag.filterStarCount = filterStarCount;
            ViewBag.filterFeatures = filterFeatures;
            ViewBag.pageId = pageId;
            ViewBag.order = order;
            return View(_hotelService.GetHotelsForSite(pageId,filterStarCount,filterName, filterPrice, filterFeatures,cityID,provinceID,order,startDate,endDate,roomCapacity));
        }

        public IActionResult ShowHotel(int id,string startDate="",string endDate="",int capacity=0)
        {
            var hotel = _hotelService.GetHotelByID(id);
            ViewBag.features = _hotelService.GetFeaturesByHotelID(id);
            ViewBag.Images = _hotelService.GetImagesByHotelID(id);
            ViewBag.Rooms = _hotelService.GetHotelRoomsByReservationFilter(id, startDate, endDate, capacity);

            if (hotel==null)
            {
                return NotFound();
            }

            if (_hotelService.CheckDateTime(startDate)==false&&startDate!="")
            {
                #region Votes

                ViewBag.cleanVotes = _hotelService.GetcleanVoteAverageByHotelID(id);
                ViewBag.employeeBehaviorVotes = _hotelService.GetEmployeeBehavierVoteAverageByHotelID(id);
                ViewBag.locationVotes = _hotelService.GetLocationVoteAverageByHotelID(id);
                ViewBag.serviceVotes = _hotelService.GetServiceVoteAverageByHotelID(id);
                #endregion
                ViewBag.wrongDate = true;
                return View(hotel);
            }
            else
            {
                ViewBag.startDate = startDate;
              

            }
            if (_hotelService.CheckDateTime(endDate)==false&&endDate!="")
            {
                #region Votes

                ViewBag.cleanVotes = _hotelService.GetcleanVoteAverageByHotelID(id);
                ViewBag.employeeBehaviorVotes = _hotelService.GetEmployeeBehavierVoteAverageByHotelID(id);
                ViewBag.locationVotes = _hotelService.GetLocationVoteAverageByHotelID(id);
                ViewBag.serviceVotes = _hotelService.GetServiceVoteAverageByHotelID(id);
                #endregion
                ViewBag.wrongDate = true;
                return View(hotel);
            }
            else
            {
                ViewBag.EndDate = endDate;


            }
            if (endDate!=""&&startDate!="")
            {
                if (_hotelService.SetStringToDate(startDate) > _hotelService.SetStringToDate(endDate))
                {
                    #region Votes

                    ViewBag.cleanVotes = _hotelService.GetcleanVoteAverageByHotelID(id);
                    ViewBag.employeeBehaviorVotes = _hotelService.GetEmployeeBehavierVoteAverageByHotelID(id);
                    ViewBag.locationVotes = _hotelService.GetLocationVoteAverageByHotelID(id);
                    ViewBag.serviceVotes = _hotelService.GetServiceVoteAverageByHotelID(id);
                    #endregion
                    ViewBag.StartDateBigger = true;
                    return View(hotel);
                }
            }    //   ViewBag.commentsCount = _hotelService.GetCommentCountsByHotelID(id);
            #region Votes

            ViewBag.cleanVotes = _hotelService.GetcleanVoteAverageByHotelID(id);
            ViewBag.employeeBehaviorVotes = _hotelService.GetEmployeeBehavierVoteAverageByHotelID(id);
            ViewBag.locationVotes = _hotelService.GetLocationVoteAverageByHotelID(id);
            ViewBag.serviceVotes = _hotelService.GetServiceVoteAverageByHotelID(id);
            #endregion
            return View(hotel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(int hotelId,string text,int? clean,int? service,int? EmployeeBehavior,int? location)
        {
            HotelComment comment = new HotelComment { 
            Clean=clean,
            EmployeeBehavior=EmployeeBehavior,
            HotelID=hotelId,
            Location=location,
            Services=service,
            Text=text,
             UserID = _userService.GetUserIDByUserName(User.Identity.Name),
            DateTime = DateTime.Now
        };
         
            

            if (!_hotelService.CheckMinAndMaxHotelFacilities(comment.Clean)|| !_hotelService.CheckMinAndMaxHotelFacilities(comment.Location) || !_hotelService.CheckMinAndMaxHotelFacilities(comment.Services) || !_hotelService.CheckMinAndMaxHotelFacilities(comment.EmployeeBehavior) )
            {
                comment.EmployeeBehavior = null;
                comment.Clean = null;
                comment.Services = null;
                comment.Location = null;
            }
            
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrWhiteSpace(text))
                _hotelService.AddComment(comment); 
            }

            return View("ShowComments", _hotelService.GetHotelCommentsByHotelID(hotelId));
        }

        public IActionResult ShowComments(int id,int pageId = 1)
        {
            return View(_hotelService.GetHotelCommentsByHotelID(id,pageId)) ;
        }
    }
}
