using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс объекта, который регистрируется у провайдеров для получения/предоставления сервиса
    */
    public interface IProviderSubscriber
    {
        /**
        * Получить список имен провайдеров, у которых должен быть зарегистрирован объект
        *
        * @return список имен провайдеров
        */
        fun getProviderSubscription(): List<String>

        /**
        * Событие - провайдер прекратил работу
        */
        fun onStopProvider(provider: IProvider)

        /**
         * Остановить
        */
        fun stop()
    }
}
