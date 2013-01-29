
namespace andri.htmlagility
{
	public class JavascriptLinkFilter : IFilter
	{
		private TransformNodeFilter _filterAction = new TransformNodeFilter("a[href^=javascript]", node =>
			{
				node.Name = "span";
#if NETFX_CORE
				node.Attributes["href"].Remove();
#else
                node.Attributes.Remove("href");
#endif
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
