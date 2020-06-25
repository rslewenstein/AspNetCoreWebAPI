using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1.0 da API de Professores
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método responsável por pegar todos os professores.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        /// <summary>
        /// Método responsável por pegar o professor pelo ID.
        /// </summary>
        //api/professor/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
                
            if (professor == null) return BadRequest("Professor não encontrado.");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        /// <summary>
        /// Método responsável por Cadastrar o professor.
        /// </summary>
        [HttpPost]
        public IActionResult Post(ProfessorRegisterDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não encontrado");
        }

        /// <summary>
        /// Método responsável por atualizar o professor.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegisterDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado.");
        }

        /// <summary>
        /// Método responsável por atualizar o professor.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegisterDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado.");
        }

        /// <summary>
        /// Método responsável por deletar o professor.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);

            if (professor == null) return BadRequest("Professor não encontrado.");

            _repo.Delete(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não deletado.");
        }
    }
}