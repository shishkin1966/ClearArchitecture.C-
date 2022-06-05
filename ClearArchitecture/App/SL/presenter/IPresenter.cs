namespace ClearArchitecture.SL
{
    public interface IPresenter : IMessengerSubscriber, ILifecycleListener, IActionListener, IActionHandler
    {
        /**
        * Флаг - регистрировать презентер в объединении презентеров
        *
        * @return true - регистрировать (презентер - глобальный)
        */
        bool IsRegister();
    }
}
