using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public abstract class ElementPresent
    {
        public virtual bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public virtual bool IsElementPresent(IWebElement el, By by)
        {
            try
            {
                el.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }
    }
}
