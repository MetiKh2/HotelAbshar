using HotelAbshar.Application.Interfaces;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Generators;
using HotelAbshar.Domain.Entities.Hotels;
using HotelAbshar.Domain.Entities.Reservation;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IAbsharContext _context;
        public HotelService(IAbsharContext context)
        {
            _context = context;
        }

        public void AddFeature(Feature feature)
        {
            _context.Features.Add(feature);
            _context.SaveChanges();
        }

        public int AddHotel(Hotel hotel, List<IFormFile> images)
        {

            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            if (images != null)
            {
                foreach (var item in images)
                {
                    HotelImages hotelImage = new HotelImages
                    {
                        HotelID = hotel.HotelID,
                        Src = GeneratCode.GenerateUniqCode() + Path.GetExtension(item.FileName),
                    };
                    _context.HotelImages.Add(hotelImage);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/HotelImages", hotelImage.Src);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }
                    ImageResizer resizer = new ImageResizer();
                    var thumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/ThumbHotelImage", hotelImage.Src);
                    resizer.Image_resize(imagePath, thumbImagePath, 282);

                    var sliderImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/SliderHotelImages", hotelImage.Src);
                    resizer.Image_resize(imagePath, sliderImagePath, 849);

                }
            }
            _context.SaveChanges();
            return hotel.HotelID;
        }

        public void AddHotelFeatures(int hotelId, List<int> selectedFeatures)
        {
            foreach (var item in selectedFeatures)
            {
                _context.HotelFeatures.Add(new HotelFeatures
                {
                    HotelID = hotelId,
                    FeatureID = item,

                });
            }
            _context.SaveChanges();
        }

        public int AddHotelRoom(HotelRoom room, IFormFile image)
        {

            if (image != null)
            {
                room.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/Room/Images", room.ImageSrc);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                ImageResizer resizer = new ImageResizer();
                var thumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/Room/ThumbImages", room.ImageSrc);
                resizer.Image_resize(imagePath, thumbImagePath, 100);
            }
            _context.HotelRooms.Add(room);
            _context.SaveChanges();
            return room.HotelRoomID;
        }

        public bool DeleteFeature(int featureId)
        {
            try
            {

                var feature = GetFeatureByID(featureId);
                feature.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

                throw;
            }
        }

        public bool DeleteHotel(int hotelId)
        {
            try
            {
                var hotel = GetHotelByID(hotelId);
                hotel.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void DeleteImages(List<string> srcImages)
        {
            foreach (var item in srcImages)
            {
                var image = _context.HotelImages.Where(p => p.Src == item).FirstOrDefault();
                _context.HotelImages.Remove(image);

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/HotelImages", item);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                var thumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/ThumbHotelImage", item);
                if (File.Exists(thumbImagePath))
                {
                    File.Delete(thumbImagePath);
                }
                var sliderImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/SliderHotelImages", item);
                if (File.Exists(sliderImagePath))
                {
                    File.Delete(sliderImagePath);
                }
            }
            _context.SaveChanges();
        }

        public bool DeleteRoom(int roomId)
        {
            try
            {
                var room = GetHotelRoomByID(roomId);
                room.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void EditFeature(Feature feature)
        {
            _context.Features.Update(feature);
            _context.SaveChanges();
        }

        public void EditHotel(Hotel hotel, List<IFormFile> images)
        {
            _context.Hotels.Update(hotel);

            if (images != null)
            {
                foreach (var item in images)
                {
                    HotelImages hotelImage = new HotelImages
                    {
                        HotelID = hotel.HotelID,
                        Src = GeneratCode.GenerateUniqCode() + Path.GetExtension(item.FileName),
                    };
                    _context.HotelImages.Add(hotelImage);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/HotelImages", hotelImage.Src);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }
                    ImageResizer resizer = new ImageResizer();
                    var thumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/ThumbHotelImage", hotelImage.Src);
                    resizer.Image_resize(imagePath, thumbImagePath, 282);

                    var sliderImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/SliderHotelImages", hotelImage.Src);
                    resizer.Image_resize(imagePath, sliderImagePath, 849);

                }
            }
            _context.SaveChanges();
        }

        public void EditHotelFeatures(int hotelId, List<int> selectedFeatures)
        {
            var lastHotelFeatures = _context.HotelFeatures.Where(p => p.HotelID == hotelId).ToList();
            foreach (var item in lastHotelFeatures)
            {
                _context.HotelFeatures.Remove(item);
            }
            AddHotelFeatures(hotelId, selectedFeatures);
        }

        public void EditHotelRoom(HotelRoom room, IFormFile image)
        {
            if (image != null)
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/Room/Images", room.ImageSrc);
                var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/Room/ThumbImages", room.ImageSrc);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
                if (File.Exists(deleteThumbImagePath))
                {
                    File.Delete(deleteThumbImagePath);
                }

                room.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/Room/Images", room.ImageSrc);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                ImageResizer resizer = new ImageResizer();
                var thumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hotels/Room/ThumbImages", room.ImageSrc);
                resizer.Image_resize(imagePath, thumbImagePath, 100);
            }
            _context.HotelRooms.Update(room);
            _context.SaveChanges();
        }

        public string GetCityTitleByCityID(int id)
        {
            return _context.HotelCities.Find(id).CityTitle;
        }



        public Feature GetFeatureByID(int id)
        {
            return _context.Features.Find(id);
        }

        public List<Feature> GetFeatures(string filter = "")
        {
            IQueryable<Feature> features = _context.Features;
            if (!string.IsNullOrEmpty(filter))
            {
                features = features.Where(p => p.FeatureTitle.Contains(filter));
            }
            return features.OrderByDescending(p => p.FeatureID).ToList();
        }

        public List<Feature> GetFeaturesByHotelID(int id)
        {
            var featuresId = _context.HotelFeatures.Where(p => p.HotelID == id).Select(p => p.FeatureID).ToList();
            List<Feature> features = new List<Feature>();
            foreach (var item in featuresId)
            {
                features.Add(GetFeatureByID(item));
            }
            return features;
        }

        public string GetFirstImageByHotelID(int hotelId)
        {
            if (_context.HotelImages.Any(p => p.HotelID == hotelId))
            {
                return _context.HotelImages.FirstOrDefault(p => p.HotelID == hotelId).Src;

            }
            return "";
        }

        public Hotel GetHotelByID(int id)
        {
            return _context.Hotels.Find(id);
        }

        public Hotel GetHotelByRoomID(int id)
        {
            var hotelId= _context.HotelRooms.Where(p => p.HotelRoomID == id).FirstOrDefault().HotelID;
            return GetHotelByID(hotelId);
        }

        public List<SelectListItem> GetHotelCitiesForAddHotel(int provinceId)
        {
            return _context.HotelCities.Where(p => p.ProvinceID == provinceId).Select(p => new SelectListItem
            {
                Value = p.CityID.ToString(),
                Text = p.CityTitle
            }).ToList();
        }

        public List<int> GetHotelFeaturesIDByHotelID(int id)
        {
            return _context.HotelFeatures.Where(p => p.HotelID == id).Select(p => p.FeatureID).ToList();
        }

        public List<SelectListItem> GetHotelProvincesForAddHotel()
        {

            return _context.HotelProvinces.Select(p => new SelectListItem
            {
                Value = p.ProvinceID.ToString(),
                Text = p.ProvinceTitle
            }).ToList();
        }

        public HotelRoom GetHotelRoomByID(int id)
        {
            return _context.HotelRooms.Include(p => p.Hotel).Where(p => p.HotelRoomID == id).FirstOrDefault();
        }

        public Tuple<List<HotelRoom>, int> GetHotelRoomsByHotelID(int hotelId, int pageId = 1, string filter = "", string order = "")
        {
            IQueryable<HotelRoom> rooms = _context.HotelRooms.Where(p => p.HotelID == hotelId);
            if (!string.IsNullOrEmpty(filter))
            {
                rooms = rooms.Where(p => p.HotelRoomTitle.Contains(filter));
            }
            switch (order)
            {
                case "minCapacity":
                    {
                        rooms = rooms.OrderBy(p => p.Capacity);
                        break;
                    }
                case "maxCapacity":
                    {
                        rooms = rooms.OrderByDescending(p => p.Capacity);
                        break;
                    }
                case "maxPrice":
                    {
                        rooms = rooms.OrderByDescending(p => p.PriceForOneNight);
                        break;
                    }
                case "minPrice":
                    {
                        rooms = rooms.OrderBy(p => p.PriceForOneNight);
                        break;
                    }
                case "newest":
                    {
                        rooms = rooms.OrderByDescending(p => p.HotelRoomID);
                        break;
                    }
                case "oldest":
                    {
                        rooms = rooms.OrderBy(p => p.HotelRoomID);
                        break;
                    }

            }
            int take = 2;
            int skip = (pageId - 1) * take;
            var pageCount = rooms.Count() / take;
            return Tuple.Create(rooms.Skip(skip).Take(take).ToList(), pageCount);

        }
        public DateTime SetStringToDate(string date)
        {
            
                string[] DateArray = date.Split('/');

                DateTime newDate = new DateTime(int.Parse(DateArray[0]), int.Parse(DateArray[1]), int.Parse(DateArray[2]), new PersianCalendar());

                return newDate;
            
           
        }
        public bool CheckDateTime(string date)
        {
            try
            {
                string[] DateArray = date.Split('/');

                DateTime newDate = new DateTime(int.Parse(DateArray[0]), int.Parse(DateArray[1]), int.Parse(DateArray[2]), new PersianCalendar());

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public List<HotelRoom> GetHotelRoomsByReservationFilter(int hotelId,string startDate = "", string endDate = "", int capacity = 0)
        {
            IQueryable<HotelRoom> rooms = _context.HotelRooms;
            
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                //string[] stDateArray = startDate.Split('/');

                //= new DateTime(int.Parse(stDateArray[0]), int.Parse(stDateArray[1]), int.Parse(stDateArray[2]), new PersianCalendar());
                if (CheckDateTime(startDate)==true&&CheckDateTime(endDate)==true)
                {
                    DateTime stDate = SetStringToDate(startDate);
                    DateTime edDate = SetStringToDate(endDate);
                    var roomsBoked = from b in _context.Reservations where b.IsFinally==true
                                     where ((stDate >= b.StartDate) && (stDate <= b.EndDate)) ||
                                     ((edDate >= b.StartDate) && (edDate <= b.EndDate)) ||
                                     ((stDate <= b.StartDate) && (edDate >= b.StartDate) && (edDate <= b.EndDate)) ||
                                     ((stDate >= b.StartDate) && (stDate <= b.EndDate) && (edDate >= b.EndDate)) ||
                                     ((stDate <= b.StartDate) && (edDate >= b.EndDate))
                                     select b;
                    rooms = _context.HotelRooms.Where(r => !roomsBoked.Any(b => b.HotelRoomID == r.HotelRoomID));
                }

           


                //string[] edDateArray = startDate.Split('/');

                //DateTime edDate = new DateTime(int.Parse(edDateArray[0]), int.Parse(edDateArray[1]), int.Parse(edDateArray[2]), new PersianCalendar());

        

            }
            if (capacity != 0)
            {
                rooms = rooms.Where(p => p.Capacity == capacity);
            }

            return rooms.OrderByDescending(p => p.HotelRoomID).Where(p=>p.HotelID==hotelId).ToList();
        }

        public List<Hotel> GetHotels()
        {
            return _context.Hotels.ToList();
        }

        public Tuple<List<Hotel>, int> GetHotelsForAdmin(int pageId = 1, int province = 0, int city = 0, string order = "", string filterHotelName = "")
        {
            IQueryable<Hotel> hotels = _context.Hotels.Include(p => p.HotelImages).Include(p => p.HotelCity).Include(p => p.HotelProvince);
            if (!string.IsNullOrEmpty(filterHotelName))
            {
                hotels = hotels.Where(p => p.HotelName.Contains(filterHotelName));
            }

            if (!string.IsNullOrEmpty(order))
            {
                switch (order)
                {
                    case "minStar":
                        {
                            hotels = hotels.OrderBy(p => p.StarCount);
                            break;
                        }
                    case "maxStar":
                        {
                            hotels = hotels.OrderByDescending(p => p.StarCount);
                            break;
                        }
                    //case "minRoom":
                    //    {
                    //        hotels = hotels.OrderBy(p => p.FloorCount);
                    //        break;
                    //    }
                    //case "maxRoom":
                    //    {
                    //        hotels = hotels.OrderByDescending(p => p.FloorCount);
                    //        break;
                    //    }
                    case "minFloor":
                        {
                            hotels = hotels.OrderBy(p => p.FloorsCount);
                            break;
                        }
                    case "maxFloor":
                        {
                            hotels = hotels.OrderByDescending(p => p.FloorsCount);
                            break;
                        }
                    //case "minPrice":
                    //    {
                    //        hotels = hotels.OrderBy(p => p.);
                    //        break;
                    //    }
                    //case "maxPrice":
                    //    {
                    //        hotels = hotels.OrderBy(p => p.);
                    //        break;
                    //    }
                    case "newest":
                        {
                            hotels = hotels.OrderByDescending(p => p.HotelID);
                            break;
                        }
                    case "oldest":
                        {
                            hotels = hotels.OrderBy(p => p.HotelID);
                            break;
                        }
                }
            }
            else
            {
                hotels = hotels.OrderByDescending(p => p.HotelID);
            }
            if (city > 0)
            {
                hotels = hotels.Where(p => p.CityID == city);
            }
            if (province > 0)
            {
                hotels = hotels.Where(p => p.ProvinceID == province);
            }

            int take = 1;
            int skip = (pageId - 1) * take;

            int pageCount = hotels.Count() / take;

            return Tuple.Create(hotels.Skip(skip).Take(take).ToList(), pageCount);
        }

        public Tuple<List<Hotel>, int> GetHotelsForSite(int pageId = 1, List<int> filterStarCount = null, string filterName = "", string filterPrice = "", List<int> filterFeatures = null, int cityID = 0, int provinceID = 0, string order = "", string startDate = "", string endDate = "",int roomCapacity=0)
        {
            IQueryable<Hotel> hotels = _context.Hotels;

            if (!string.IsNullOrEmpty(startDate)&&!string.IsNullOrEmpty(endDate))
            {
                if (CheckDateTime(startDate) == true && CheckDateTime(endDate) == true)
                {
                    DateTime stDate = SetStringToDate(startDate);
                    DateTime edDate = SetStringToDate(endDate);
                    var roomsBoked = from b in _context.Reservations
                                     where ((stDate >= b.StartDate) && (stDate <= b.EndDate)) ||
                                     ((edDate >= b.StartDate) && (edDate <= b.EndDate)) ||
                                     ((stDate <= b.StartDate) && (edDate >= b.StartDate) && (edDate <= b.EndDate)) ||
                                     ((stDate >= b.StartDate) && (stDate <= b.EndDate) && (edDate >= b.EndDate)) ||
                                     ((stDate <= b.StartDate) && (edDate >= b.EndDate))
                                     select b;
                 var   rooms = _context.HotelRooms.Where(r => !roomsBoked.Any(b => b.HotelRoomID == r.HotelRoomID)).ToList();
                    List<int> hotelsId = rooms.Select(p => p.HotelID).ToList();
                    hotels = hotels.Where(p => hotelsId.Contains(p.HotelID));
                }
            }

            if (roomCapacity!=0)
            {
                var roomsByCapacity = _context.HotelRooms.Where(p => p.Capacity == roomCapacity).ToList();
                List<int> hotelsIdByRoomCapacity = roomsByCapacity.Select(p => p.HotelID).ToList();
                hotels = hotels.Where(p => hotelsIdByRoomCapacity.Contains(p.HotelID));
            }


            if (!string.IsNullOrEmpty(filterName))
            {
                hotels = hotels.Where(p => p.HotelName.Contains(filterName));
            }

            if (provinceID != 0)
            {
                hotels = hotels.Where(p => p.ProvinceID == provinceID);
            }

            if (cityID != 0)
            {
                hotels = hotels.Where(p => p.CityID == cityID);
            }

            if (!string.IsNullOrEmpty(filterPrice))
            {
                switch (filterPrice)
                {
                    case "before100000":
                        {
                            hotels = hotels.Where(p => p.MinPriceForOneNight < 100000);
                            break;
                        }
                    case "past100000Before250000":
                        {
                            hotels = hotels.Where(p => p.MinPriceForOneNight >= 100000 && p.MinPriceForOneNight < 250000);
                            break;
                        }
                    case "past250000Before500000":
                        {
                            hotels = hotels.Where(p => p.MinPriceForOneNight >= 250000 && p.MinPriceForOneNight < 500000);
                            break;
                        }
                    case "past500000Before750000":
                        {
                            hotels = hotels.Where(p => p.MinPriceForOneNight >= 500000 && p.MinPriceForOneNight < 750000);
                            break;
                        }
                    case "past750000":
                        {
                            hotels = hotels.Where(p => p.MinPriceForOneNight >= 750000);
                            break;
                        }

                }
            }
            if (filterStarCount.Count > 0)
            {
                switch (filterStarCount.Count)
                {
                    case 1:
                        {
                            hotels = hotels.Where(p => p.StarCount == filterStarCount[0]);
                            break;
                        }
                    case 2:
                        {
                            hotels = hotels.Where(p => p.StarCount == filterStarCount[0] || p.StarCount == filterStarCount[1]);
                            break;
                        }
                    case 3:
                        {
                            hotels = hotels.Where(p => p.StarCount == filterStarCount[0] || p.StarCount == filterStarCount[1] || p.StarCount == filterStarCount[2]);
                            break;
                        }
                    case 4:
                        {
                            hotels = hotels.Where(p => p.StarCount == filterStarCount[0] || p.StarCount == filterStarCount[1] || p.StarCount == filterStarCount[2] || p.StarCount == filterStarCount[3]);
                            break;
                        }
                    case 5:
                        {
                            hotels = hotels.Where(p => p.StarCount == filterStarCount[0] || p.StarCount == filterStarCount[1] || p.StarCount == filterStarCount[2] || p.StarCount == filterStarCount[3] || p.StarCount == filterStarCount[4]);
                            break;
                        }
                }
            }


            if (!string.IsNullOrEmpty(order))
            {
                switch (order)
                {
                    case "maxPrice":
                        {
                            hotels = hotels.OrderByDescending(p=>p.MinPriceForOneNight);
                            break;
                        }
                    case "minPrice":
                        {
                            hotels = hotels.OrderBy(p => p.MinPriceForOneNight);
                            break;
                        }
                }
            }

            

            int take = 2;
            int skip = (pageId - 1) * take;

            var pageCount = hotels.Count() / take;
            return Tuple.Create(hotels.Skip(skip).Take(take).ToList(), pageCount);


        }

        public List<string> GetImagesByHotelID(int id)
        {
            return _context.HotelImages.Where(p => p.HotelID == id).Select(p => p.Src).ToList();
        }

        public IQueryable<Hotel> GetIQueryableHotelByID(int id)
        {
            IQueryable<Hotel> Hotel = _context.Hotels.Where(p => p.HotelID == id);
            return Hotel;
        }

        public string GetProvinceTitleByProvinceID(int id)
        {
            return _context.HotelProvinces.Find(id).ProvinceTitle;
        }

        public Tuple<List<HotelProvince>, List<HotelCity>> GetProvincesAndCities()
        {
            var provinces = _context.HotelProvinces.ToList();
            var cities = _context.HotelCities.ToList();
            return Tuple.Create(provinces,cities);
        }

        public void AddUserHotel(UserHotel userHotel)
        {
            
            if (!_context.UserHotels.Any(p=>p.UserID==userHotel.UserID&&p.HotelID==userHotel.HotelID))
            {
                _context.UserHotels.Add(userHotel);
                _context.SaveChanges();
            }
           
        }

        public List<Hotel> GetHotelsByUserID(int id)
        {
            var hotelsId= _context.UserHotels.Where(p => p.UserID == id).Select(p=>p.HotelID).ToList();

            List<Hotel> hotels = new List<Hotel>();
            foreach (var item in hotelsId)
            {
                hotels.Add(GetHotelByID(item));
            }
            return hotels;
        }

        public void DeleteUserHotel(int userId, int hotelId)
        {
            if (_context.UserHotels.Any(p => p.UserID == userId && p.HotelID == hotelId))
            {
                var userHotel = _context.UserHotels.Where(p => p.UserID == userId && p.HotelID == hotelId).FirstOrDefault();

                _context.UserHotels.Remove(userHotel);
                _context.SaveChanges();

            }
         
        }

        public string GetHotelRoomTitleByID(int id)
        {
            return _context.HotelRooms.Find(id).HotelRoomTitle;
        }

        public int GetHotelIDByRoomID(int id)
        {
            return _context.HotelRooms.Find(id).HotelID;
        }

        public List<User> GetHotelEmployee(int hotelId)
        {
            var usersId= _context.UserHotels.Where(p => p.HotelID == hotelId).Select(p=>p.UserID).ToList();
            List<User> users = new List<User> ();
            foreach (var item in usersId)
            {
                users.Add(_context.Users.Find(item));
            }

            return users;
            
            
        }

        public bool CheckMinAndMaxHotelFacilities(int? vote)
        {
            if (vote==null)
            {
                return true;
            }
            if (vote>10||vote<1)
            {
                return false;
            }
            return true;
        }

        public void AddComment(HotelComment comment)
        {
            _context.HotelComments.Add(comment);
            _context.SaveChanges();
        }

        public List<HotelComment> GetHotelCommentsByHotelID(int id, int pageId = 1)
        {
            return _context.HotelComments.Where(p => p.HotelID == id).Take(pageId * 4).ToList();
        }

        public int GetCommentCountsByHotelID(int id)
        {
            return _context.HotelComments.Where(p => p.HotelID == id).Count() ;
        }

        public double GetLocationVoteAverageByHotelID(int id)
        {
            var voteSum = _context.HotelComments.Where(p => p.HotelID == id&&p.Location!=null).Sum(p=>p.Location);
            var count = _context.HotelComments.Where(p => p.HotelID == id && p.Location != null).Count();
            return (double)voteSum / count;
        }

        public double GetServiceVoteAverageByHotelID(int id)
        {
            var voteSum = _context.HotelComments.Where(p => p.HotelID == id).Sum(p => p.Services);
            var count = _context.HotelComments.Where(p => p.HotelID == id && p.Services != null).Count();
            return (double)voteSum / count;
        }

        public double GetcleanVoteAverageByHotelID(int id)
        {
            var voteSum = _context.HotelComments.Where(p => p.HotelID == id).Sum(p => p.Clean);
            var count = _context.HotelComments.Where(p => p.HotelID == id && p.Clean != null).Count();
            return (double)voteSum / count;
        }

        public double GetEmployeeBehavierVoteAverageByHotelID(int id)
        {
            var voteSum = _context.HotelComments.Where(p => p.HotelID == id).Sum(p => p.EmployeeBehavior);
            var count = _context.HotelComments.Where(p => p.HotelID == id && p.EmployeeBehavior != null).Count();
            return (double)voteSum / count;
        }

        public bool ExistHotel(int id)
        {
            return _context.Hotels.Any(p=>p.HotelID==id);
        }

        public bool ExistRoome(int id)
        {
            return _context.HotelRooms.Any(p => p.HotelRoomID == id);

        }
    }
}
