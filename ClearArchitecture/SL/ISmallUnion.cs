using System;
using System.Collections.Generic;
using System.Text;

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
        virtual ISecretary<IProviderSubscriber> createSecretary()

        /*
        * Зарегестрировать подписчика
        *
        * @param subscriber подписчик
        */
        virtual Boolean register(IProviderSubscriber subscriber)

        /*
        * Отключить подписчика
        *
        * @param subscriber подписчик
        * @return true - провайдер должен быть остановлен и выгружен
        */
        virtual void unregister(IProviderSubscriber subscriber)

        /*
        * Отключить подписчика по его имени
        *
        * @param name имя подписчика
        * @return true - провайдер должен быть остановлен и выгружен
        */
        virtual void unregister(String name)

        /*
        * Получить список подписчиков
        *
        * @return список подписчиков
        */
        virtual List<IProviderSubscriber> getSubscribers()

        /*
        * Получить список валидных подписчиков
        *
        * @return список подписчиков
        */
        virtual List<IProviderSubscriber> getValidatedSubscribers()

        /*
        * Получить список готовых Stateable подписчиков
        *
        * @return список подписчиков
        */
        virtual List<IProviderSubscriber> getReadySubscribers()

        /*
        * Проверить наличие подписчиков
        *
        * @return true - подписчики есть
        */
        virtual Boolean hasSubscribers()

        /*
        * Проверить наличие подписчика
        *
        * @param name имя подписчика
        * @return true - подписчик есть
        */
        virtual Boolean hasSubscriber(String name)

        /*
         * Получить подписчика по его имени
         *
         * @param name имя подписчика
         * @return подписчик
         */
         virtual IProviderSubscriber? getSubscriber(String name)

         /*
         * Событие - появился первый подписчик
         */
         virtual void onRegisterFirstSubscriber()

        /*
        * Событие - отписан последний подписчик
        */
        virtual void onUnRegisterLastSubscriber()

        /*
        * Событие - добавлен подписчик
        *
        * @param subscriber подписчик
        */
        virtual void onAddSubscriber(IProviderSubscriber subscriber)

        /*
        * Проверить регистрацию подписчика
        *
        * @param subscriber подписчик
        * @return true подписчик зарегистрирован
        */
        virtual Boolean contains(IProviderSubscriber subscriber) 
    }
}
