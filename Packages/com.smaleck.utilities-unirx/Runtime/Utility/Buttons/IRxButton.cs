using System;
using UniRx;

namespace UtilitiesUniRx.Utility.Buttons
{
    public interface IRxButton
    {
        IObservable<Unit> OnButtonClicked { get; }
        IObservable<Unit> OnInactiveButtonClicked { get; }
        IReadOnlyReactiveProperty<bool> IsEnabled { get; }
    }
}
