using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskBots.Model.Control
{
    public class MouseMoveManager
    {
        public static void SetDragAndDrop(UIElement window, UIElement component)
        {
            var mouseDown = Observable.FromEvent<MouseButtonEventHandler, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => component.MouseDown += h,
                h => component.MouseDown -= h);

            var mouseMove = Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                h => (s, e) => h(e),
                h => window.MouseMove += h,
                h => window.MouseMove -= h);

            var mouseUp = Observable.FromEvent<MouseButtonEventHandler, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => window.MouseUp += h,
                h => window.MouseUp -= h);

            var dragOffset = new Point();
            var drag = mouseMove
                .SkipUntil(mouseDown.Do(e => {
                    window.CaptureMouse();
                    dragOffset = e.GetPosition(component);
                }))
                .TakeUntil(mouseUp.Do(_ => window.ReleaseMouseCapture()))
                .Repeat();

            drag.Select(e => e.GetPosition(null))
                .Subscribe(p => {
                    UIElement el = component as UIElement;
                    Canvas.SetLeft(el, p.X - dragOffset.X);
                    Canvas.SetTop(el, p.Y - dragOffset.Y);
                });
        } 
    }
}
