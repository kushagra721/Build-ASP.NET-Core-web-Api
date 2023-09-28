using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZwalksDBcontext dBcontext;

        public RegionsController(NZwalksDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }
        [HttpGet]

        // Get all regions data
        public async Task< IActionResult> getAll()
        {
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Auckland region",
            //        Code = "AKD",
            //        RegionImageUrl= "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTuZtAlN-N6XjGupcedmWMBJMMofvR0oNEu_yGsKIEdO_XM8cA&s"


            //    },
            //};

            var regionsDomain = await dBcontext.Regions.ToListAsync();
            var regiondto = new List<RegionDto>(); // here normally we get data from databse in domain models so we converted into DTOs to increase the perfromanec it is also best practice
            foreach (var region in regionsDomain)
            {
                regiondto.Add(new RegionDto()
                {
                    Id = region.Id,

                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
                ;
            };


            return Ok(regiondto);
        }

        // Get data by ID

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task< IActionResult> get([FromRoute] Guid id) {


            /* var regions = dBcontext.Regions.Find(id);*/ //1st method
            var region =await  dBcontext.Regions.FirstOrDefaultAsync(x => x.Id == id); //2nd method

            if (region == null)
            {
                return BadRequest();
            }

            var regiondto = new List<RegionDto>(); // here normally we get data from databse in domain models so we converted into DTOs to increase the perfromanec it is also best practice

            regiondto.Add(new RegionDto()
            {
                Id = region.Id,

                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            });
            ;


            return Ok(regiondto);



        }

        //create POST req 

        [HttpPost]


        public async Task<IActionResult> Create([FromBody] PostRegionDto postRegionDto)
        {

            if (ModelState.IsValid)
            {
                var regiondomainmodel = new Region
                {
                    Code = postRegionDto.Code,
                    Name = postRegionDto.Name,
                    RegionImageUrl = postRegionDto.RegionImageUrl,
                };

                await dBcontext.Regions.AddAsync(regiondomainmodel);
                await dBcontext.SaveChangesAsync();

                var regiondto = new RegionDto
                {
                    Id = regiondomainmodel.Id,
                    Code = regiondomainmodel.Code,
                    Name = regiondomainmodel.Name,
                    RegionImageUrl = regiondomainmodel.RegionImageUrl,

                };
                return CreatedAtAction(nameof(get), new { id = regiondomainmodel.Id }, regiondto);
            }
            else
            {
                return BadRequest();
            }
        }


        // code for put req for updation
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Updatedata([FromRoute] Guid id, [FromBody] updateregionDto updateregionDto)
        {
            if (ModelState.IsValid)
            {
                var domainModel = await dBcontext.Regions.FirstOrDefaultAsync(x => x.Id == id); //2nd method

                if (domainModel == null)
                {
                    return BadRequest();
                }

                domainModel.Code = updateregionDto.Code;
                domainModel.Name = updateregionDto.Name;
                domainModel.RegionImageUrl = updateregionDto.RegionImageUrl;

                await dBcontext.SaveChangesAsync();


                var regiondto = new RegionDto
                {
                    Id = domainModel.Id,
                    Code = domainModel.Code,
                    Name = domainModel.Name,
                    RegionImageUrl = domainModel.RegionImageUrl,

                };


                return Ok(regiondto);
            }
            else
            {
                return BadRequest();
            }


        }


        //delete req 

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteData([FromRoute] Guid id) {
            var domainModel = await dBcontext.Regions.FirstOrDefaultAsync(x => x.Id == id); 

            if (domainModel == null)
            {
                return BadRequest();
            }

            dBcontext.Regions.Remove(domainModel);
            await dBcontext.SaveChangesAsync();
            return Ok();
        }

       
    }
}
