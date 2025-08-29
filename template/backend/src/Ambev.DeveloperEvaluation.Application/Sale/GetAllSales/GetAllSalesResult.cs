namespace Ambev.DeveloperEvaluation.Application.Sale.GetAllSales
{
    public class GetAllSalesResult
    {
        public Guid SaleId { get; set; }
        public int SaleNumber { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
    }
}