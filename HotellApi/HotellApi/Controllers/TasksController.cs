using Microsoft.AspNetCore.Mvc;
using HotellApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace HotellApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TasksController : ControllerBase
	{
		Assignment4Context context = new Assignment4Context();
		public TasksController() {

		}

		// Fetches DbSet of all roomservices
		[HttpGet("fetch_roomservice", Name = "GetRoomservice")]
		public async Task<DbSet<RoomService>> getService()
		{
			return context.RoomServices;
		}

		// Temp controller - Adds a single new roomservice task
		[HttpPost("add_roomservice", Name = "AddRoomservice")]
		public async Task<IActionResult> addService([FromQuery] int roomServiceId, [FromQuery] string serviceType,
			[FromQuery] string requestStatus, [FromQuery] string requestNotes, [FromQuery] DateTime createdAt, [FromQuery] int roomNumber)
		{
			RoomService service = new RoomService {
				RoomServiceId = roomServiceId,
				ServiceType = serviceType,
				RequestStatus = requestStatus,
				RequestNotes = requestNotes,
				CreatedAt = createdAt,
				RoomNumber = roomNumber

			};
			context.RoomServices.Add(service);
			await context.SaveChangesAsync();
			return Ok();
		}

		// Temp controller - Adds 30 roomservice tasks
		[HttpPost("test_add", Name = "testadd")]
		public async Task<IActionResult> testAdd()
		{
			for (int i = 0; i < 30; i++)
			{
				RoomService service = new RoomService
				{
					RoomServiceId = i,
					ServiceType = "Cleaning",
					RequestStatus = "New",
					RequestNotes = "temp",
					CreatedAt = DateTime.Now,
					RoomNumber = 203,
				};
				context.RoomServices.Add(service);
				await context.SaveChangesAsync();
			}
			return Ok();
		}

		// Change the status (new/in-progress/completed) of a specific task, by primary key
		[HttpPut("changestatus_roomservice", Name = "ChangeStatusRoomservice")]
		public async Task<IActionResult> changeStatus([FromQuery] int roomServiceId, [FromQuery] string requestStatus)
		{
			List<RoomService> services = context.RoomServices.ToList();
			RoomService serv = services.Find((service) => service.RoomServiceId == roomServiceId);

			if (requestStatus == "New")
			{
				serv.RequestStatus = "In-progress";
			}
			else if (requestStatus == "In-progress")
			{
				serv.RequestStatus = "Completed";
			}

			try
			{
				await context.SaveChangesAsync();
				return Ok();
			} catch (Exception ex)
			{
				return StatusCode(500, "Error occured");
			}
		}
		// Temp controller - Changes status of specific task back to new
		[HttpPut("temp_statuschange", Name = "ChangeStatusRoomserviceTemp")]
		public async Task<IActionResult> changeStatusTemp([FromQuery] int roomServiceId, [FromQuery] string requestStatus)
		{
			List<RoomService> services = context.RoomServices.ToList();
			RoomService serv = services.Find((service) => service.RoomServiceId == roomServiceId);

			serv.RequestStatus = "New";

			try
			{
				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Error occured");
			}
		}

		// Delete a roomservice task
		[HttpDelete("delete_roomservice", Name = "DeleteRoomservice")]
		public async Task<IActionResult> deleteRoomservice([FromQuery] int[] roomServiceIds)
		{
			List<RoomService> services = context.RoomServices.ToList();

			List<RoomService> deletable = new List<RoomService>();
			// Add all the lists that should be deleted to a new list
			foreach (RoomService s in services) {
				if (roomServiceIds.Contains(s.RoomServiceId))
				{
					deletable.Add(s);
				}
			}


			try
			{
				// Delete 
				foreach (RoomService s in deletable)
				{
					context.RoomServices.Remove(s);
				}
				context.SaveChanges();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		// Change notes on roomservice task
		[HttpPut("notes_roomservice", Name = "NotesRoomservice")]
		public async Task<IActionResult> notesRoomservice([FromQuery] int roomServiceId, [FromQuery] string newNote)
		{
			try
			{
				RoomService service = context.RoomServices.Find(roomServiceId);
				service.RequestNotes = newNote;
				
				context.SaveChanges();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
    }
}
