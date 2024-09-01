using System;
using Foundation;
using UIKit;

namespace IFRHoldClearanceTrainer.Platforms.iOS.Services;

public static class SavePictureService
{
    public static void SavePicture(byte[] arr)
    {
        var imageData = NSData.FromArray(arr);
        var image = UIImage.LoadFromData(imageData);

        image.SaveToPhotosAlbum((img, error) =>
        {
        });
    }
}
