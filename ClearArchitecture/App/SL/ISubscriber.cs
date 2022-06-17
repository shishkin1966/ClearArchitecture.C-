namespace ClearArchitecture.SL
{
    /*
    * Интерфейс подписчика
    */
    public interface ISubscriber : INamed, IValidated, IBusy
    {
        /*
        * Добавить комментарий подписчику
        */
        void AddComment(string comment);

        /*
        * Получить комментарий подписчика
        * @return string - комментарий подписчика
        */
        string GetComment();
    }
}
