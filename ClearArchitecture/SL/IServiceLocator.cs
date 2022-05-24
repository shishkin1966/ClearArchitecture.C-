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
        Boolean Exists(String name);

        /*
        * Получить провайдера
        *
        * @param Any  тип провайдера
        * @param name имя провайдера
        * @return провайдер
        */
        Object GetProvider(String name);

        /*
        * Зарегистрировать провайдера
        *
        * @param provider провайдер
        * @return флаг - операция завершена успешно
        */
        Boolean RegisterProvider(IProvider provider);

        /*
        * Зарегистрировать провайдера
        *
        * @param name имя провайдера
        * @return флаг - операция завершена успешно
        */
        Boolean RegisterProvider(String name);

        /*
        * Отменить регистрацию провайдера
        *
        * @param name имя провайдера
        * @return флаг - операция завершена успешно
        */
        Boolean UnregisterProvider(String name);

        /*
        * Зарегистрировать подписчика провайдера
        *
        * @param subscriber подписчик провайдера
        * @return флаг - операция завершена успешно
        */
        Boolean RegisterSubscriber(IProviderSubscriber subscriber);

        /*
        * Отменить регистрацию подписчика провайдера
        *
        * @param subscriber подписчик провайдера
        * @return флаг - операция завершена успешно
        */
        Boolean UnregisterSubscriber(IProviderSubscriber subscriber);

        /*
        * Установить подписчика текущим
        *
        * @param subscriber подписчик
        * @return флаг - операция завершена успешно
        */
        Boolean setCurrentSubscriber(IProviderSubscriber subscriber);

        /*
        * Остановитить работу service locator
        */
        void Stop();

        /*
        * Запустить работу service locator
        */
        void Start();

        /*
        * Получить список провайдеров
        *
        * @return список провайдеров
        */
        List<IProvider> GetProviders();

        /*
        * Получить фабрику провайдеров
        *
        * @return фабрика провайдеров
        */
        IProviderFactory GetProviderFactory();  
    }
}
