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
        public ActionResult<List<Policy>> Get()
        {
            return Ok(_policyRepository.AsQueryable());
        }

        // GET api/<PolicyController>/5
        [HttpGet("{id}")]
        public ActionResult<Policy> Get(string id)
        {
            var policy = _policyRepository.FindById(id);

            if (policy == null)
            {
                return NotFound($"Policy with Id : {id} not found");
            }
            return policy;
        }

        // POST api/<PolicyController>
        [HttpPost]
        public ActionResult<Policy> Post([FromBody] Policy policy)
        {
            _policyRepository.InsertOne(policy);

            return CreatedAtAction(nameof(Get), new { id = policy.Id }, policy);
        }

        // PUT api/<PolicyController>/5
        [HttpPut("{id}")]
        public ActionResult<Policy> Put(string id, [FromBody] Policy policy)
        {

            var existingPolicy = _policyRepository.FindById(id);

            if (existingPolicy == null)
            {
                return NotFound($"Policy with Id : {id} not found");
            }

            _policyRepository.ReplaceOne(existingPolicy.Id, policy);

            return NoContent();
        }

        // DELETE api/<PolicyController>/5
        [HttpDelete("{id}")]
        public ActionResult<Policy> Delete(string id)
        {
            var existingPolicy = _policyRepository.FindById(id);

            if (existingPolicy == null)
            {
                return NotFound($"Policy with Id : {id} not found");
            }

            _policyRepository.DeleteById(existingPolicy.Id.ToString());

            return Ok($"Policy with Id : {id} deleted");
        }
    }
}
