using Code.Runtime.UI;

namespace Code.Runtime.Services.WindowsService
{
    public interface IWindowService
    {
        void Open(WindowType windowType, bool returnPage = false);
        void Close();
        void Initialize();
    }
}