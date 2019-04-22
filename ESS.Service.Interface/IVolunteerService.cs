using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using Framework;

namespace ESS.Service.Interface
{
    public interface IVolunteerService : IService<Volunteer>
    {
        Result<Volunteer> GetVolunteerById(int Id);

    }
}
