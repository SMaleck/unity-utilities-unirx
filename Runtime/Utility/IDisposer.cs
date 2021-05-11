using System;

namespace UtilitiesUniRx.Utility
{
    public interface IDisposer
    {
        void Add(IDisposable disposable);
    }
}