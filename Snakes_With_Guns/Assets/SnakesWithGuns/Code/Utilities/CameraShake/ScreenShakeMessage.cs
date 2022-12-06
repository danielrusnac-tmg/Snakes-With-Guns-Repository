namespace SnakesWithGuns.Utilities.CameraShake
{
    public struct ScreenShakeMessage
    {
        public CameraShakeType ShakeType;

        public ScreenShakeMessage(CameraShakeType shakeType)
        {
            ShakeType = shakeType;
        }
    }
}