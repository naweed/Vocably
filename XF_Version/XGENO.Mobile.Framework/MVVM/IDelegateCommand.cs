using System.Threading.Tasks;
using System.Windows.Input;

namespace XGENO.Mobile.Framework.MVVM
{
    public interface IDelegateCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public interface IDelegateCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }

}
