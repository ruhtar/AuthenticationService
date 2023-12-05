﻿using ByteStore.Application.Services;
using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using Moq;
using Xunit;

namespace ByteStore.Tests.Application;

public class ProductServiceTests
{
    [Theory]
    [InlineData(0, 5)]
    [InlineData(1, 5)]
    [InlineData(2, 3)]
    public async Task GetAllProducts_ReturnsAListOfProducts(int pageIndex, int pageSize)
    {
        //Arrange
        var productRepo = new Mock<IProductRepository>();
        
        var input = new GetProductsInputPagination
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        productRepo.Setup(repo => repo.GetAllProducts(input))!
            .ReturnsAsync(Utils.GetProductsMock());


        var productService = new ProductService(productRepo.Object);
        //Act
        var result = await productService.GetAllProducts(input);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(Utils.GetProductsMock().Count, result.Count);

        for (var i = 0; i < Utils.GetProductsMock().Count; i++)
        {
            Assert.Equal(Utils.GetProductsMock()[i].ProductId, result[i]?.ProductId);
            Assert.Equal(Utils.GetProductsMock()[i].Name, result[i]?.Name);
            Assert.Equal(Utils.GetProductsMock()[i].Price, result[i]?.Price);
        }
    }

    [Fact]
    public async Task GetProductById_ReturnsAProduct()
    {
        //Arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];
        productRepo.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);

        var productService = new ProductService(productRepo.Object);

        //Act
        var result = await productService.GetProductById(product.ProductId);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(product, result);
    }
    
    [Fact]
    public async Task GetProductById_ReturnsNull()
    {
        //Arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];
        var wrongId = product.ProductId + 1;
        productRepo.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);

        var productService = new ProductService(productRepo.Object);

        //Act
        var result = await productService.GetProductById(wrongId);
        
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddProduct_ShouldReturnAddedProduct()
    {
        //arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];
        
        productRepo.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);

        var productService = new ProductService(productRepo.Object);
        
        //Act
        var productDto = new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            ProductQuantity = product.ProductQuantity,
            ImageStorageUrl = product.ImageStorageUrl,
            Description = product.Description
        };
        var result = await productService.AddProduct(productDto);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.Name, result.Name);
        Assert.Equal(productDto.ProductQuantity, result.ProductQuantity);
        Assert.Equal(productDto.Price, result.Price);
        Assert.Equal(productDto.ImageStorageUrl, result.ImageStorageUrl);
        Assert.Equal(productDto.Description, result.Description);

        productRepo.Verify(repo => repo.AddProduct(It.IsAny<Product>()), Times.Once);
    }
}