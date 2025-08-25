using AutoMapper;
using Corelia.DataLake.Dashboard.Domain.Entities.Workspaces;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			#region Workspace

			CreateMap<CreateWorkspaceRequest, Workspace>();

			CreateMap<UpdateWorkspaceRequest, Workspace>();

			CreateMap<Workspace, ReturnWorkspaceResponse>()
				.ForMember(d => d.id, opt => opt.MapFrom(src => src.Id))
				.ForMember(d => d.created_by, opt => opt.MapFrom(src => src.CreatedBy));
			#endregion
		}
	}
}
