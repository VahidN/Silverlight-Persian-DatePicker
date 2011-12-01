using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SilverlightPersianDatePicker.Behaviors
{
    //from: http://kentb.blogspot.com/2010/07/popupstaysopen-in-silverlight.html
    public static class Popup
    {
        private static readonly IDictionary<System.Windows.Controls.Primitives.Popup, PopupWatcher> PopupWatcherCache = new Dictionary<System.Windows.Controls.Primitives.Popup, PopupWatcher>();

        public static readonly DependencyProperty StaysOpenProperty = DependencyProperty.RegisterAttached("StaysOpen",
            typeof(bool),
            typeof(Popup),
            new PropertyMetadata(true, onStaysOpenChanged));

        public static bool GetStaysOpen(System.Windows.Controls.Primitives.Popup popup)
        {
            return (bool)popup.GetValue(StaysOpenProperty);
        }

        public static void SetStaysOpen(System.Windows.Controls.Primitives.Popup popup, bool staysOpen)
        {
            popup.SetValue(StaysOpenProperty, staysOpen);
        }

        private static void onStaysOpenChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var popup = dependencyObject as System.Windows.Controls.Primitives.Popup;
            PopupWatcher popupWatcher;
            var staysOpen = (bool)e.NewValue;

            if (popup == null) return;

            PopupWatcherCache.TryGetValue(popup, out popupWatcher);

            if (staysOpen)
            {
                if (popupWatcher != null)
                {
                    popupWatcher.Detach();
                    PopupWatcherCache.Remove(popup);
                }
            }
            else
            {
                if (popupWatcher == null)
                {
                    popupWatcher = new PopupWatcher(popup);
                    PopupWatcherCache[popup] = popupWatcher;
                }

                popupWatcher.Attach();
            }
        }

        private sealed class PopupWatcher
        {
            private readonly System.Windows.Controls.Primitives.Popup _popup;

            public PopupWatcher(System.Windows.Controls.Primitives.Popup popup)
            {
                _popup = popup;
            }

            public void Attach()
            {
                _popup.Opened += onPopupOpened;
                _popup.Closed += onPopupClosed;
            }

            public void Detach()
            {
                _popup.Opened -= onPopupOpened;
                _popup.Closed -= onPopupClosed;
            }

            private void onPopupOpened(object sender, EventArgs e)
            {
                var popupParent = _popup.Parent as UIElement;

                if (popupParent == null)
                {
                    return;
                }

                popupParent.AddHandler(UIElement.MouseLeftButtonDownEvent, (MouseButtonEventHandler)onMouseLeftButtonDown, true);
            }

            private void onPopupClosed(object sender, EventArgs e)
            {
                var popupParent = _popup.Parent as UIElement;

                if (popupParent == null)
                {
                    return;
                }

                popupParent.RemoveHandler(UIElement.MouseLeftButtonDownEvent, (MouseButtonEventHandler)onMouseLeftButtonDown);
            }

            private void onMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                var storyboard = new Storyboard { Duration = TimeSpan.Zero };
                var objectAnimation = new ObjectAnimationUsingKeyFrames { Duration = TimeSpan.Zero };
                objectAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero), Value = false });
                Storyboard.SetTarget(objectAnimation, _popup);
                Storyboard.SetTargetProperty(objectAnimation, new PropertyPath("IsOpen"));
                storyboard.Children.Add(objectAnimation);
                storyboard.Begin();
            }
        }
    }
}
