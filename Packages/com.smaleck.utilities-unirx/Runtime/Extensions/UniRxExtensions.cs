using System;
using System.Linq;
using UniRx;
using UtilitiesUniRx.Utility.Buttons;

namespace UtilitiesUniRx.Extensions
{
    public static class UniRxExtensions
    {
        public static IDisposable SubscribeBlind<T>(this IObservable<T> source, Action action)
        {
            return source.Subscribe(_ => action());
        }

        public static IDisposable SubscribeBlind<T, TResult>(this IObservable<T> source, Func<TResult> function)
        {
            return source.Subscribe(_ => function());
        }

        public static IObservable<T> SkipInitialValue<T>(this IReadOnlyReactiveProperty<T> source)
        {
            return source.HasValue ? source.Skip(1) : source;
        }

        public static IObservable<T> IfNotNull<T>(this IObservable<T> elements)
        {
            return elements.Where(value => value != null);
        }

        public static IObservable<Pair<bool>> IfSwitchedToTrue(this IObservable<bool> observable)
        {
            return observable.Pairwise()
                .Where(pair => !pair.Previous && pair.Current);
        }

        public static IObservable<Pair<bool>> IfSwitchedToFalse(this IObservable<bool> observable)
        {
            return observable.Pairwise()
                .Where(pair => pair.Previous && !pair.Current);
        }

        public static IObservable<Pair<int>> IfIncreased(this IObservable<int> observable)
        {
            return observable.Pairwise()
                .Where(pair => pair.Previous < pair.Current);
        }

        public static IObservable<Pair<int>> IfDecreased(this IObservable<int> observable)
        {
            return observable.Pairwise()
                .Where(pair => pair.Previous > pair.Current);
        }

        public static IObservable<Pair<double>> IfIncreased(this IObservable<double> observable)
        {
            return observable.Pairwise()
                .Where(pair => pair.Previous < pair.Current);
        }

        public static IObservable<Pair<double>> IfDecreased(this IObservable<double> observable)
        {
            return observable.Pairwise()
                .Where(pair => pair.Previous > pair.Current);
        }

        public static IObservable<T> OncePerFrame<T>(this IObservable<T> observable)
        {
            return observable
                .BatchFrame()
                .Select(batch => batch.Last());
        }

        public static IObservable<Unit> OnAnyCollectionChange<T>(this IReadOnlyReactiveCollection<T> reactiveCollection)
        {
            return Observable.Merge(
                reactiveCollection.ObserveReset().AsUnitObservable(),
                reactiveCollection.ObserveAdd().AsUnitObservable(),
                reactiveCollection.ObserveMove().AsUnitObservable(),
                reactiveCollection.ObserveRemove().AsUnitObservable(),
                reactiveCollection.ObserveReplace().AsUnitObservable());
        }

        public static IObservable<Unit> OnAnyCollectionChange<TKey, TValue>(this IReadOnlyReactiveDictionary<TKey, TValue> reactiveDictionary)
        {
            return Observable.Merge(
                reactiveDictionary.ObserveReset().AsUnitObservable(),
                reactiveDictionary.ObserveAdd().AsUnitObservable(),
                reactiveDictionary.ObserveRemove().AsUnitObservable(),
                reactiveDictionary.ObserveReplace().AsUnitObservable(),
                reactiveDictionary.ObserveCountChanged().AsUnitObservable());
        }

        public static IObservable<TSource> TakeUntilInclusive<TSource>(
            this IObservable<TSource> source, Func<TSource, bool> predicate)
        {
            return Observable
                .Create<TSource>(o => source.Subscribe(x =>
                {
                    o.OnNext(x);
                    if (predicate(x))
                    {
                        o.OnCompleted();
                    }
                },
                    o.OnError,
                    o.OnCompleted
                ));
        }



        public static IDisposable BindTo(this IReactiveCommand<Unit> command, ICommandBindableButton button)
        {
            return command.BindTo(button.AsCommandBindable);
        }
    }
}
