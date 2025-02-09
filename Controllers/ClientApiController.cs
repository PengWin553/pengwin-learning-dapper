using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace pengwin_learning_dapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientApiController : ControllerBase
    {

        private SqliteConnection _connection = new SqliteConnection("Data Source = bodyData.db");

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients(){

            const string query = "Select * from Client order by id desc";
            var result  = await _connection.QueryAsync<Client>(query);
            
            if(result.Count() == 0)
                return BadRequest("alskjdfalskjf");

            return Ok(result);
        }

        // for the update function
        [HttpGet("GetClient")]
        public async Task<ActionResult<Client>> GetClients(int id){

            const string query = "Select * from Client where Id = @id LIMIT 1";
            var result  = await _connection.QueryFirstAsync<Client>(query, new {id = id});
            
            if(result == null)
                return BadRequest("alskjdfalskjf");

            return Ok(result);
        }


        [HttpPost("SaveClient")]
        public async Task<IActionResult> SaveClientAsync(Client client){
            
            const string query = "Insert into Client (ClientName, Residency) Values ( @ClientName, @Residency ); Select * from Client order by Id desc Limit 1";
            
            var result  = await _connection.QueryAsync<Client>(query, client);

            return Ok(result);
        }

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClientAsync(int Id, Client client){
            
            const string query = "Update Client set ClientName = @sldkfjsdlf, Residency = @alt124 where Id = @Id; Select * from Client where Id = @Id limit 1 ";
            
            var result  = await _connection.QueryAsync<Client>(query, new {
                Id = Id,
                sldkfjsdlf = client.ClientName,
                alt124 = client.Residency
            });

            return Ok(result);
        }

        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int Id){
            
            const string query = "Delete From Client where Id = @Id; ";
            
            await _connection.QueryAsync<Client>(query, new { Id = Id,});

            return Ok();
        }
    }
}