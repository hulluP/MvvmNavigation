using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TipCalc.Core.Services;

namespace TipCalc.Core.ViewModels
{
    public class TipViewModel : MvxViewModel
    {
        private readonly ICalculationService _calculationService;
        public MvxAsyncCommand CommandShowList { get; private set; }
        public MvxAsyncCommand CommandCheckLeak { get; private set; }
        private readonly IMvxNavigationService _navigationService;
        private readonly IMonkeyService _monkeyService;
        public TipViewModel(IMvxNavigationService navigationService, ICalculationService calculationService, IMonkeyService monkeyService)
        {
            _calculationService = calculationService;
            CommandShowList = new MvxAsyncCommand(asyc => CommandShowListFunction());
            _navigationService = navigationService;
            _monkeyService = monkeyService;

        }
        private string myMonkeySelection;
        public string MonkeySelection
        {
            get => myMonkeySelection;
            set
            {
                myMonkeySelection = value;
                RaisePropertyChanged(() => MonkeySelection);
            }
        }

        private async Task CommandShowListFunction()
        {

            Monkey leakyMonkey = _monkeyService.GetMonkeys().First();
            var monkeySelection = await _navigationService.Navigate<MonkeysViewModel, Monkey, Monkey>(leakyMonkey);
            if (monkeySelection != null)
            {
                MonkeySelection = monkeySelection.Name;
            }
            else
            {
                MonkeySelection = "no Monkey";
            }
            //await _navigationService.Navigate<MonkeysViewModel>();

        }

        public override async Task Initialize()
        {
            await base.Initialize();

            SubTotal = 100;
            Generosity = 10;
            Recalcuate();
        }

        private double _subTotal;
        public double SubTotal
        {
            get => _subTotal;
            set
            {
                _subTotal = value;
                RaisePropertyChanged(() => SubTotal);

                Recalcuate();
            }
        }

        private int _generosity;
        public int Generosity
        {
            get => _generosity;
            set
            {
                _generosity = value;
                RaisePropertyChanged(() => Generosity);

                Recalcuate();
            }
        }

        private double _tip;

        public double Tip
        {
            get => _tip;
            set
            {
                _tip = value;
                RaisePropertyChanged(() => Tip);
            }
        }

        private void Recalcuate()
        {
            Tip = _calculationService.TipAmount(SubTotal, Generosity);
        }
    }
}
