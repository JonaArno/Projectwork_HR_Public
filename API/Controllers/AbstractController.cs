using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AbstractController : ControllerBase
    {
        public IActionResult OkOrNotFound<T>(T entity)
        {
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        public IActionResult OkOrBadRequest<T>(T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }
            return Ok(entity);
        }
        public IActionResult NoContentOrBadRequest<T>(T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }
            return NoContent();
        }
        public IActionResult CreatedOrBadRequest<T>(string location, T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            return Created(location, entity);
        }
    }
}
