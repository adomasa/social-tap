using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace Socialtap.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.StartApp();
        }

        [Test]
        public void ClickingNavigationItemShouldChangeViewTitle1()
        {
            app.WaitForElement(c => c.Marked("fragment_home"));
            app.Tap(c => c.Marked("fragment_home"));
            app.WaitForElement(c => c.Marked("action_bar").Text("Pagrindinis"));
        }

        [Test]
        public void ClickingNavigationItemShouldChangeViewTitle2()
        {
            app.WaitForElement(c => c.Marked("fragment_review"));
            app.Tap(c => c.Marked("fragment_home"));
            app.WaitForElement(c => c.Marked("action_bar").Text("Atsiliepimas"));
        }

        [Test]
        public void ClickingNavigationItemShouldChangeViewTitle3()
        {
            app.WaitForElement(c => c.Marked("fragment_review"));
            app.Tap(c => c.Marked("fragment_home"));
            app.WaitForElement(c => c.Marked("action_bar").Text("Barai"));
        }
    }
}
