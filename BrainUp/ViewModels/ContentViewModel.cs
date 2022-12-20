using BrainUp.Models;

namespace BrainUp.ViewModels
{
    public class ContentViewModel
    {
            public int ContentId { get; set; }

            public string Text { get; set; } = null!;

            public int SubMenuId { get; set; }

            public string Name { get; set; } = null!;

            public virtual SubMenu SubMenu { get; set; } = null!;

            public void FromContent(Content content)
            {
                this.ContentId = content.Id;
                this.Text = content.Text;
                this.SubMenuId  = content.SubMenuId;
                this.Name = content.Name;
            }

            public Content ToContent()
            {
                var content = new Content();
                content.Id = this.ContentId;
                content.Text = this.Text;
                content.SubMenuId = this.SubMenuId;
                content.Name = this.Name;
                return content;
            }
    }
}
