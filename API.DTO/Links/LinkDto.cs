using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Links
{
    public class LinkDto
    {
        /// <summary>
        /// Unique identifier of the resource.
        /// </summary>
        public int Identifier { get; private set; }
        /// <summary>
        /// The URI of the resource.
        /// </summary>
        public string Href { get; private set; }
        /// <summary>
        /// How the resource relates to the current call.
        /// </summary>
        public string Rel { get; private set; }
        /// <summary>
        /// The method which can be executed on the provided URI.
        /// </summary>
        public string Method { get; private set; }

        public LinkDto(int identifier, string href, string rel, string method)
        {
            Identifier = identifier;
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
