using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToDoApi.Services;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/to-do-lists")]
    public class ToDoListsController : ControllerBase
    {
        private readonly ToDoListsService _service;
        public ToDoListsController(ToDoListsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize("read:todolist")]
        public IActionResult GetLists()
        {
            var temp = _service.GetLists();
            return Ok(temp);
        }

        [HttpGet("{id}")]
        [Authorize("read:todolist")]
        public IActionResult GetList([FromRoute] Guid id)
        {
            var found = _service.GetList(id);

            if(found == null)
                return NotFound();

            return Ok(found);
        }

        [HttpPost]
        [Authorize("create:todolist")]
        public IActionResult CreateList([FromBody] ToDoList newList)
        {
            var created = _service.CreateList(newList);
            if (created == null)
                return BadRequest();
            return Ok(created);
        }

        [HttpPut("{id}")]
        [Authorize("update:todolist")]
        public IActionResult UpdateList([FromBody] ToDoList list, [FromRoute]Guid id)
        {
            ToDoList toUpdate = _service.UpdateList(list, id);

            if (toUpdate == null)
                return NotFound();

            return Ok(toUpdate);
        }

        [HttpPut("{id}/move/{position}")]
        public IActionResult UpdateListPosition([FromBody] ToDoList list, [FromRoute]Guid id, [FromRoute]int position)
        {
            ToDoList returnList = _service.UpdateListPosition(id, position);
            return Ok(returnList);
        }

        [HttpDelete("{id}")]
        [Authorize("delete:todolist")]
        public IActionResult DeleteList([FromRoute] Guid id)
        {
            ToDoList deleted = _service.DeleteList(id);

            if (deleted == null)
                return NotFound();

            return Ok();
        }

        [HttpGet("search/{title}")]
        public IActionResult FindByTitle([FromRoute] string title)
        {
            return Ok(_service.FindByTitle(title));
        }


        //List items

        [HttpGet("{listId}/list-items")]
        [Authorize("read:todoitem")]
        public IActionResult GetListItems([FromRoute]Guid listId)
        {
            return Ok(_service.GetListItems(listId));
        }

        [HttpGet("{listId}/list-items/{listItemId}")]
        [Authorize("read:todoitem")]
        public IActionResult GetListItem([FromRoute] Guid listId, [FromRoute] Guid listItemId)
        {
            ListItem found = _service.GetListItem(listId, listItemId);
            if (found == default)
                return NotFound();
            return Ok(found);
        }

        [HttpPost("{listId}/list-items")]
        [Authorize("create:todoitem")]
        public IActionResult CreateListItem([FromRoute]Guid listId, [FromBody] ListItem newItem)
        {
            var created = _service.CreateListItem(listId, newItem);
            if (created == null)
                return BadRequest();
            return Ok(created);
        }

        [HttpPut("{listId}/list-items/{listItemId}")]
        [Authorize("update:todoitem")]
        public IActionResult UpdateItem([FromBody] ListItem item, [FromRoute] Guid listId, [FromRoute]Guid listItemId)
        {
            ListItem toUpdate = _service.UpdateItem(item, listId, listItemId);

            if (toUpdate == default)
                return NotFound();

            return Ok(toUpdate);
        }

        [HttpPut("{listId}/list-items/{listItemId}/move/{position}")]
        public IActionResult UpdateItemPosition([FromBody] ListItem item, [FromRoute] Guid listId, [FromRoute] Guid listItemId, [FromRoute] int position)
        {
            ListItem returnItem = _service.UpdateItemPosition(listId, listItemId, position);
            return Ok(returnItem);
        }

        [HttpDelete("{listId}/list-items/{listItemId}")]
        [Authorize("delete:todoitem")]
        public IActionResult DeleteItem([FromRoute] Guid listId, [FromRoute] Guid listItemId)
        {
            ListItem deleted = _service.DeleteItem(listId, listItemId);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }

    }
}

