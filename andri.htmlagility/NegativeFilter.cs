using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using Fizzler.Systems.HtmlAgilityPack;

namespace andri.htmlagility
{
	public class NegativeFilter : IFilter
	{
		public string Filter {get; protected set;}

		public NegativeFilter(string filter)
		{
			Filter = filter;
		}
		
		public virtual void Apply(HtmlAgilityPack.HtmlDocument doc)
		{
			var tagsToBeRemoved = doc.DocumentNode.QuerySelectorAll(Filter).ToList();
			foreach(var tag in tagsToBeRemoved)
			{
#if NETFX_CORE
				tag.Remove();
#else
                tag.ParentNode.RemoveChild(tag);
#endif
			}
		}
	}
}
