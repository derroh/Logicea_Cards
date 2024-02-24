using Logicea_Cards.Entity;
using Microsoft.AspNetCore.Mvc;
using Logicea_Cards.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.IdentityModel.Tokens;

namespace Logicea_Cards.Controllers
{
    [ApiController]
    [Route("api/cards")]
    [Authorize]
    public class CardsController : Controller
    {
        IRepository<Card> _cards;
        public CardsController(IRepository<Card> card)
        {
            _cards = card;
        }
        [HttpGet]
        public async Task<IEnumerable<Card>> Get()
        {
            string role = User.FindFirstValue(ClaimTypes.Role);
            string name = User.FindFirstValue(ClaimTypes.Name);

            if (role == "Member")
                return await _cards.ReadAllAsync(x => x.UserEmail == name);

            return await _cards.ReadAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            string role = User.FindFirstValue(ClaimTypes.Role);
            string name = User.FindFirstValue(ClaimTypes.Name);

            if (role == "Admin")
                Ok(await _cards.ReadAllAsync(x => x.Id == Convert.ToInt32(id)));
            return Ok(await _cards.ReadAllAsync(x => x.Id == Convert.ToInt32(id) && x.UserEmail == name));

        }       
        [HttpPost]
        public async Task<IActionResult> Post(DTOs.CardDTO card)
        {
            if (card == null)
            {
                return BadRequest();
            }

            string name = User.FindFirstValue(ClaimTypes.Name);

            var _card = new Logicea_Cards.Models.Card
            {
                Name = card.Name,
                Color = card.Color,
                Description = card.Description,
                UserEmail = name,                
            };

            await _cards.CreateAsync(_card);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(DTOs.CardDTO card)
        {

            string name = User.FindFirstValue(ClaimTypes.Name);

            if (card == null)
                return BadRequest();

            var _card = new Models.Card
            {
                Id = card.Id,
                Name = card.Name,
                Color = card.Color,
                Description = card.Description,
                Status = card.Status,
                UserEmail = name
            };

            await _cards.UpdateAsync(_card, x =>x.UserEmail == name);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            string role = User.FindFirstValue(ClaimTypes.Role);
            string name = User.FindFirstValue(ClaimTypes.Name);

            if (role == "Admin")
                await _cards.DeleteAsync(x => x.Id == Convert.ToInt32(id));
            await _cards.DeleteAsync(x => x.Id == Convert.ToInt32(id) && x.UserEmail == name);

            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] QueryParameters parameters)
        {
            if (parameters == null)
                return BadRequest();

            string name = User.FindFirstValue(ClaimTypes.Name);

            return Ok(await _cards.ReadAllFilterAsync(x =>x.UserEmail == name && ( x.Name.Contains(parameters.Name) || x.Color == parameters.Color || x.Status == parameters.Status || x.CreationDate.Date == Convert.ToDateTime(parameters.DateOfCreation)), (parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize));

        }
    }
}
