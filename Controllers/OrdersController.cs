using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _dutchRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository dutchRepository,
                                ILogger<OrdersController> logger,
                                IMapper mapper)
        {
            this._dutchRepository = dutchRepository;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            var orders = _dutchRepository.GetAllOrders(includeItems);

            var mappedOrders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

            return Ok(mappedOrders);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = _dutchRepository.GetOrderById(id);

            if(order == null)
            {
                return NotFound();
            }

            OrderViewModel ovm = _mapper.Map<Order, OrderViewModel>(order);

            return Ok(ovm);
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*
            var newOrder = new Order()
            {
               OrderDate = model.OrderDate,
               OrderNumber = model.OrderNumber,
               Id = model.OrderId
            };
            */

            var newOrder = _mapper.Map<OrderViewModel, Order>(model);

            if(newOrder.OrderDate == DateTime.MinValue)
            {
                newOrder.OrderDate = DateTime.Now;
            }

            _dutchRepository.AddEntity(newOrder);
            bool isSaved = _dutchRepository.SaveAll();

            if(!isSaved)
            {
                return BadRequest("Failed to save new order");
            }

            /*
            var viewModel = new OrderViewModel()
            {
                OrderId = newOrder.Id,
                OrderDate = newOrder.OrderDate,
                OrderNumber = newOrder.OrderNumber
            };
            */

            var viewModel = _mapper.Map<Order, OrderViewModel>(newOrder);

            return Created($"/api/orders/{viewModel.OrderId}", viewModel);
        }
    }
}
