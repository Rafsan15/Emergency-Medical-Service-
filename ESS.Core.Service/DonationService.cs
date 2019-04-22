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
   public class DonationService:IDonationService
    {
        private ESSDbContext _context;

        public DonationService(ESSDbContext context)
        {
            _context = context;
        }

        public Result<Donation> Save(Donation Entity)
        {
            var result = new Result<Donation>();
            try
            {
                var objtosave = _context.Donations.FirstOrDefault(u => u.DonationId == Entity.DonationId);
                if (objtosave == null)
                {
                    objtosave = new Donation();
                    _context.Donations.Add(Entity);
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
                var objtodelete = _context.Donations.FirstOrDefault(q => q.DonationId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Donations.Remove(objtodelete);
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

        public Result<List<Donation>> GetAll(string key = "")
        {
            var result = new Result<List<Donation>>() { Data = new List<Donation>() };
            try
            {
                IQueryable<Donation> query = _context.Donations;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.DonationId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.DonationId.Equals(Int32.Parse(key)));
                }

                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Donation> GetById(int Id)
        {
            var result = new Result<Donation>();
            try
            {
                var obj = _context.Donations.FirstOrDefault(u => u.DonationId == Id);
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

        public bool IsValid(Donation obj, Result<Donation> result)
        {
            if (!ValidationHelper.IsStringValid((obj.DonationId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid DonationId";
                return false;
            }

            return true;
        }
    }
}
