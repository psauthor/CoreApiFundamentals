using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;

        public CampsController(ICampRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CampModels[]>> Get(bool includeTalks = false)
        {
            try
            {
                var result = await repository.GetAllCampsAsync(includeTalks);

                CampModels[] models = mapper.Map<CampModels[]>(result);

                return models;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModels>> Get(string moniker)
        {
            try
            {
                var result = await repository.GetCampAsync(moniker);
                if (result == null)
                {
                    return NotFound();
                }
                return mapper.Map<CampModels>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");

            }
        }

    }
}
