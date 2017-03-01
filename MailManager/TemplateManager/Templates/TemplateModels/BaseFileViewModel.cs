using System;
using MailManager.Utility;
using Nustache.Core;

namespace MailManager.TemplateManager.Templates.TemplateModels
{
    public class BaseFileViewModel: BindableBase, IRenderable
    {
        protected string Source { get; set; }

        public string RenderThis()
        {
            // By default, Nustache converts the string to html encoding, which makes russian text unreadable.
            // Since I don't need html encoding anyway, I change default encoder to simply return input string
            // https://github.com/jdiamond/Nustache/blob/master/Nustache.Core/Encoders.cs
            return Render.StringToString(Source, this, new RenderContextBehaviour() { HtmlEncoder = text => text });

            // Left as an example of another approach
            // return System.Net.WebUtility.HtmlDecode(Render.StringToString(_source, this));
        }

        public event EventHandler RenderRequest;

        protected virtual void OnRenderRequest(BaseFileViewModel viewModel)
        {
            EventHandler handler = RenderRequest;
            handler?.Invoke(this, null);
        }
    }
}
