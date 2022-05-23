using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /*
     * Интерфейс объекта, который может быть проверен на жизнеспособность
     */
    public interface IValidated
    {
        /*
        * Проверить работоспособность объекта
        *
        * @return true - объект может обеспечивать свою функциональность
        */
        virtual Boolean isValid() 
    }
}
