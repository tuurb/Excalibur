using System;
using System.Reactive.Linq;
using FluentAssertions;
using ReactiveUI;
using Xunit;

namespace Excalibur.Tests
{
    public class ViewModelTests
    {
        public class FirstViewModel : ReactiveObject
        {
            private int _input;
            private int _firstOutput;
            private readonly ObservableAsPropertyHelper<int> _secondOutput;

            public int Input
            {
                get => _input;
                set => this.RaiseAndSetIfChanged(ref _input, value);
            }

            public int FirstOutput
            {
                get => _firstOutput;
                set => this.RaiseAndSetIfChanged(ref _firstOutput, value);
            }

            public int SecondOutput => _secondOutput.Value;


            public FirstViewModel()
            {
                // Elke keer wanneer de Input property verandert
                // en Input is deelbaar door 2
                // dan wordt de Output property op 2 * Input gezet.
                this.WhenAnyValue(vm => vm.Input)
                    .Where(x => x % 2 == 0)
                    .Do(x => FirstOutput = x * 2)
                    .Subscribe();

                // Dit doet precies hetzelfde als bovenstaande regel
                // en is te gebruiken voor ReadOnly properties.
                _secondOutput = this.WhenAnyValue(vm => vm.Input)
                    .Where(x => x % 2 == 0)
                    .Select(x => x * 2)
                    .ToProperty(this, vm => vm.SecondOutput);

                Input = 0;
            }
        }

        [Fact]
        public void FirstViewModelTests()
        {
            var viewModel = new FirstViewModel();
            
            viewModel.Input.Should().Be(0);
            viewModel.FirstOutput.Should().Be(0);
            viewModel.SecondOutput.Should().Be(0);

            viewModel.Input = 1;
            viewModel.FirstOutput.Should().Be(0);
            viewModel.SecondOutput.Should().Be(0);

            viewModel.Input = 2;
            viewModel.FirstOutput.Should().Be(4);
            viewModel.SecondOutput.Should().Be(4);

            viewModel.Input = 3;
            viewModel.FirstOutput.Should().Be(4);
            viewModel.SecondOutput.Should().Be(4);

            viewModel.Input = 4;
            viewModel.FirstOutput.Should().Be(8);
            viewModel.SecondOutput.Should().Be(8);
        }


        public class SecondViewModel : ReactiveObject
        {
            private string _firstName;
            private string _lastName;
            private readonly ObservableAsPropertyHelper<string> _fullName;

            public string FirstName
            {
                get => _firstName;
                set => this.RaiseAndSetIfChanged(ref _firstName, value);
            }

            public string LastName
            {
                get => _lastName;
                set => this.RaiseAndSetIfChanged(ref _lastName, value);
            }

            public string FullName => _fullName.Value;

            public SecondViewModel()
            {
                // Wanneer FirstName of LastName wordt aangepast, wordt FullName opnieuw gezet.
                _fullName = this.WhenAnyValue(vm => vm.FirstName, vm => vm.LastName)
                    .Select(_ => $"{FirstName} {LastName}".Trim())
                    .ToProperty(this, vm => vm.FullName);
            }
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("First", "", "First")]
        [InlineData("", "Last", "Last")]
        [InlineData("First", "Last", "First Last")]
        public void SecondViewModelTests(string firstName, string lastName, string expectedFullName)
        {
            var viewModel = new SecondViewModel();
            
            viewModel.FirstName = firstName;
            viewModel.LastName = lastName;

            viewModel.FullName.Should().Be(expectedFullName);
        }
    }
}
