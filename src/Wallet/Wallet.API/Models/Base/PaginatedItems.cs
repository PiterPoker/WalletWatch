namespace Wallet.API.Models.Base
{
    public record PaginatedItems<TEntity>
        where TEntity : class
    {
        public List<TEntity> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public bool HasMoreItems { get; private set; }

        public PaginatedItems(List<TEntity> items, int pageIndex, int pageSize, bool hasMoreItems)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            HasMoreItems = hasMoreItems;
        }
    }
}
