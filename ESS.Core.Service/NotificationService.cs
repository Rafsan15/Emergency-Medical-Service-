using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using ESS.Infrastructure;
using ESS.Service.Interface;
using Framework;

namespace ESS.Core.Service
{
  public  class NotificationService:INotificationService
    {
        private ESSDbContext _context;

        public NotificationService(ESSDbContext context)
        {
            _context = context;
        }

        public Result<Notification> Save(Notification Entity)
        {
            var result = new Result<Notification>();
            try
            {
                var objtosave = _context.Notifications.FirstOrDefault(u => u.NotificationId == Entity.NotificationId);
                if (objtosave == null)
                {
                    objtosave = new Notification();
                    _context.Notifications.Add(Entity);
                }
                else
                {
                    objtosave = Entity;

                }

                if (!IsValid(objtosave, result))
                {
                    return result;
                }


                _context.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;

            }

            return result;


        }

        public bool Delete(int Id)
        {
            var result = new Result<bool>();
            try
            {
                var objtodelete = _context.Notifications.FirstOrDefault(q => q.NotificationId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Notifications.Remove(objtodelete);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
                return false;
            }

            return true;
        }

        public Result<List<Notification>> GetAll(string key = "")
        {
            var result = new Result<List<Notification>>() { Data = new List<Notification>() };
            try
            {
                IQueryable<Notification> query = _context.Notifications;
               
                query = query.Where(q => q.IsDeliver.Equals("false"));

                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<List<Notification>> GetAllByRequest(int key)
        {
            var result = new Result<List<Notification>>() { Data = new List<Notification>() };
            try
            {
                IQueryable<Notification> query = _context.Notifications;

                query = query.Where(q => q.RequestId==key);

                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<List<Notification>> GetAllByUserId(int key)
        {
            var result = new Result<List<Notification>>() { Data = new List<Notification>() };
            try
            {
                IQueryable<Notification> query = _context.Notifications;

                query = query.Where(q => q.UserId == key);

                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Notification> GetById(int Id)
        {
            var result = new Result<Notification>();
            try
            {
                var obj = _context.Notifications.FirstOrDefault(u => u.RequestId == Id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return result;
                }

                result.Data = obj;
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public bool IsValid(Notification obj, Result<Notification> result)
        {
            if (!ValidationHelper.IsStringValid((obj.NotificationId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid NotificationId";
                return false;
            }

            return true;
        }
    }
}
