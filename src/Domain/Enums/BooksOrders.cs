namespace Domain.Enums
{
    /// <summary>
    /// Tipos de ordenações possíveis para a listagem dos livros.
    /// </summary>
    public enum BooksOrders
    {
        /// <summary>
        /// Ordernar lista de livros do maior preço para o menor.
        /// </summary>
        ByPriceDesc = 1,

        /// <summary>
        /// Ordernar lista de livros do menor preço para o maior.
        /// </summary>
        ByPriceAsc = 2
    }
}
