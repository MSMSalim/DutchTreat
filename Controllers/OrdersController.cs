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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository dutchRepository,
                                ILogger<OrdersController> logger,
                                IMapper mapper,
                                UserManager<StoreUser> userManager)
        {
            this._dutchRepository = dutchRepository;
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            var userName = User.Identity.Name;

            var orders = _dutchRepository.GetAllOrdersByUser(userName, includeItems);

            var mappedOrders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

            return Ok(mappedOrders);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = _dutchRepository.GetOrderById(User.Identity.Name, id);

            if(order == null)
            {
                return NotFound();
            }

            OrderViewModel ovm = _mapper.Map<Order, OrderViewModel>(order);

            return Ok(ovm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel model)
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

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            newOrder.User = currentUser;

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
