using HtmlAgilityPack;
using System;

using Fizzler.Systems.HtmlAgilityPack;

namespace andri.htmlagility
{
	public class TransformNodeFilter : IFilter
	{
		public string Filter
		{
			get;
			protected set;
		}
		public Action<HtmlNode> FilterAction
		{
			get;
			protected set;
		}

		public TransformNodeFilter(string filter, Action<HtmlNode> action)
		{
			this.Filter = filter;
			FilterAction = action;
		}

		public virtual void Apply(HtmlAgilityPack.HtmlDocument doc)
		{
			var tags = doc.DocumentNode.QuerySelectorAll(Filter);
			foreach(var tag in tags)
			{
				FilterAction.Invoke(tag);
			}
		}
	}
}
