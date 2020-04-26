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

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/orders/{orderid:int}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _dutchRepository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository dutchRepository,
                                ILogger<OrderItemsController> logger,
                                IMapper mapper)
        {
            this._dutchRepository = dutchRepository;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderid)
        {
            var order = this._dutchRepository.GetOrderById(User.Identity.Name, orderid);

            if(order == null)
            {
                return NotFound();
            }

            var orderItems = _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items);

            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderid, int id)
        {
            var order = this._dutchRepository.GetOrderById(User.Identity.Name, orderid);

            if (order == null)
            {
                return NotFound();
            }

            var orderItem = order.Items.Where(i => i.Id == id).FirstOrDefault();

            var orderItemVM = _mapper.Map<OrderItem, OrderItemViewModel>(orderItem);

            return Ok(orderItemVM);
        }
    }
}
