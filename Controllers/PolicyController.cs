using Microsoft.AspNetCore.Mvc;
using policystore.Models;
using policystore.Services;

namespace policystore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IMongoRepository<Policy> _policyRepository;

        public PolicyController(IMongoRepository<Policy> policyRepository)
        {
            _policyRepository = policyRepository;
        }

        // GET: api/<PolicyController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Policy>> Get()
        {
            try
            {
                return Ok(_policyRepository.AsQueryable());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please try again later.");
            }
            
        }

        // GET api/<PolicyController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Policy> Get(string id)
        {
            try
            {
                var policy = _policyRepository.FindById(id);

                if (policy == null)
                {
                    return NotFound($"Policy with Id : {id} not found");
                }
                return policy;
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please try again later.");
            }

        }

        // POST api/<PolicyController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Policy> Post([FromBody] Policy policy)
        {
            try
            {
                _policyRepository.InsertOne(policy);

                return CreatedAtAction(nameof(Get), new { id = policy.Id }, policy);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please try again later.");
            }

        }

        // PUT api/<PolicyController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Policy> Put(string id, [FromBody] Policy policy)
        {
            try
            {
                var existingPolicy = _policyRepository.FindById(id);

                if (existingPolicy == null)
                {
                    return NotFound($"Policy with Id : {id} not found");
                }

                _policyRepository.ReplaceOne(existingPolicy.Id, policy);

                return Ok(policy);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please try again later.");
            }

        }

        // DELETE api/<PolicyController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Policy> Delete(string id)
        {
            try
            {
                var existingPolicy = _policyRepository.FindById(id);

                if (existingPolicy == null)
                {
                    return NotFound($"Policy with Id : {id} not found");
                }

                _policyRepository.DeleteById(existingPolicy.Id.ToString());

                return Ok($"Policy with Id : {id} deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please try again later.");
            }

        }
    }
}
