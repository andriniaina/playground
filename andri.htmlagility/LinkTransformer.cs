using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace andri.htmlagility
{
	public class LinkToSpanTransformer : IFilter
	{
		private TransformNodeFilter _filterAction = new TransformNodeFilter("a", node =>
			{
				node.Name = "span";
			});

		public string Filter
		{
			get { return _filterAction.Filter; }
		}

		public void Apply(HtmlAgilityPack.HtmlDocument doc)
		{
			_filterAction.Apply(doc);
		}
	}
}
