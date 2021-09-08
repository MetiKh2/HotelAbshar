using HotelAbshar.Domain.Entities.Hotels;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
  public  interface IHotelService
    {
        List<SelectListItem> GetHotelProvincesForAddHotel();
        List<SelectListItem> GetHotelCitiesForAddHotel(int provinceId);
        Tuple<List<HotelProvince>, List<HotelCity>> GetProvincesAndCities();

        #region Feature
        void AddFeature(Feature feature);
        List<Feature> GetFeatures(string filter="");
        List<Feature> GetFeaturesByHotelID(int id);
        Feature GetFeatureByID(int id);
        void EditFeature(Feature feature);
        bool DeleteFeature(int featureId);
        void EditHotelFeatures(int hotelId,List<int> selectedFeatures);
        void AddHotelFeatures(int hotelId,List<int> selectedFeatures);
        #endregion

        #region Hotel
        Tuple<List<Hotel>, int> GetHotelsForAdmin(int pageId=1,int province = 0, int city = 0, string order = "", string filterHotelName = "");
        string GetFirstImageByHotelID(int hotelId);
        
        string GetCityTitleByCityID(int id);
        string GetProvinceTitleByProvinceID(int id);

        Hotel GetHotelByID(int id);
        List<int> GetHotelFeaturesIDByHotelID(int id);
        List<string> GetImagesByHotelID(int id);
        void DeleteImages(List<string> srcImages);

        int AddHotel(Hotel hotel, List<IFormFile> images);

        void EditHotel(Hotel hotel,List<IFormFile> images);

        bool DeleteHotel(int hotelId);
        Tuple<List<Hotel>, int> GetHotelsForSite(int pageId = 1, List<int> filterStarCount = null, string filterName = "", string filterPrice = "", List<int> filterFeatures = null,int cityID=0,int provinceID=0,string order="",string startDate="",string endDate="", int roomCapacity = 0);
        IQueryable<Hotel> GetIQueryableHotelByID(int id);
        List<Hotel> GetHotels();
        Hotel GetHotelByRoomID(int id);
        int GetHotelIDByRoomID(int id);
        bool ExistHotel(int id);
        bool CheckMinAndMaxHotelFacilities(int? vote);
        #endregion

        #region Room
        int AddHotelRoom(HotelRoom room,IFormFile image);
        Tuple<List<HotelRoom>, int> GetHotelRoomsByHotelID(int hotelId,int pageId=1,string filter="",string order="");
        HotelRoom GetHotelRoomByID(int id);
        void EditHotelRoom(HotelRoom room,IFormFile image);
        bool DeleteRoom(int roomId);


        bool ExistRoome(int id);
        List<HotelRoom> GetHotelRoomsByReservationFilter(int hotelId,string startDate="",string endDate="",int capacity=0);
        #endregion
        DateTime SetStringToDate(string date);
        bool CheckDateTime(string date);
        string GetHotelRoomTitleByID(int id);


        #region UserHotel
        void AddUserHotel(UserHotel userHotel);
        List<Hotel> GetHotelsByUserID(int id);
        void DeleteUserHotel(int userId,int hotelId);
        List<User> GetHotelEmployee(int hotelId);
        #endregion

        #region Comment
        void AddComment(HotelComment comment);
        List<HotelComment> GetHotelCommentsByHotelID(int id,int pageId=1);
        int GetCommentCountsByHotelID(int id);
        double GetLocationVoteAverageByHotelID(int id);
        double GetServiceVoteAverageByHotelID(int id);
        double GetcleanVoteAverageByHotelID(int id);
        double GetEmployeeBehavierVoteAverageByHotelID(int id);
        #endregion

    }
}
