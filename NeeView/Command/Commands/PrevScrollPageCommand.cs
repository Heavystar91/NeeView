using NeeView.Properties;

namespace NeeView
{
    public class PrevScrollPageCommand : CommandElement
    {
        public PrevScrollPageCommand()
        {
            this.Group = TextResources.GetString("CommandGroup.Move");
            this.ShortCutKey = new ShortcutKey("WheelUp");
            this.IsShowMessage = false;
            this.PairPartner = "NextScrollPage";

            this.ParameterSource = new CommandParameterSource(new ScrollPageCommandParameter());
        }

        public override bool CanExecute(object? sender, CommandContext e)
        {
            return !NowLoading.Current.IsDisplayNowLoading;
        }

        public override void Execute(object? sender, CommandContext e)
        {
            var parameter = e.Parameter.Cast<ScrollPageCommandParameter>();

            if (BookSettings.Current.PageMode == PageMode.Webtoon)
            {
                // Webtoon mode treats the loaded panorama as one tall surface and uses
                // a smaller movement per wheel notch for browser-like fine scrolling.
                var pagesAsOne = parameter.PagesAsOne;
                var scrollType = parameter.ScrollType;
                var scroll = parameter.Scroll;
                var lineBreakStopTime = parameter.LineBreakStopTime;
                try
                {
                    parameter.PagesAsOne = true;
                    parameter.ScrollType = NScrollType.Vertical;
                    parameter.Scroll = 0.25;
                    parameter.LineBreakStopTime = 0.0;
                    BookOperation.Current.Control.ScrollToPrevFrame(sender, parameter);
                }
                finally
                {
                    parameter.PagesAsOne = pagesAsOne;
                    parameter.ScrollType = scrollType;
                    parameter.Scroll = scroll;
                    parameter.LineBreakStopTime = lineBreakStopTime;
                }
                return;
            }

            BookOperation.Current.Control.ScrollToPrevFrame(sender, parameter);
        }
    }

}
