using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UtilitiesUniRx.Utility
{
    [DisallowMultipleComponent]
    public class RxButton : Button, IRxButton
    {
        [SerializeField] private Sprite _inactiveButtonSprite;

        private readonly Subject<Unit> _buttonClickedObservable = new Subject<Unit>();
        public IObservable<Unit> OnButtonClicked => _buttonClickedObservable;

        private readonly Subject<Unit> _inactiveButtonClickedObservable = new Subject<Unit>();
        public IObservable<Unit> OnInactiveButtonClicked => _inactiveButtonClickedObservable;

        private IReadOnlyReactiveProperty<bool> _isInteractable = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsInteractable
        {
            get
            {
                return _isInteractable != null
                    ? _isInteractable
                    : (_isInteractable = this
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
