using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheaplay.Models;
using Cheaplay.Data;

namespace Cheaplay.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreRepository _storeRepository = new StoreRepository();
        [HttpGet("all")]
        public ActionResult<List<Store>> GetAll() => _storeRepository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Store> GetById(int id) => _storeRepository.GetById(id);
    }
}
