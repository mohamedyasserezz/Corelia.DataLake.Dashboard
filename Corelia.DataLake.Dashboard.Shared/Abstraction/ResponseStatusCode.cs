using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Abstraction
{
	public enum ResponseStatusCode
	{
		// Success
		Success = 200,
		Created = 201,
		NoContent = 204,

		// Client Errors
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		Conflict = 409,
		UnprocessableEntity = 422,

		// Server Errors
		InternalServerError = 500,
		NotImplemented = 501,
		BadGateway = 502,
		ServiceUnavailable = 503,
		GatewayTimeout = 504
	}
}
