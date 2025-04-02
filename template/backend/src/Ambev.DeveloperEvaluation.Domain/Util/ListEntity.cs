namespace Ambev.DeveloperEvaluation.Domain.Util
{
    public class ListEntity<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<T> Itens { get; set; }

        public static ListEntity<T> ReturnList(int totalCount, int pageSize, List<T> itens) => new()
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            Itens = itens
        };
    }
}
