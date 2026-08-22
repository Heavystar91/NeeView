using NeeView.Properties;

namespace NeeView
{
    public class NextScrollPageCommand : CommandElement
    {
        public NextScrollPageCommand()
        {
            this.Group = TextResources.GetString("CommandGroup.Move");
            this.ShortCutKey = new ShortcutKey("WheelDown");
            this.IsShowMessage = false;
            this.PairPartner = "PrevScrollPage";

            // PrevScrollPage
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
                // Webtoon mode treats the loaded panorama as one tall surface so the
                // mouse wheel scrolls through page boundaries instead of turning pages.
                var pagesAsOne = parameter.PagesAsOne;
                var scrollType = parameter.ScrollType;
                try
                {
                    parameter.PagesAsOne = true;
                    parameter.ScrollType = NScrollType.Vertical;
                    BookOperation.Current.Control.ScrollToNextFrame(sender, parameter);
                }
                finally
                {
                    parameter.PagesAsOne = pagesAsOne;
                    parameter.ScrollType = scrollType;
                }
                return;
            }

            BookOperation.Current.Control.ScrollToNextFrame(sender, parameter);
        }
    }


}
