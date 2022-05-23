using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс Фабрики поставщиков услуг
    */
    public interface IProviderFactory
    {
        virtual IProvider create(String name) 
    }
}
