﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebVella.Erp.Web.Models;
using WebVella.Erp.Web.Utils;

namespace WebVella.Erp.Web.TagHelpers
{
	[HtmlTargetElement("wv-icon-card")]
	public class WvIconCard : TagHelper
	{
		public WvIconCard(IHtmlGenerator generator)
		{
			Generator = generator;
		}

		protected IHtmlGenerator Generator { get; }

		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		[HtmlAttributeName("title")]
		public string Title { get; set; } = "";

		[HtmlAttributeName("description")]
		public string Description { get; set; } = "";

		[HtmlAttributeName("icon-color")]
		public ErpColor IconColor { get; set; } = ErpColor.Default;

		[HtmlAttributeName("class")]
		public string Class { get; set; } = "";

		[HtmlAttributeName("icon-class")]
		public string IconClass { get; set; } = "";

		[HtmlAttributeName("is-card")]
		public bool IsCard { get; set; } = true;

		[HtmlAttributeName("has-shadow")]
		public bool HasShadow { get; set; } = true;

		[HtmlAttributeName("is-clickable")]
		public bool IsClickable { get; set; } = true;


		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var embeddedContent = await output.GetChildContentAsync();

			output.TagName = "div";
			output.AddCssClass("icon-card-body");
			if (!IsCard) {
				output.AddCssClass(Class);
			}


			#region << Init wrappers >>
			var cardWrapperEl = new TagBuilder("div");
			cardWrapperEl.AddCssClass("card icon-card");
			if (IsClickable) {
				cardWrapperEl.AddCssClass("clickable");
			}

			cardWrapperEl.AddCssClass(Class);
			if (HasShadow)
			{
				cardWrapperEl.AddCssClass("shadow-sm");
			}

			var cardBodyWrapperEl = new TagBuilder("div");
			cardBodyWrapperEl.AddCssClass("card-body p-2");

			if (IsCard) {
				output.PreElement.AppendHtml(cardWrapperEl.RenderStartTag());
				output.PreElement.AppendHtml(cardBodyWrapperEl.RenderStartTag());
				output.PostElement.AppendHtml(cardBodyWrapperEl.RenderEndTag());

				output.PostElement.AppendHtml(embeddedContent);
				output.PostElement.AppendHtml(cardWrapperEl.RenderEndTag());
			}

			#endregion


			var IconEl = new TagBuilder("i");
			IconEl.AddCssClass("icon");
			IconEl.AddCssClass(IconClass);
			if (IconColor != ErpColor.Default)
			{
				IconEl.AddCssClass("go-" + IconColor.GetLabel());
			}
			output.Content.AppendHtml(IconEl);

			var metaEl = new TagBuilder("div");
			metaEl.AddCssClass("meta");

			var metaTitleEl = new TagBuilder("div");
			metaTitleEl.AddCssClass("title");
			metaTitleEl.InnerHtml.AppendHtml(Title);
			metaEl.InnerHtml.AppendHtml(metaTitleEl);

			var metaDescriptionEl = new TagBuilder("div");
			metaDescriptionEl.AddCssClass("description");
			metaDescriptionEl.InnerHtml.AppendHtml(Description);
			metaEl.InnerHtml.AppendHtml(metaDescriptionEl);

			output.Content.AppendHtml(metaEl);

		}
	}
}
