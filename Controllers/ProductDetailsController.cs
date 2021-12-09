using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repositories;
using WebApi.Dtos;
using System.Threading.Tasks;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("productsDetails")]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailsRepository productDetailsRepository;

        public ProductDetailsController(IProductDetailsRepository productDetailsRepository)
        {
            this.productDetailsRepository = productDetailsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDetailsDto>> GetAllAsync()
        {
            var productDetails = (await productDetailsRepository.GetAllAsync()).Select(p => p.AsDetailDto());
            return productDetails;
        }

        [HttpGet("{id}")]

        public async Task<IEnumerable<ProductDetailsDto>> GetAsync(Guid id)
        {
            var productDetails = (await productDetailsRepository.GetByIdAsync(id)).Select(p => p.AsDetailDto());
            
            return productDetails;
        }
    }
}