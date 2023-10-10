using System;
using System.Threading.Tasks;

namespace XGENO.Mobile.Framework.Services
{
    public interface ISMSService
    {
        string SendSMS(string fromNameOrNumber, string toNo, string message);
    }
}
