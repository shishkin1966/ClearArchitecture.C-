﻿using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsProviderSubscriber : AbsSubscriber, IProviderSubscriber
    {
        private readonly Secretary<string> providers = new();

        public abstract List<string> GetProviderSubscription();

        protected AbsProviderSubscriber(string name) : base(name)
        {
        }

        public void OnStopProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            RemoveProvider(provider);
            OnRemoveProvider(provider);
        }

        public void Stop()
        {
            providers.Clear();
        }

        public List<string> GetProviders()
        {
            return providers.Values();
        }

        public void SetProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            providers.Put(provider,provider);
            OnSetProvider(provider);
        }

        public void RemoveProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            providers.Remove(provider);
        }

        public void OnSetProvider(string provider)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": Подключен к провайдеру " + provider + " подписчик "+ GetName());
        }

        public void OnRemoveProvider(string provider)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": Отключен от провайдера " + provider + " подписчик " + GetName());
        }
    }
}
