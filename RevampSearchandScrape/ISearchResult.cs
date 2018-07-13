using System;
namespace RevampSearchandScrape
{
    public interface ISearchResult: IElement, IWriteListToList, IListFilter, IWriteToList, IGoToElement
    {
    }
}
