using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Views.Controls
{
    public class RoundedCornerFrame : Frame
    {
        /*public static readonly BindableProperty RoundedCornersProperty =
            BindableProperty.Create(nameof(RoundedCorners), typeof(Corner), typeof(RoundedCornerFrame), Corner.All);

        public Corner RoundedCorners
        {
            get { return (Corner)GetValue(RoundedCornersProperty); }
            set { SetValue(RoundedCornersProperty, value); }
        }*/
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(RoundedCornerFrame), typeof(CornerRadius), typeof(RoundedCornerFrame));
        public RoundedCornerFrame()
        {
            // MK Clearing default values (e.g. on iOS it's 5)
            base.CornerRadius = 0;
        }

        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
    /*public enum Corner
    {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }*/
}
