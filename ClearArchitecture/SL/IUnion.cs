using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс объединения подписчиков
    */
    public interface IUnion<IProviderSubscriber> : ISmallUnion<IProviderSubscriber>
    {
        /*
         * Установить текущего специалиста
         *
         * @param subscriber специалист
         */
        Boolean SetCurrentSubscriber(IProviderSubscriber subscriber);

        /*
         * Получить текущего специалиста
         *
         * @return специалист
         */
        IProviderSubscriber GetCurrentSubscriber(); 

    }
}
