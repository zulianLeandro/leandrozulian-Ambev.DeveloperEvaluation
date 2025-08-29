using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        [Fact]
        public void CreateSaleItem_WithQuantityBelowFour_ShouldNotApplyDiscount()
        {
            // Arrange
            var quantity = 3;
            var unitPrice = 100.00m;

            // Act
            var saleItem = new SaleItem("product_id_1", quantity, unitPrice);

            // Assert
            Assert.Equal(0, saleItem.Discount);
            Assert.Equal(quantity * unitPrice, saleItem.TotalItemAmount);
        }

        [Fact]
        public void CreateSaleItem_WithQuantityBetweenFourAndNine_ShouldApplyTenPercentDiscount()
        {
            // Arrange
            var quantity = 5;
            var unitPrice = 100.00m;
            var expectedDiscount = 5 * 100.00m * 0.10m;

            // Act
            var saleItem = new SaleItem("product_id_2", quantity, unitPrice);

            // Assert
            Assert.Equal(expectedDiscount, saleItem.Discount);
            Assert.Equal(500.00m - expectedDiscount, saleItem.TotalItemAmount);
        }

        [Fact]
        public void CreateSaleItem_WithQuantityBetweenTenAndTwenty_ShouldApplyTwentyPercentDiscount()
        {
            // Arrange
            var quantity = 15;
            var unitPrice = 100.00m;
            var expectedDiscount = 15 * 100.00m * 0.20m;

            // Act
            var saleItem = new SaleItem("product_id_3", quantity, unitPrice);

            // Assert
            Assert.Equal(expectedDiscount, saleItem.Discount);
            Assert.Equal(1500.00m - expectedDiscount, saleItem.TotalItemAmount);
        }

        [Fact]
        public void CreateSaleItem_WithQuantityAboveTwenty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var quantity = 21;
            var unitPrice = 100.00m;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new SaleItem("product_id_4", quantity, unitPrice));
        }

        [Fact]
        public void CancelSaleItem_ShouldSetIsItemCancelledToTrue()
        {
            // Arrange
            var saleItem = new SaleItem("product_id_5", 1, 10m);

            // Act
            saleItem.Cancel();

            // Assert
            Assert.True(saleItem.IsItemCancelled);
        }
    }
}
