using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace andri.htmlagility
{
	public class ScriptTagFilter : IFilter
	{
		private NegativeFilter _internalScriptFilter;
		public ScriptTagFilter()
		{
			this._internalScriptFilter = new NegativeFilter("script");
		}

		public void Apply(HtmlAgilityPack.HtmlDocument doc)
		{
			_internalScriptFilter.Apply(doc);
		}

		public string Filter
		{
			get { return this._internalScriptFilter.Filter; }
		}
	}
}
