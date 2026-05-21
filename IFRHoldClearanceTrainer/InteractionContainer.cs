
namespace IFRHoldClearanceTrainer;

public class InteractionContainer : ContentView
{
    public double offsetX, offsetY;
    double currentScale;
    double startScale = 1;

    public InteractionContainer()
    {
        // Set PanGestureRecognizer.TouchPoints to control the
        // number of touch points needed to pan
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated!;
        GestureRecognizers.Add(panGesture);

        PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
        pinchGesture.PinchUpdated += OnPinchUpdated!;
        GestureRecognizers.Add(pinchGesture);
    }

    void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                // Translate and pan.
                double boundsX = Content.Width;
                double boundsY = Content.Height;
                Content.TranslationX = Math.Clamp(offsetX + e.TotalX, -boundsX, 0);
                Content.TranslationY = Math.Clamp(offsetY + e.TotalY, -boundsY, 0);
                break;

            case GestureStatus.Completed:
                // Store the translation applied during the pan
                offsetX = Content.TranslationX;
                offsetY = Content.TranslationY;
                Console.WriteLine($"Offsets: {offsetX}, {offsetY}");
                break;

        }
    }

    void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        if (e.Status == GestureStatus.Started)
        {
            // Store the current scale factor applied to the wrapped user interface element,
            // and zero the components for the center point of the translate transform.
            startScale = Content.Scale;
            Content.AnchorX = offsetX;
            Content.AnchorY = offsetY;
            currentScale = startScale;
        }
        if (e.Status == GestureStatus.Running)
        {
            // Calculate the scale factor to be applied.
            
            currentScale += e.Scale - 1;

            Console.WriteLine($"Event Scale: {e.Scale} | Current Scale: {currentScale}");

            // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
            // so get the X pixel coordinate.
            double renderedX = Content.X + offsetX;
            double deltaX = renderedX / Width;
            double deltaWidth = Width / (Content.Width * startScale);
            double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

            // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
            // so get the Y pixel coordinate.
            double renderedY = Content.Y + offsetY;
            double deltaY = renderedY / Height;
            double deltaHeight = Height / (Content.Height * startScale);
            double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

            // Calculate the transformed element pixel coordinates.
            double targetX = offsetX - originX * Content.Width * (currentScale - startScale);
            double targetY = offsetY - originY * Content.Height * (currentScale - startScale);

            try{
                // Apply translation based on the change in origin.
                Content.TranslationX = Math.Clamp(targetX, -Content.Width * (currentScale - 1), Content.Width * currentScale);
                Content.TranslationY = Math.Clamp(targetY, -Content.Height * (currentScale - 1), Content.Height * currentScale);

                // Apply scale factor
                Content.Scale = currentScale;
                Content.IsVisible = true;
            }
            catch(ArgumentException exp){
                Console.WriteLine($"Transition Exception",exp);
            }
        }
        if (e.Status == GestureStatus.Completed)
        {
            // Store the translation delta's of the wrapped user interface element.
            offsetX = Content.TranslationX;
            offsetY = Content.TranslationY;
            Content.IsVisible = true;
        }
    }
}
