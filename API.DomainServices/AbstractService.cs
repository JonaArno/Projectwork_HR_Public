using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Links;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices
{
    public class AbstractService
    {
        public LinkDto CreateLink(int id, string getCall, IUrlHelper urlHelper)
        {
            return new LinkDto(id, urlHelper.Link($"{getCall}", new { id = id }),
                "self",
                "GET");
        }
    }
}
