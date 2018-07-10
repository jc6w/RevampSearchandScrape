using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{//MAKE ABSTRACT
    public abstract class WebElement
    {
        private List<string> list;
        List<List<string>> list2;

        static ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        IWebDriver driver;

        WebElement(IWebDriver dr, IWebElement we)
        {
            driver = dr;
            element = we;
        }

        public abstract void FindList();

        bool IsElementPresent(By by)
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

        bool IsElementPresent(IWebElement el, By s)
        {
            try
            {
                el.FindElement(s);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public void AddToList(IWebElement e, By s)
        {
            IWebElement temp;
            string text = "";
            if (IsElementPresent(e, s))
            {
                temp = e.FindElement(s);
                if (string.IsNullOrEmpty(temp.Text))
                {
                    text = temp.GetAttribute("innerHTML");
                }
                else
                {
                    text = temp.Text;
                }
                text.TrimEnd('\0');
                text.TrimEnd(' ');
                list.Add(text);
            }
            else
            {
                list.Add(null);
            }
        }

        public void AddListToList()
        {
            list2.Add(list);
        }
    }
}
