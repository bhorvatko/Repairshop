using Repairshop.Client.Common.UserNotifications;
using System.Media;

namespace Repairshop.Client.Infrastructure.UserNotifications;

internal class SoundNotificationService
    : ISoundNotificationService
{
    private bool _soundPlaying = false;

    public async Task PlaySoundNotification()
    {
        if (_soundPlaying) return;
        
        _soundPlaying = true;

        SoundPlayer soundPlayer = new SoundPlayer(UserNotificationResources.sound_notification);
        soundPlayer.Play();

        await Task.Delay(5000);

        _soundPlaying = false;
    }
}
