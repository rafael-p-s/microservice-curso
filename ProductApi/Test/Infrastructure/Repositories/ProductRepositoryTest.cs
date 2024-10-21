namespace Test.Infrastructure.Repositories;

public class ProductRepositoryTest
{
    private ProductApiDbContext _productApiDbContext { get; }
    private ProductRepository _productRepository { get; }

    public ProductRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<ProductApiDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging(true)
            .Options;

        _productApiDbContext = new ProductApiDbContext(options);
        _productRepository = new ProductRepository(_productApiDbContext);
    }

    [Fact]
    public async Task Dispose_ReturnDisposeInstance_WhenDisposeIsCalledAsync()
    {
        // Arrange
        // no arrange needed for this test

        // Act
        _productRepository.Dispose();

        var result = await Record.ExceptionAsync(_productRepository.GetAll);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectDisposedException>();
    }


    [Fact]
    public void Dispose_ReturnDispose_WhenDisposeIsCalledTwice()
    {
        // Arrange
        // no arrange needed for this test

        // Act
        _productRepository.Dispose();

        var result = _productRepository.Dispose;

        // Assert
        result.Should().NotBeNull();
        result.Should().Throw<ObjectDisposedException>();
    }

    [Fact]
    public async Task GellAll_ShouldReturnAll_WhenThereAreDataIntoDatabase()
    {
        // Arrange
        var products = ProductFaker.ProductList();

        _productApiDbContext.Products.AddRange(products);
        _productApiDbContext.SaveChanges();

        // Act
        var result = await _productRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
        result.Should().BeOfType<List<Product>>();
        result.Should().BeEquivalentTo(products);
    }

}
