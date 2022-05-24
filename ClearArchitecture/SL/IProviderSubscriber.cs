using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс объекта, который регистрируется у провайдеров для получения/предоставления сервиса
    */
    public interface IProviderSubscriber : ISubscriber
    {
        /*
        * Получить список имен провайдеров, у которых должен быть зарегистрирован объект
        *
        * @return список имен провайдеров
        */
        List<String> GetProviderSubscription();

        /*
        * Событие - провайдер прекратил работу
        */
        void OnStopProvider(IProvider provider);

        /*
        * Остановить
        */
        void Stop();
    }
}
