using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using Framework;

namespace ESS.Service.Interface
{
   public interface INotificationService:IService<Notification>
   {
       Result<List<Notification>> GetAllByRequest(int key);
       Result<List<Notification>> GetAllByUserId(int key);

   }
}
