using AutoMapper;
using Store.ApplicationCore.DTOs;
using Store.ApplicationCore.Entities;
using Store.ApplicationCore.Exceptions;
using Store.ApplicationCore.Interfaces;
using Store.ApplicationCore.Utils;
using Store.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Store.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;

        public ProductRepository(StoreContext storeContext, IMapper mapper)
        {
            this._storeContext = storeContext;
            this._mapper = mapper;
        }

        public ProductResponse CreateProduct(CreateProductRequest request)
        {
            var product = this._mapper.Map<Product>(request);
            product.Stock = 0;
            product.CreatedAt = product.UpdatedAt = DateUtil.GetCurrentDate();

            this._storeContext.Products.Add(product);
            this._storeContext.SaveChanges();

            return this._mapper.Map<ProductResponse>(product);
        }

        ProductResponse IProductRepository.UpdateProduct(int productId, UpdateProductRequest request)
        {
            return UpdateProduct(productId, request);
        }

        ProductResponse IProductRepository.GetProductById(int productId)
        {
            return GetProductById(productId);
        }

        public void DeleteProductById(int productId)
        {
            var product = this._storeContext.Products.Find(productId);
            if (product != null)
            {
                this._storeContext.Products.Remove(product);
                this._storeContext.SaveChanges();
            }

            throw new NotFoundException();
        }

        ProductResponse IProductRepository.CreateProduct(CreateProductRequest request)
        {
            return CreateProduct(request);
        }

        List<ProductResponse> IProductRepository.GetProducts()
        {
            return GetProducts();
        }

        public ProductResponse GetProductById(int productId)
        {
            var product = this._storeContext.Products.Find(productId);
            if (product != null)
            {
                return this._mapper.Map<ProductResponse>(product);
            }

            throw new NotFoundException();
        }

        public List<ProductResponse> GetProducts()
        {
            return this._storeContext.Products.Select(p => this._mapper.Map<ProductResponse>(p)).ToList();
        }

        public ProductResponse UpdateProduct(int productId, UpdateProductRequest request)
        {
            var product = this._storeContext.Products.Find(productId);
            if (product != null)
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Stock = request.Stock;
                product.UpdatedAt = DateUtil.GetCurrentDate();

                this._storeContext.Products.Update(product);
                this._storeContext.SaveChanges();

                return this._mapper.Map<ProductResponse>(product);
            }

            throw new NotFoundException();
        }
    }
}