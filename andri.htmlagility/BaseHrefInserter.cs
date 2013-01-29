using System;

namespace andri.htmlagility
{
	public class BaseHrefInserter : IFilter
	{
		TransformNodeFilter _hrefFilter;
		TransformNodeFilter _imgFilter;
		private Uri _baseURL;
		public BaseHrefInserter(string baseURL)
		{
			this._baseURL = new Uri(baseURL);

			this._hrefFilter = new TransformNodeFilter("a", node =>
			{
				var attribute = node.Attributes["href"];
				attribute.Value = toAbsoluteURI(attribute.Value);
			}
			);
			this._imgFilter = new TransformNodeFilter("img", node =>
			{
				var attribute = node.Attributes["src"];
				attribute.Value = toAbsoluteURI(attribute.Value);
			}
			);
		}

		private string toAbsoluteURI(string uri)
		{
			if (uri.StartsWith("http://") || uri.StartsWith("https://"))
			{
				// nothing
			}
			else
			{
				uri = new Uri(this._baseURL, uri).ToString();
			}
			return uri;
		}

		public string Filter
		{
			get { return _hrefFilter.Filter; }
		}

		public void Apply(HtmlAgilityPack.HtmlDocument doc)
		{
			doc
				.ApplyFilter(this._hrefFilter)
				.ApplyFilter(this._imgFilter);
		}
	}
}
