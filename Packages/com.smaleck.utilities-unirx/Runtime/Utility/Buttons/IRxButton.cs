using System;
using UniRx;

namespace UtilitiesUniRx.Utility
{
    public interface IRxButton
    {
        IObservable<Unit> OnButtonClicked { get; }
        IObservable<Unit> OnInactiveButtonClicked { get; }
        IReadOnlyReactiveProperty<bool> IsInteractable { get; }
    }
}
