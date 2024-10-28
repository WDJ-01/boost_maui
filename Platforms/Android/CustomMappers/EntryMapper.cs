using Android.Content;
using Android.Graphics.Drawables;
using Boost.Views.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Boost.Platforms.Android;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Boost.Platforms.Android.CustomMappers
{
    public static class EntryMapper
    {
       public static void Map(IElementHandler handler, IElement view)
        {
            if(view is CustomEntry)
            {
                var casted = (EntryHandler)handler;
                var viewData = (CustomEntry)view;

                var gd = new GradientDrawable();

                gd.SetCornerRadius((int)handler.MauiContext?.Context.ToPixels(viewData.CornerRadius));
                gd.SetStroke((int)handler.MauiContext?.Context.ToPixels(viewData.BorderThickness), viewData.BorderColor.ToAndroid());
                //gd.SetColor(viewData.BackgroundColor.ToAndroid());

                casted.PlatformView?.SetBackground(gd);

                var padTop = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Top);
                var padBottom = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Bottom);
                var padRight = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Right);
                var padLeft = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Left);

                casted.PlatformView?.SetPadding(padLeft, padTop, padRight, padBottom);

            }
        }
    }

}
