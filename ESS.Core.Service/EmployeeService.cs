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
    public class EmployeeService:IEmployeeService
    {
        private ESSDbContext _context;

        public EmployeeService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<Employee> Save(Employee Entity)
        {
            var result = new Result<Employee>();
            try
            {
                var objtosave = _context.Employees.FirstOrDefault(u => u.EmployeeId == Entity.EmployeeId);
                if (objtosave == null)
                {
                    objtosave = new Employee();
                    _context.Employees.Add(Entity);
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
                var objtodelete = _context.Employees.FirstOrDefault(q => q.UserId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Employees.Remove(objtodelete);
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

        public Result<List<Employee>> GetAll(string key = "")
        {
            var result = new Result<List<Employee>>() { Data = new List<Employee>() };
            try
            {
                IQueryable<Employee> query = _context.Employees;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.EmployeeId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.EmployeeId.Equals(Int32.Parse(key)));
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

        public Result<Employee> GetById(int Id)
        {
            var result = new Result<Employee>();
            try
            {
                var obj = _context.Employees.FirstOrDefault(u => u.UserId == Id);
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

        public bool IsValid(Employee obj, Result<Employee> result)
        {
            if (!ValidationHelper.IsStringValid((obj.EmployeeId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid EmployeeId";
                return false;
            }

            return true;
        }
    }
}
