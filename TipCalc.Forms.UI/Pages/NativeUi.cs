using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Views;
using TipCalc.Core.ViewModels;
using Xamarin.Forms;

namespace TipCalc.Forms.UI.Pages
{
    public class NativeUi : MvxContentPage<TipViewModel>
    {
        private Label TipLabel;
        private Slider GenerositySlider;
        private Entry SubTotalEntry;
        private Button showList;
        private Label monkeyLabel;
        private Label MonkeyReport;
        public NativeUi()
        {
            StackLayout stackLayout = new StackLayout();
            Label Subtotal = new Label
            {
                Text = "Subtotal"
            };
            stackLayout.Children.Add(Subtotal);
            SubTotalEntry = new Entry()
            {
                Keyboard = Keyboard.Numeric,
            };
            stackLayout.Children.Add(SubTotalEntry);
            Label Generosity = new Label
            {
                Text = "Generosity"
            };
            stackLayout.Children.Add(Generosity);
            GenerositySlider = new Slider()
            {
                Maximum = 100
            };
            stackLayout.Children.Add(GenerositySlider);
            TipLabel = new Label
            {
                Text = "xx"
            };
            stackLayout.Children.Add(TipLabel);

            showList = new Button()
            {
                Text = "show List"
            };
            stackLayout.Children.Add(showList);

            monkeyLabel = new Label()
            {
                Text = "monkey Selection"
            };
            stackLayout.Children.Add(monkeyLabel);
            MonkeyReport = new Label
            {
                Text = "-----"
               ,
                HorizontalTextAlignment = TextAlignment.Center
            };
            stackLayout.Children.Add(MonkeyReport);
            Content = stackLayout;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var set = this.CreateBindingSet<NativeUi, TipViewModel>();
            set.Bind(SubTotalEntry).For(v => v.Text).To(vm => vm.SubTotal).TwoWay();
            set.Bind(GenerositySlider).For(v => v.Value).To(vm => vm.Generosity).TwoWay();
            set.Bind(showList).For("Clicked").To(vm => vm.CommandShowList);
            set.Bind(monkeyLabel).For("Clicked").To(vm => vm.CommandCheckLeak);
            set.Bind(MonkeyReport).For(v => v.Text).To(vm => vm.MonkeySelection).OneWay();

            set.Apply();

        }
    }
}
