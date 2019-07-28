using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Excalibur.Tests
{
    // Voor uitleg van IObserver en IObservable zie:
    // http://introtorx.com/Content/v1.0.10621.0/02_KeyTypes.html

    public class ObservableTests
    {
        [Fact]
        public async Task SimpleTask()
        {
            var log = new List<string>();

            try
            {
                var task = Task.FromResult(42);

                var value = await task.ConfigureAwait(false);
                log.Add($"value: {value}");

                log.Add("completed");
            }
            catch (Exception ex)
            {
                log.Add($"error: {ex.Message}");
            }

            log.Should().BeEquivalentTo("value: 42", "completed");
        }

        [Fact]
        public void SimpleTaskToObservable()
        {
            // Deze test doet functioneel hetzelfde als bovenstaande test SimpleTask maar maakt gebruik van IObservable.
            // De task wordt uitgevoerd zodra de Subscribe method op de observable wordt aangeroepen.
            // Eerst wordt de OnNext() aangeroepen met het result van de task als parameter, gevolgd door OnCompleted().

            var log = new List<string>();
            
            var task = Task.FromResult(42);

            var observable = task.ToObservable();

            observable.Subscribe(
                onNext: value => log.Add($"value: {value}"),
                onCompleted: () => log.Add("completed"),
                onError: ex => log.Add($"error: {ex.Message}"));

            log.Should().BeEquivalentTo("value: 42", "completed");
        }

        [Fact]
        public async Task ExceptionThrowingTask()
        {
            var log = new List<string>();

            try
            {
                var task = Task.FromException<int>(new ArgumentException("oops"));

                var value = await task.ConfigureAwait(false);
                log.Add($"value: {value}");

                log.Add("completed");
            }
            catch (Exception ex)
            {
                log.Add($"error: {ex.Message}");
            }

            log.Should().BeEquivalentTo("error: oops");
        }

        [Fact]
        public void ExceptionThrowingTaskToObservable()
        {
            // Deze test doet functioneel hetzelfde als bovenstaande test ExceptionThrowingTask maar maakt gebruik van IObservable.
            // De task wordt uitgevoerd zodra de Subscribe method op de observable wordt aangeroepen.
            // Omdat er binnen de task een Exception wordt gegooid, wordt de OnError() aangeroepen met deze exception als parameter.

            var log = new List<string>();
            
            var task = Task.FromException<int>(new ArgumentException("oops"));
            
            var observable = task.ToObservable();

            observable.Subscribe(
                onNext: value => log.Add($"value: {value}"),
                onCompleted: () => log.Add("completed"),
                onError: ex => log.Add($"error: {ex.Message}"));

            log.Should().BeEquivalentTo("error: oops");
        }

        [Fact]
        public void CombiningObservables()
        {
            var log = new List<string>();

            var firstObservable = Enumerable.Range(1, 3).ToObservable();
            var secondObservable = Enumerable.Range(5, 3).ToObservable();
            
            // Zie ook: http://reactivex.io/documentation/operators/merge.html
            var combined = firstObservable.Merge(secondObservable);

            combined.Subscribe(
                onNext: value => log.Add($"value: {value}"),
                onCompleted: () => log.Add("completed"),
                onError: ex => log.Add($"error: {ex.Message}"));

            log.Should().BeEquivalentTo("value: 1", "value: 2", "value: 3", "value: 5", "value: 6", "value: 7", "completed");
        }

        [Fact]
        public void TakeUntil()
        {
            var log = new List<string>();

            var firstObservable = Enumerable.Range(1, 10).ToObservable();

            // Blijf waardes uit de eerste observable nemen. Stop de stream zodra de waarde deelbaar door 5 is.
            // Het stoppen van de stream kan ook nadat een andere observable een waarde geeft of na een bepaalde tijd.
            // Zie ook: http://reactivex.io/documentation/operators/takeuntil.html
            var secondObservable = firstObservable.TakeUntil(x => x % 5 == 0);

            secondObservable.Subscribe(
                onNext: value => log.Add($"value: {value}"),
                onCompleted: () => log.Add("completed"),
                onError: ex => log.Add($"error: {ex.Message}"));

            log.Should().BeEquivalentTo("value: 1", "value: 2", "value: 3", "value: 4", "value: 5", "completed");
        }
    }
}
