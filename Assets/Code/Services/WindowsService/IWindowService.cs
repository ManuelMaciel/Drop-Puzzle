using Code.Runtime.UI;

namespace Code.Services.WindowsService
{
    public interface IWindowService
    {
        void Open(WindowType windowType, bool returnPage = false);
        void Close();
    }
}