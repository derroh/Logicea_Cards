using Logicea_Cards.Entity;
using Microsoft.AspNetCore.Mvc;
using Logicea_Cards.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Put(Card card)
        {
            if (card == null)
                return BadRequest();
            await _cards.UpdateAsync(card);
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
    }
}
