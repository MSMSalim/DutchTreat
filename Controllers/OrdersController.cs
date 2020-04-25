﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _dutchRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IDutchRepository dutchRepository,
                                ILogger<OrdersController> logger)
        {
            this._dutchRepository = dutchRepository;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _dutchRepository.GetAllOrders();

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = _dutchRepository.GetOrderById(id);

            if(order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}