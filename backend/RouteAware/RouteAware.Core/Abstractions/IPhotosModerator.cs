using System.Drawing;

namespace RouteAware.Moderation
{
    public interface IPhotosModerator
    {
        bool Moderate(Image image);
    }
}