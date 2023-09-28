using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTOs;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class walkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly NZwalksDBcontext dBcontext;
        private object domainmodel;

        public walkController(IMapper mapper, NZwalksDBcontext dBcontext)
        {
            this.mapper = mapper;
            this.dBcontext = dBcontext;
        }


        // get all walks

        [HttpGet]

        public async Task<IActionResult> Getall()
        {

            var domainmodel = await dBcontext.Walks.ToArrayAsync();

            return Ok(mapper.Map<List<Walk>>(domainmodel));


        }

        // get by id 

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Getbyid([FromRoute] Guid id)
        {
            var domainmodel = await dBcontext.Walks.FirstOrDefaultAsync(x => x.Id == id); //2nd method

            if (domainmodel == null)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<walkdomaintodto>(domainmodel));


        }




        //  post for add walks

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] walkpostdto walkpostdto)
        {

            if (walkpostdto == null)
            {
                return BadRequest("Invalid data"); // You should handle validation properly, this is a basic example
            }
            if (ModelState.IsValid)
            {

                var domainmodel = mapper.Map<Walk>(walkpostdto);

                await dBcontext.Walks.AddAsync(domainmodel);
                await dBcontext.SaveChangesAsync();

                return Ok(mapper.Map<walkdomaintodto>(domainmodel));
            }
            else
            {
                return BadRequest();
            }








        }


        


        //update by id 


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> update([FromBody] updatewalkdto updatewalkdto, [FromRoute] Guid id)
        {
            if (ModelState.IsValid)
            {
                var domainModel = mapper.Map<Walk>(updatewalkdto);
                domainModel = await dBcontext.Walks.FirstOrDefaultAsync(x => x.Id == id); //2nd method

                if (domainModel == null)
                {
                    return BadRequest();
                }

                domainModel.Name = updatewalkdto.Name;
                domainModel.Description = updatewalkdto.Description;
                domainModel.LengthInKm = updatewalkdto.LengthInKm;
                domainModel.walkImageurl = updatewalkdto.walkImageurl;
                domainModel.DifficultyId = updatewalkdto.DifficultyId;
                domainModel.RegionId = updatewalkdto.RegionId;





                await dBcontext.SaveChangesAsync();

                return Ok(mapper.Map<walkdomaintodto>(domainModel));
            }
            else
            {
                return BadRequest();
            }

        }

        //delete 

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteData([FromRoute] Guid id)
        {
            var domainModel = await dBcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (domainModel == null)
            {
                return BadRequest();
            }

            dBcontext.Walks.Remove(domainModel);
            await dBcontext.SaveChangesAsync();
            return Ok();
        }
    }
}
