using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс малого объединения подписчиков
    */
    public interface ISmallUnion<T> : IProvider where T : IProviderSubscriber
    {
        /*
        * Зарегестрировать подписчика
        *
        * @param subscriber подписчик
        */
        bool RegisterSubscriber(T subscriber);

        /*
        * Отключить подписчика
        *
        * @param subscriber подписчик
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void UnRegisterSubscriber(T subscriber);

        /*
        * Отключить подписчика по его имени
        *
        * @param name имя подписчика
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void UnRegisterSubscriber(string name);

        /*
        * Получить список подписчиков
        *
        * @return список подписчиков
        */
        List<T> GetSubscribers();

        /*
        * Получить список валидных подписчиков
        *
        * @return список подписчиков
        */
        List<T> GetValidatedSubscribers();

        /*
        * Получить список готовых Stateable подписчиков
        *
        * @return список подписчиков
        */
        List<T> GetReadySubscribers();

        /*
        * Проверить наличие подписчиков
        *
        * @return true - подписчики есть
        */
        bool HasSubscribers();

        /*
        * Проверить наличие подписчика
        *
        * @param name имя подписчика
        * @return true - подписчик есть
        */
        bool HasSubscriber(string name);

        /*
        * Получить подписчика по его имени
        *
        * @param name имя подписчика
        * @return подписчик
        */
        T GetSubscriber(string name);

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
        void OnAddSubscriber(T subscriber);

        /*
        * Проверить регистрацию подписчика
        *
        * @param subscriber подписчик
        * @return true подписчик зарегистрирован
        */
        bool ContainsSubscriber(T subscriber);
    }
}
