using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService_CRUD.Data;
using GrpcService_CRUD.Models;
using GrpcService_CRUD.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcService_CRUD.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext appContext)
        {
            _context = appContext;
        }

        public override async Task<Protos.Product> GetProduct(GetProductRequest request, ServerCallContext context)
        {

            var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (productEntity == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Requested Product Not Found"));
            }
            else

            {

                var productProto = new Protos.Product
                {
                    Id = productEntity.Id,
                    Name = productEntity.Name,
                    Describtion = productEntity.Describtion,
                    Price = productEntity.Price ?? 0.0
                };

                return productProto;
            }
        }
        public override async Task<ProductListResponse> ListProduct(Empty request, ServerCallContext context)
        {
            var products=await _context.Products.ToListAsync();
            
            var response = new ProductListResponse();

            foreach (var product in products)
            {
                response.Products.Add(new Protos.Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Describtion = product.Describtion,
                    Price = product.Price ?? 0.0
                });
            }

            return await Task.FromResult( response);
        }

        public override  async Task<Protos.Product> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            // Map the gRPC CreateProductRequest to the database Product model
            var dbProduct = new GrpcService_CRUD.Models.Product
            {
                Name = request.Name,
                Price = request.Price,
                Describtion = request.Describtion
            };

            // Add to database
            _context.Products.Add(dbProduct);
            await _context.SaveChangesAsync();

            var response = new Protos.Product
            {
                Id = dbProduct.Id, 
                Name = dbProduct.Name,
                Price = dbProduct.Price??0.0,
                Describtion = dbProduct.Describtion
            };

            return await Task.FromResult(response);
        }
        public override async Task<Protos.Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var productEntity =await _context.Products.FirstOrDefaultAsync(x=>x.Id==request.Id);
            if (productEntity == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Requested Product Not Found"));
            }
            productEntity.Name=request.Name;
            productEntity.Price=request.Price;  
            productEntity.Describtion=request.Describtion;
            _context.Products.Update(productEntity);
            _context.SaveChanges();
            var response = new Protos.Product
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price ?? 0.0,
                Describtion = productEntity.Describtion
            };

            return await Task.FromResult(response);
        }
        public override async Task<Empty> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (productEntity == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Requested Product Not Found"));
            }
            _context.Products.Remove(productEntity);
            _context.SaveChanges();
            var response = new Protos.Product
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price ?? 0.0,
                Describtion = productEntity.Describtion
            };

            return await Task.FromResult(new Empty());
        }
    }
}

