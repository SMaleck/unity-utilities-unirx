using UnityEngine.UI;

namespace UtilitiesUniRx.Utility
{
    public interface ICommandBindableButton : IRxButton
    {
        Button AsCommandBindable { get; }
    }
}
