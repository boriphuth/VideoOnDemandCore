﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VideoOnDemandCore.Tag_Helpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("button-container")]
    public class ButtonContainerTagHelper : TagHelper
    {
        #region Properties
        public string Controller { get; set; }
        public string Actions { get; set; }
        public string Descriptions { get; set; }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            base.Process(context, output);

            // Replaces <button-container> with <span> tag
            output.TagName = "span";
            output.Content.SetHtmlContent("<span style='min-width:100px; display:inline-block;'>");

            var href = "";

            var actions = Actions.Split(',');
            var descriptions = Descriptions != null ? Descriptions.Split(',') :
                new string[0];
            var ids = context.AllAttributes.Where(c => c.Name.StartsWith("id"));

            foreach (var action in actions)
            {
                if (Controller != null && Controller.Length > 0)
                {
                    if (action != null && action.Length > 0)
                        href = $@"href='/admin/{Controller}/{action}'";
                    else
                        href = $@"href='/admin/{Controller}/Index'";
                }

                var description = action;
                if (descriptions.Length >= actions.Length)
                    description = descriptions[Array.IndexOf(actions, action)];
                if (description.Length.Equals(0)) description = action;

                var param = "";
                foreach (var id in ids)
                {
                    var splitId = id.Name.Split('-');
                    if (splitId.Length.Equals(1)) param = $"Id={id.Value}";
                    if (splitId.Length.Equals(2))
                        param += $"&{splitId[1]}={id.Value}";
                    if (param.StartsWith("&")) param = param.Substring(1);
                }
                if (param.Length > 0)
                    href = href.Insert(href.Length - 1, $"?{param}");


                output.Content.AppendHtml($@"<a {href}>{description}</a>");
            }

            output.Content.AppendHtml("</span>");
        }
    }
}
