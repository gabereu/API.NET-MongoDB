using System;
using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("infectado")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            this._mongoDB = mongoDB;
            this._infectadosCollection = mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDTO infectadoDto){
            var infectado = new Infectado(infectadoDto.DataNascimento, infectadoDto.Sexo, infectadoDto.Latitude, infectadoDto.Longitude);
            this._infectadosCollection.InsertOne(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados(){
            var infectados = this._infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }
        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] UpdateInfectadoDTO infectadoDto){

            var infectado = new Infectado(infectadoDto.DataNascimento, infectadoDto.Sexo, infectadoDto.Latitude, infectadoDto.Longitude);

            var query = Builders<Infectado>.Filter.Where(i => i.DataNascimento == infectadoDto.DataNascimento);

            var update = Builders<Infectado>.Update
            .Set("Sexo", infectado.Sexo)
            .Set("DataNascimento", infectado.DataNascimento)
            .Set("Localização", infectado.Localização);

            this._infectadosCollection.UpdateOne(query, update);
            
            return Ok("Atualizado com sucesso");
        }
        // [HttpDelete]
        // public ActionResult DeletarInfectado([FromBody] DeleteInfectadoDTO infectadoDto){

        //     var query = Builders<Infectado>.Filter.Where(i => i.DataNascimento == infectadoDto.DataNascimento);

        //     this._infectadosCollection.DeleteOne(query);
            
        //     return Ok("Deletado com sucesso");
        // }
        [HttpDelete("{dataNascimento?}")]
        public ActionResult DeletarInfectado([FromRoute] string dataNascimento){

            var query = Builders<Infectado>.Filter.Where(i => i.DataNascimento == Convert.ToDateTime(dataNascimento));

            this._infectadosCollection.DeleteOne(query);
            
            return Ok("Deletado com sucesso");
        }
    }
}