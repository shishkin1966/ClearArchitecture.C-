namespace ClearArchitecture.SL
{
    public interface IRequest : INamed, IValidated
    {
        /**
        * Запустить запрос
        */
        void Execute(object obj);

        /**
        * Получить данные запроса
        *
        * @return данные запроса
        */
        object GetData();

        /**
        * Получить имя источника запроса
        *
        * @return имя источника запроса
        */
        string GetSender();

        /**
        * Получить id запроса
        *
        * @return id запроса
        */
        int GetId();

        /**
        * Установить id запроса
        *
        * @param id запроса
        * @return запрос
        */
        IRequest SetId(int id);

        /**
        * Установить Executor запроса
        *
        * @param Executor запроса
        * @return запрос
        */
        IRequest SetExecutor(IExecutor executor);

        /**
        * Установить флаг - запрос прерван
        */
        IRequest SetCanceled();

        /**
        * Проверить прерван ли запрос
        *
        * @return true - запрос прерван
        */
        bool IsCancelled();

        /**
        * Проверить является ли запрос уникальным
        *
        * @return true - при запуске все предыдущие такие же запросы будут прерваны
        */
        bool IsDistinct();

        /**
        * Что делать если такой запрос уже есть
        *
        * @return 0 - удалить старые запросы, 1 = не выполнять
        */
        int GetAction(IRequest oldRequest);

        /**
        * Удалить из очереди Executor
        */
        void RemoveRequest();

        /**
        * Получить имя получателя запроса
        *
        * @return имя получателя запроса
        */
        string GetReceiver();
    }
}
