using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс малого объединения подписчиков
    */
    public interface ISmallUnion<IProviderSubscriber> : IProvider
    {
        /**
        * Получить секретаря (объект учитывающий подписчиков)
        */
        ISecretary<IProviderSubscriber> createSecretary() 

        /**
        * Зарегестрировать подписчика
        *
        * @param subscriber подписчик
        */
        Boolean register(IProviderSubscriber subscriber)  

        /**
        * Отключить подписчика
        *
        * @param subscriber подписчик
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void unregister(IProviderSubscriber subscriber)

        /**
        * Отключить подписчика по его имени
        *
        * @param name имя подписчика
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void unregister(String name)

        /**
        * Получить список подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> getSubscribers()

        /**
        * Получить список валидных подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> getValidatedSubscribers()

        /**
        * Получить список готовых Stateable подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> getReadySubscribers()

        /**
        * Проверить наличие подписчиков
        *
        * @return true - подписчики есть
        */
        Boolean hasSubscribers()

        /**
        * Проверить наличие подписчика
        *
        * @param name имя подписчика
        * @return true - подписчик есть
        */
        Boolean hasSubscriber(String name)

        /**
         * Получить подписчика по его имени
         *
         * @param name имя подписчика
         * @return подписчик
         */
         IProviderSubscriber? getSubscriber(String name) 

         /**
         * Событие - появился первый подписчик
         */
         void onRegisterFirstSubscriber()

        /**
        * Событие - отписан последний подписчик
        */
        void onUnRegisterLastSubscriber()

        /**
        * Событие - добавлен подписчик
        *
        * @param subscriber подписчик
        */
        void onAddSubscriber(IProviderSubscriber subscriber)

        /**
        * Проверить регистрацию подписчика
        *
        * @param subscriber подписчик
        * @return true подписчик зарегистрирован
        */
        Boolean contains(IProviderSubscriber subscriber) 
    }
}
