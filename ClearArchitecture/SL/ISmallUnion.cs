using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс малого объединения подписчиков
    */
    public interface ISmallUnion<IProviderSubscriber> : IProvider
    {
        /*
        * Получить секретаря (объект учитывающий подписчиков)
        */
        //ISecretary<IProviderSubscriber> CreateSecretary();

        /*
        * Зарегестрировать подписчика
        *
        * @param subscriber подписчик
        */
        Boolean Register(IProviderSubscriber subscriber);

        /*
        * Отключить подписчика
        *
        * @param subscriber подписчик
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void Unregister(IProviderSubscriber subscriber);

        /*
        * Отключить подписчика по его имени
        *
        * @param name имя подписчика
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void Unregister(String name);

        /*
        * Получить список подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> GetSubscribers();

        /*
        * Получить список валидных подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> GetValidatedSubscribers();

        /*
        * Получить список готовых Stateable подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> GetReadySubscribers();

        /*
        * Проверить наличие подписчиков
        *
        * @return true - подписчики есть
        */
        Boolean HasSubscribers();

        /*
        * Проверить наличие подписчика
        *
        * @param name имя подписчика
        * @return true - подписчик есть
        */
        Boolean HasSubscriber(String name);

        /*
        * Получить подписчика по его имени
        *
        * @param name имя подписчика
        * @return подписчик
        */
        IProviderSubscriber GetSubscriber(String name);

        /*
        * Событие - появился первый подписчик
        */
        void OnRegisterFirstSubscriber();

        /*
        * Событие - отписан последний подписчик
        */
        void OnUnRegisterLastSubscriber();

        /*
        * Событие - добавлен подписчик
        *
        * @param subscriber подписчик
        */
        void OnAddSubscriber(IProviderSubscriber subscriber);

        /*
        * Проверить регистрацию подписчика
        *
        * @param subscriber подписчик
        * @return true подписчик зарегистрирован
        */
        Boolean Contains(IProviderSubscriber subscriber);
    }
}
