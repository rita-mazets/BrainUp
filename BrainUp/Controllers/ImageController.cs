using BrainUp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BrainUp.Controllers
{
    public class ImageController : Controller
    {
        private BrainUpBdContext db;
        IWebHostEnvironment env;

        public ImageController(BrainUpBdContext context, IWebHostEnvironment env)
        {
            db = context;
            this.env = env;
        }
       /* public async Task<FileResult> GetAvatar()
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            if (user!= null && user.AvatarImage != null)
                return File(user.AvatarImage, "img/...");
            else
            {
                var avatarPath = "/img/avatar.png";
                return File(env.WebRootFileProvider.GetFileInfo(avatarPath).CreateReadStream(), "image/...");
            }
        }*/

        public async Task<FileResult> GetAvatar()
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            string path;

            if (user != null && user.Image != null)
                 path =  $"/img/{user.Image}";
            else
            {
                 path = "/img/avatar.png";
            }

            return File(env.WebRootFileProvider.GetFileInfo(path).CreateReadStream(), "image/...");
        }

        public async Task<FileResult> GetImage(byte[]? image)
        {
            if (image != null)
                return File(image, "img/...");
            else
            {
                var avatarPath = "/img/avatar.png";
                return File(env.WebRootFileProvider.GetFileInfo(avatarPath).CreateReadStream(), "img/...");
            }
        }
    }
}
