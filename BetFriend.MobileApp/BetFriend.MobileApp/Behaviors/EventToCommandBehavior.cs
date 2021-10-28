using System.Windows.Input;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Behaviors
{
    public class EventToCommandBehavior : BaseBehavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            // Abonnement à ItemTapped
            bindable.ItemTapped += Bindable_ItemSelected;
            bindable.ItemSelected += Bindable_ItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            // Désabonnement à ItemTapped (très important sans ça vous risquez de créer des fuites mémoire).
            bindable.ItemTapped -= Bindable_ItemSelected;
            bindable.ItemSelected -= Bindable_ItemSelected;
        }

        private void Bindable_ItemSelected<T>(object sender, T e)
        {
            var cmd = Command;

            if (cmd != null && cmd.CanExecute(null))
            {
                cmd.Execute(e);
            }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                nameof(Command),
                typeof(ICommand),
                typeof(EventToCommandBehavior),
                null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}
