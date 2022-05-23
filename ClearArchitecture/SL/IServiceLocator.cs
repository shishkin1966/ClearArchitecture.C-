using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс администратора(Service Locator)
    */
    public interface IServiceLocator
    {
        /*
        * Проверить существование провайдера
        *
        * @param name имя провайдера
        * @return true - провайдер существует
        */
        virtual Boolean exists(String name)

        /*
        * Получить провайдера
        *
        * @param Any  тип провайдера
        * @param name имя провайдера
        * @return провайдер
        */
        virtual Object? getProvider(String name)

        /*
        * Зарегистрировать провайдера
        *
        * @param provider провайдер
        * @return флаг - операция завершена успешно
        */
        virtual Boolean registerProvider(IProvider provider)

        /*
        * Зарегистрировать провайдера
        *
        * @param name имя провайдера
        * @return флаг - операция завершена успешно
        */
        virtual Boolean registerProvider(String name)

        /*
        * Отменить регистрацию провайдера
        *
        * @param name имя провайдера
        * @return флаг - операция завершена успешно
        */
        virtual Boolean unregisterProvider(String name)

        /*
        * Зарегистрировать подписчика провайдера
        *
        * @param subscriber подписчик провайдера
        * @return флаг - операция завершена успешно
        */
        virtual Boolean registerSubscriber(IProviderSubscriber subscriber)

        /*
        * Отменить регистрацию подписчика провайдера
        *
        * @param subscriber подписчик провайдера
        * @return флаг - операция завершена успешно
        */
        virtual Boolean unregisterSubscriber(IProviderSubscriber subscriber)

        /*
        * Установить подписчика текущим
        *
        * @param subscriber подписчик
        * @return флаг - операция завершена успешно
        */
        virtual Boolean setCurrentSubscriber(IProviderSubscriber subscriber)

        /*
        * Остановитить работу service locator
        */
        virtual void stop()

        /*
        * Запустить работу service locator
        */
        virtual void start()

        /*
        * Получить список провайдеров
        *
        * @return список провайдеров
        */
        virtual List<IProvider> getProviders()

        /*
        * Получить фабрику провайдеров
        *
        * @return фабрика провайдеров
        */
        virtual IProviderFactory getProviderFactory()  
    }
}
