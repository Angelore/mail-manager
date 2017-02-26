using System;

namespace MailManager.TemplateManager
{
    public interface IRenderable
    {
        string RenderThis();

        event EventHandler RenderRequest;
    }
}
