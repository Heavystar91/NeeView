using NeeView.ComponentModel;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace NeeView.PageFrames
{
    public partial class PageFrameBox
    {
        private DateTime _webtoonLastScrollTime = DateTime.MinValue;

        public void ScrollWebtoon(LinkedListDirection direction)
        {
            if (_disposedValue) return;
            if (!_bookContext.IsEnabled) return;

            var contentRect = CreatePanoramaContentRect();
            var viewRect = _transformControlFactory.CreateViewRect(_viewBox.Rect);

            var parameter = new ScrollPageCommandParameter
            {
                PagesAsOne = true,
                ScrollType = NScrollType.Vertical,
                Scroll = 0.70,
                EndMargin = 0.0,
                LineBreakStopTime = 0.0,
            };

            var math = new NScroll(_context, contentRect, viewRect);
            var scroll = math.ScrollN(direction.ToSign(), parameter, 0.0);
            if (scroll.IsTerminated) return;

            // Accumulate against the destination point, not the current visual point.
            // This lets rapid wheel input extend one continuous glide instead of
            // starting a sequence of independent page-scroll hops.
            var target = _scrollViewer.Point + scroll.Vector;

            var now = DateTime.UtcNow;
            var rapid = (now - _webtoonLastScrollTime).TotalMilliseconds < 220.0;
            _webtoonLastScrollTime = now;

            IEasingFunction ease = rapid
                ? new LinerEase()
                : new CubicEase { EasingMode = EasingMode.EaseInOut };

            _scrollViewer.SetPoint(target, TimeSpan.FromMilliseconds(220), ease, ease);

            _selected.SetAuto();
            AssertSelectedExists();
            ResetSnapAnchor();
        }
    }
}
