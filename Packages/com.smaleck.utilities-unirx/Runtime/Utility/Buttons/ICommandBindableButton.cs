using UnityEngine.UI;

namespace UtilitiesUniRx.Utility.Buttons
{
    public interface ICommandBindableButton : IRxButton
    {
        Button AsCommandBindable { get; }
    }
}
