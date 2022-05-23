using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс объекта, который регистрируется у провайдеров для получения/предоставления сервиса
    */
    public interface IProviderSubscriber
    {
        /*
        * Получить список имен провайдеров, у которых должен быть зарегистрирован объект
        *
        * @return список имен провайдеров
        */
        virtual List<String> getProviderSubscription()

        /*
        * Событие - провайдер прекратил работу
        */
        virtual void onStopProvider(IProvider provider)

        /*
        * Остановить
        */
        virtual void stop()
    }
}
