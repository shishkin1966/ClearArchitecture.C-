using System;
using System.Collections.Generic;
using System.Text;

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
        virtual Boolean setCurrentSubscriber(IProviderSubscriber subscriber)

        /*
         * Получить текущего специалиста
         *
         * @return специалист
         */
        virtual IProviderSubscriber? getCurrentSubscriber() 

    }
}
