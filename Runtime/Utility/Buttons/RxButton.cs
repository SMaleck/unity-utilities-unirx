using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UtilitiesUniRx.Utility.Buttons
{
    [DisallowMultipleComponent]
    public class RxButton : Button, IRxButton
    {
        [SerializeField] private Sprite _inactiveButtonSprite;

        private readonly Subject<Unit> _buttonClickedObservable = new Subject<Unit>();
        public IObservable<Unit> OnButtonClicked => _buttonClickedObservable;

        private readonly Subject<Unit> _inactiveButtonClickedObservable = new Subject<Unit>();
        public IObservable<Unit> OnInactiveButtonClicked => _inactiveButtonClickedObservable;

        private IReadOnlyReactiveProperty<bool> _isEnabled = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsEnabled
        {
            get
            {
                return _isEnabled != null
                    ? _isEnabled
                    : (_isEnabled = this
                        .ObserveEveryValueChanged(b => b.interactable)
                        .ToReadOnlyReactiveProperty());
            }
        }

        public void SetIsInteractable(bool value)
        {
            interactable = value;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (interactable)
            {
                _buttonClickedObservable.OnNext(Unit.Default);
            }
            else
            {
                _inactiveButtonClickedObservable.OnNext(Unit.Default);
            }
        }
    }
}
