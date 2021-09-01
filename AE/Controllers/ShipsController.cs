using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AE.Data;
using AE.Models;
using Geolocation;
using System.Net.Mime;

namespace AE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly AEContext _context;

        public ShipsController(AEContext context)
        {
            _context = context;
        }

        /// <summary>
        /// See all ships in the system
        /// </summary>
        /// <returns></returns>
        // GET: api/Ships
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ship>>> GetShips()
        {
            return await _context.Ships.ToListAsync();
        }

        // GET: api/Ships/5
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ship>> GetShip(string id)
        {
            var ship = await _context.Ships.FindAsync(id);

            if (ship == null)
            {
                return NotFound();
            }

            return ship;
        }

        // PUT: api/Ships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutShip(string id, Ship ship)
        //{
        //    if (id != ship.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ship).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ShipExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        /// <summary>
        /// Add ships to the system
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        // POST: api/Ships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Ship>> PostShip(Ship ship)
        {
            _context.Ships.Add(ship);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShipExists(ship.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShip", new { id = ship.Id }, ship);
        }

        // DELETE: api/Ships/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteShip(string id)
        //{
        //    var ship = await _context.Ships.FindAsync(id);
        //    if (ship == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Ships.Remove(ship);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        /// <summary>
        /// Update ship velocity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="velocity"></param>
        /// <returns></returns>
        [HttpPut("{id}/Velocity")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutShip(string id, double velocity)
        {
            //if (id != ship.Id)
            //{
            //    return BadRequest();
            //}

            Ship ship = new()
            {
                Id = id,
                Velocity = velocity
            };

            _context.Entry(ship).Property(prop => prop.Velocity).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Get cloest port
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Ships/5/CloestPort
        [HttpGet("{id}/CloestPort")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShipClosestPort>> GetShipClosestPort(string id)
        {
            Ship ship = await _context.Ships.FindAsync(id);

            if (ship == null)
            {
                return NotFound();
            }

            ClosestPort closestPort = (await _context.Ports.ToListAsync())
                .Select(result => new ClosestPort()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Latitude = result.Latitude,
                    Longitude = result.Longitude,
                    Distance = GeoCalculator.GetDistance(ship.Latitude, ship.Longitude, result.Latitude, result.Longitude, 1, DistanceUnit.NauticalMiles),
                })
                .OrderBy(x => x.Distance)
                .FirstOrDefault();

            ShipClosestPort shipClosestPort = new()
            {
                Ship = ship,
                CloestPort = closestPort,
                GenerationTime = DateTimeOffset.UtcNow
            };

            shipClosestPort.CloestPort.EstimatedArrivalTime = shipClosestPort.GenerationTime.AddHours(closestPort.Distance / ship.Velocity);

            return shipClosestPort;
        }

        private bool ShipExists(string id)
        {
            return _context.Ships.Any(e => e.Id == id);
        }
    }
}
