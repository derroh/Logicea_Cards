using Logicea_Cards.Entity;
using Microsoft.AspNetCore.Mvc;
using Logicea_Cards.Models;

namespace Logicea_Cards.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            return await _cards.ReadAllAsync();
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            return Ok(await _cards.ReadAllAsync(x => x.Id == Convert.ToInt32(id)));

        }
        [HttpPost]
        public async Task<IActionResult> Post(Card card)
        {
            if (card == null)
                return BadRequest();
            await _cards.CreateAsync(card);
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
            await _cards.DeleteAsync(x => x.Id == Convert.ToInt32(id));
            return Ok();
        }
    }
}
