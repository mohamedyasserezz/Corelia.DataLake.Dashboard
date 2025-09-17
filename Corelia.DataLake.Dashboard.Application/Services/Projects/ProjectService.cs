using Corelia.DataLake.Dashboard.Domain.Contract.Service.Projects;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application.Services.Projects
{

	internal class ProjectService : IProjectService
	{
		private readonly HttpClient _httpClient;

		public ProjectService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:8080/api/");
			_httpClient.DefaultRequestHeaders.Add("Authorization", "Token 1e98e8df7131adfe6d3fd62278bc9163967a97ea");
		}
		public async Task<Result<PagedResult<ProjectResponse>>> ListProjectsAsync()
		{
			var response = await _httpClient.GetAsync("projects/");
			if (!response.IsSuccessStatusCode)
				return Result.Failure<PagedResult<ProjectResponse>>(new Error(response.StatusCode.ToString(), $"Status: {response.StatusCode}", (int)response.StatusCode));

			var content = await response.Content.ReadAsStringAsync();

			Console.WriteLine("Content: " + content); // هتشوف JSON الحقيقي

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

			var obj = JsonSerializer.Deserialize<PagedResult<ProjectResponse>>(content, options);

			Console.WriteLine("obj.results.Count: " + obj.results.Count); // هتأكد ان المشاريع موجودة
			Console.WriteLine("obj.results: " + obj.results); // هتأكد ان المشاريع موجودة

			if (obj == null)
				return Result.Failure<PagedResult<ProjectResponse>>(new Error("NullResponse", "Response was null", (int)ResponseStatusCode.BadRequest));

			return Result.Success(obj);
		}

		public async Task<Result<ProjectResponse>> CreateProjectAsync(CreateProjectRequest req)
		{
			var payload = new
			{
				title = req.title,
				description = req.description,
				//label_config = req.labelConfig,
				//is_published = req.isPublished ?? false
			};

			var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("projects/", content);
			if (!response.IsSuccessStatusCode)
				return Result.Failure<ProjectResponse>(new Error(response.StatusCode.ToString(), $"Status: {response.StatusCode}", (int)response.StatusCode));

			var responseBody = await response.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var resp = JsonSerializer.Deserialize<ProjectResponse>(responseBody, options);
			if (resp == null)
				return Result.Failure<ProjectResponse>(new Error("NullResponse", "Response was null", (int)ResponseStatusCode.BadRequest));

			return Result.Success(resp);
		}

		public async Task<Result<ProjectResponse>> GetProject(string id)
		{
			var response = await _httpClient.GetAsync($"projects/{id}");

			if (!response.IsSuccessStatusCode)
				return Result.Failure<ProjectResponse>(new Error(response.StatusCode.ToString(), $"Status: {response.StatusCode}", (int)response.StatusCode));

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

			var obj = JsonSerializer.Deserialize<ProjectResponse>(content, options);

			if (obj == null)
				return Result.Failure<ProjectResponse>(new Error("NullResponse", "Response was null", (int)ResponseStatusCode.BadRequest));

			return Result.Success(obj);
		}
	}
}
