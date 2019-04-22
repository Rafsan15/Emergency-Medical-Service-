using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using Framework;

namespace ESS.Service.Interface
{
    public interface IAssignedRequestService : IService<AssignedRequest>
    {
        Result<List<AssignedRequest>> GetVoluenteerAll(int key = 0);
        Result<List<AssignedRequest>> GetDoctorAll(int key = 0);
        Result<List<AssignedRequest>> GetEmployeeAll(int key = 0);

    }
}
