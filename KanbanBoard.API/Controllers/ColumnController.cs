﻿using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Columns;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColumnController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var columns = await _unitOfWork.Column.GetAllAsync();
            return Ok(columns);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id) {
            var column = await _unitOfWork.Column.GetByIdAsync(id);
            return column == null ? NotFound() : Ok(column);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateColumnDto columnDto)
        { 
            if (columnDto == null) return BadRequest();
            var columnModel = columnDto.ToColumn();
            await _unitOfWork.Column.AddAsync(columnModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = columnModel.ColumnId }, columnModel.ToSimplifiedColumn());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateColumnDto columnDto)
        {
            var columnModel = await _unitOfWork.Column.GetByIdAsync(id);
            if (columnModel == null) return BadRequest();

            columnModel.Name = columnDto.Name;

            _unitOfWork.Column.Update(columnModel);
            await _unitOfWork.SaveAsync();
            return Ok(columnModel.ToSimplifiedColumn());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            var columnModel = await _unitOfWork.Column.GetByIdAsync(id);
            if (columnModel == null) return NotFound();

            _unitOfWork.Column.Remove(columnModel);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}