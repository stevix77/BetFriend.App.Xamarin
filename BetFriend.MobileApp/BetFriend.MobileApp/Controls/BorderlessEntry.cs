using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BetFriend.MobileApp.Controls
{
    /// <summary>
    /// This class is inherited from Xamarin.Forms.Entry to remove the border for Entry control in the Android platform.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class BorderlessEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty =
      BindableProperty.Create(nameof(BorderColor),
          typeof(Color), typeof(BorderlessEntry), Color.Gray);
        // Gets or sets BorderColor value  
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }


        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
        BindableProperty.Create(nameof(IsCurvedCornersEnabled),
            typeof(bool), typeof(BorderlessEntry), true);
        // Gets or sets IsCurvedCornersEnabled value  
        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }
    }
}