using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TipCalc.Core.Services;

namespace TipCalc.Core.ViewModels
{
    public class DaMonkeyDetailsModel : MvxViewModel<string, Monkey>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMonkeyService _monkeyService;

        private Monkey _DaMonkey;
        public IMvxCommand CommandNavigateBack { get; private set; }

        public Monkey DaMonkey
        {
            get => _DaMonkey;
            set
            {
                _DaMonkey = value;
                RaisePropertyChanged(() => DaMonkey);
            }
        }
        public DaMonkeyDetailsModel(IMvxNavigationService navigationService, IMonkeyService monkeyService)
        {
            _navigationService = navigationService;
            _monkeyService = monkeyService;
            CommandNavigateBack = new MvxAsyncCommand(CommandNavigateBackFunction);
        }

        private async Task CommandNavigateBackFunction()
        {
            await _navigationService.Close(this, _DaMonkey);
        }

        public override Task Initialize()
        {
            // Async initialization, YEY!


            return base.Initialize();

        }
        public override void Prepare(string daMonkey)
        {
            foreach (var monkey in _monkeyService.GetMonkeys())
            {
                if (monkey.Name == daMonkey)
                {
                    DaMonkey = monkey;
                }

            }
        }
    }
}
