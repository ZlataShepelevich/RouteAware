using FastYolo;
using FastYolo.Model;
using System.Drawing;
using System.Drawing.Imaging;

namespace RouteAware.Moderation
{
    public class PhotosModerator : IPhotosModerator
    {
        public bool Moderate(Image image)
        {
            try
            {
                YoloWrapper yolo = new YoloWrapper("YOLOv3/yolov3.cfg", "YOLOv3/yolov3.weights", "YOLOv3/coco.names");

                MemoryStream memoryStream = new MemoryStream();

                bool isThePhotoNormal = false;

                image.Save(memoryStream, ImageFormat.Jpeg);

                List<YoloItem> items = yolo.Detect(memoryStream.ToArray()).ToList<YoloItem>();

                foreach (YoloItem item in items)
                {
                    if (item.Type.Equals("car") ||
                        item.Type.Equals("bicycle") ||
                        item.Type.Equals("motorbike") ||
                        item.Type.Equals("bus") ||
                        item.Type.Equals("truck") ||
                        item.Type.Equals("horse"))
                    {
                        isThePhotoNormal = true;
                    }

                    Console.WriteLine(item.Type);
                }

                return isThePhotoNormal;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
    }
}
