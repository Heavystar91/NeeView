namespace NeeView.UnitTest
{
    public class PageModeTests
    {
        [Fact]
        public void ToggleCyclesThroughWebtoon()
        {
            Assert.Equal(PageMode.WidePage, PageMode.SinglePage.GetToggle(+1, true));
            Assert.Equal(PageMode.Webtoon, PageMode.WidePage.GetToggle(+1, true));
            Assert.Equal(PageMode.SinglePage, PageMode.Webtoon.GetToggle(+1, true));

            Assert.Equal(PageMode.WidePage, PageMode.Webtoon.GetToggle(-1, true));
            Assert.Equal(PageMode.SinglePage, PageMode.WidePage.GetToggle(-1, true));
            Assert.Equal(PageMode.Webtoon, PageMode.SinglePage.GetToggle(-1, true));
        }

        [Fact]
        public void ToggleClampsWhenLoopIsDisabled()
        {
            Assert.Equal(PageMode.SinglePage, PageMode.SinglePage.GetToggle(-1, false));
            Assert.Equal(PageMode.Webtoon, PageMode.Webtoon.GetToggle(+1, false));
        }

        [Fact]
        public void ValidateAcceptsWebtoonAndClampsUnknownValues()
        {
            Assert.Equal(PageMode.Webtoon, PageMode.Webtoon.Validate());
            Assert.Equal(PageMode.SinglePage, ((PageMode)(-1)).Validate());
            Assert.Equal(PageMode.Webtoon, ((PageMode)999).Validate());
        }
    }
}
