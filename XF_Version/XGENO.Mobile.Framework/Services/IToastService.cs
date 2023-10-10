using System;
namespace XGENO.Mobile.Framework.Services
{
    public interface IToastService
    {
        void ShortAlert(string message);
        void LongAlert(string message);
    }
}
