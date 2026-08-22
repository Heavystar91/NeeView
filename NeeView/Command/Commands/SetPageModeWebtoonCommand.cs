using NeeView.Properties;
using System.Windows.Data;

namespace NeeView
{
    public class SetPageModeWebtoonCommand : CommandElement
    {
        public SetPageModeWebtoonCommand()
        {
            this.Group = TextResources.GetString("CommandGroup.PageSetting");
            this.ShortCutKey = new ShortcutKey("Ctrl+3");
            this.IsShowMessage = true;
        }

        public override BindingBase CreateIsCheckedBinding()
        {
            return BindingGenerator.PageMode(PageMode.Webtoon);
        }

        public override bool CanExecute(object? sender, CommandContext e)
        {
            return BookSettings.Current.CanEdit;
        }

        public override void Execute(object? sender, CommandContext e)
        {
            BookSettings.Current.SetPageMode(PageMode.Webtoon);
        }
    }
}
