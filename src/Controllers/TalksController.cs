using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [ApiController]
    [Route("api/camps/{moniker}/talks")]
    public class TalksController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public TalksController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        public async Task<ActionResult<TalkModel[]>> Get(string moniker) {
            try
            {
                var talks = await repository.GetTalksByMonikerAsync(moniker);
                if (talks == null) return NotFound();
                return mapper.Map<TalkModel[]>(talks);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id) {
            try
            {
                var camp = await repository.GetCampAsync(moniker);
                if (camp == null) return NotFound("Camp doesnt found");

                var talk = await repository.GetTalkByMonikerAsync(moniker, id);

                if (talk == null) return NotFound("Talk doesnt found");

                return mapper.Map<TalkModel>(talk);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model) {

            try
            {
                var camp = await repository.GetCampAsync(moniker);
                if (camp == null) return NotFound("Camp doesnt found");
                var talk = mapper.Map<Talk>(model);

                talk.Camp = camp;
                if (model.Speaker == null) return BadRequest("Camp doesnt exist");
                var speaker = await repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                if (speaker == null) return BadRequest("Speaker could not be found");
                talk.Speaker = speaker;

                repository.Add(talk);

                if (await repository.SaveChangesAsync())
                {
                    var url = linkGenerator.GetPathByAction(HttpContext, "Get", values: new
                    {
                        moniker,
                        id = talk.TalkId
                    });
                    return Created(url, mapper.Map<TalkModel>(talk));
                }
                else {
                    return BadRequest("Failed to save new talk");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model) {
            try
            {
                var talk = await repository.GetTalkByMonikerAsync(moniker, id);
                if (talk == null) return NotFound("Couldn't find the talk");

                if (model.Speaker != null) {
                    var speaker = await repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    talk.Speaker = speaker;
                }

                mapper.Map(model, talk);

                if (await repository.SaveChangesAsync()) {
                    return mapper.Map<TalkModel>(talk);
                }
                return BadRequest();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string moniker, int id) {

            try
            {
                var talk = await repository.GetTalkByMonikerAsync(moniker, id);
                if (talk == null) return NotFound("Failed to find the talk to delete");

                repository.Delete(talk);

                if (await repository.SaveChangesAsync()) {

                    return Ok();
                }

                return BadRequest("Failed to delete task");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }

        }



    }
}
 