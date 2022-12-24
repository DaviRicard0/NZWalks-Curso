using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IRegionRepository regionRepository, IMapper mapper, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await walkRepository.GetAllAsync();

            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walks = await walkRepository.GetAsync(id);

            if (walks is null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walks);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.WalkRequest walkRequest)
        {
            await ValidateWalkAsync(walkRequest);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walk = new Models.Domain.Walk()
            {
                Name= walkRequest.Name,
                Length=walkRequest.Length,
                WalkDifficultyId=walkRequest.WalkDifficultyId,
                RegionId=walkRequest.RegionId,
            };

            walk = await walkRepository.AddAsync(walk);

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);

            if (walk is null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.WalkRequest walkRequest)
        {
            await ValidateWalkAsync(walkRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walk = new Models.Domain.Walk()
            {
                Length = walkRequest.Length,
                Name= walkRequest.Name,
                WalkDifficultyId=walkRequest.WalkDifficultyId,
                RegionId=walkRequest.RegionId,
            };

            var response = await walkRepository.UpdateAsync(id, walk);

            if (response is null)
                return NotFound();

            var walkDTO = mapper.Map<Models.DTO.Walk>(response);

            return Ok(walkDTO);
        }


        #region Private Methods

        private async Task<bool> ValidateWalkAsync(WalkRequest walkRequest)
        {
            var region = await regionRepository.GetAsync(walkRequest.RegionId);
            if (region is null)
            {
                ModelState.AddModelError(nameof(walkRequest.RegionId), $"{nameof(walkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(walkRequest.WalkDifficultyId);
            if (walkDifficulty is null)
            {
                ModelState.AddModelError(nameof(walkRequest.WalkDifficultyId), $"{nameof(walkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
