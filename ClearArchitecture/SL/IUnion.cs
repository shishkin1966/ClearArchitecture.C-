﻿using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс объединения подписчиков
    */
    public interface IUnion<T> : ISmallUnion<T> where T : IProviderSubscriber
    {
        /*
         * Установить текущего специалиста
         *
         * @param subscriber специалист
         */
        bool SetCurrentSubscriber(T subscriber);

        /*
         * Получить текущего специалиста
         *
         * @return специалист
         */
        T GetCurrentSubscriber(); 
    }
}
