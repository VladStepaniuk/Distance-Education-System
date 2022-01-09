using DESystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Controllers
{
    [Route("api/insitution")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route ("list")]
        public async Task<IActionResult> GetIstitutions()
        {
            return Ok(await unitOfWork.InstitutionRepository.Get());
        }
    }
}
