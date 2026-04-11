using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;
        private readonly IMapper _mapper;

        public SponsorController(ISponsorService sponsorService, IMapper mapper)
        {
            _sponsorService = sponsorService;
            _mapper = mapper;
        }

        // ── CRUD ──

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SponsorResponseDTO>>> GetAll()
        {
            var sponsors = await _sponsorService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorResponseDTO>> GetById(int id)
        {
            var sponsor = await _sponsorService.GetByIdAsync(id);
            if (sponsor == null) return NotFound();

            return Ok(_mapper.Map<SponsorResponseDTO>(sponsor));
        }

        [HttpPost]
        public async Task<ActionResult<SponsorResponseDTO>> Create(SponsorRequestDTO dto)
        {
            try
            {
                var sponsor = _mapper.Map<Sponsor>(dto);
                var created = await _sponsorService.CreateAsync(sponsor);
                var response = _mapper.Map<SponsorResponseDTO>(created);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SponsorRequestDTO dto)
        {
            try
            {
                var sponsor = _mapper.Map<Sponsor>(dto);
                await _sponsorService.UpdateAsync(id, sponsor);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sponsorService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // ── Relación con Torneos ──

        [HttpPost("{id}/tournaments")]
        public async Task<ActionResult<TournamentSponsorResponseDTO>> LinkToTournament(int id, TournamentSponsorRequestDTO dto)
        {
            try
            {
                var linked = await _sponsorService.LinkToTournamentAsync(id, dto.TournamentId, dto.ContractAmount);
                var response = _mapper.Map<TournamentSponsorResponseDTO>(linked);
                return Created("", response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/tournaments")]
        public async Task<ActionResult<IEnumerable<TournamentSponsorResponseDTO>>> GetTournamentsBySponsor(int id)
        {
            try
            {
                var tournaments = await _sponsorService.GetTournamentsBySponsorAsync(id);
                return Ok(_mapper.Map<IEnumerable<TournamentSponsorResponseDTO>>(tournaments));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/tournaments/{tid}")]
        public async Task<IActionResult> UnlinkFromTournament(int id, int tid)
        {
            try
            {
                await _sponsorService.UnlinkFromTournamentAsync(id, tid);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
