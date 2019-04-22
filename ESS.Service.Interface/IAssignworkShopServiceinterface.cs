using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using Framework;

namespace ESS.Service.Interface
{
    public interface IAssignworkShopServiceinterface:IService<AssignWorkShop>
    {
        Result<List<AssignWorkShop>> GetAllByWorkShopId(int key );
        Result<List<AssignWorkShop>> GetAllByUserId(int key );

    }
}
