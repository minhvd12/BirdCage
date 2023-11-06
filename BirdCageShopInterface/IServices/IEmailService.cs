using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCageShopOther.Email;

namespace BirdCageShopInterface.IServices
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
