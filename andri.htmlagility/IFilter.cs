using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace andri.htmlagility
{
    public interface IFilter
    {
		string Filter { get; }
		void Apply(HtmlDocument doc);
    }

	public static class HtmlDocumentExtensions
	{
		public static HtmlDocument ApplyFilter(this HtmlDocument doc, IFilter filter)
		{
			filter.Apply(doc);
			return doc;
		}
	}
}
