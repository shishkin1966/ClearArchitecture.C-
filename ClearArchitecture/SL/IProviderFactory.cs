using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс Фабрики поставщиков услуг
    */
    public interface IProviderFactory
    {
        IProvider create(String name) 
    }
}
