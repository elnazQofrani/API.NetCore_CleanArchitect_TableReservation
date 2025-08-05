using AutoMapper;
using Azure.Core;
using BookTableReservation.CustomActionValidation;
using Domain.Entities;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interface;

namespace BookTableReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService _customerService, IMapper mapper)
        {
            this._customerService = _customerService;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CustomerAddDto customerDto)
        {
            var customerList = mapper.Map<Customer>(customerDto);
            try
            {
                var result = await _customerService.CreatAsync(customerList);
                return Ok(new { CustomerId = result.Id });

            }
            catch   (Exception ex)
            {
         
                return StatusCode( 500, ex.Message);
            }

        }

    }
}
