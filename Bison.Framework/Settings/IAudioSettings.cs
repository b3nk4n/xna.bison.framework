using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Settings
{
    public interface IAudioSettings
    {
        float SoundEffectVolume { get; set; }
        float MusicVolume { set; get; }
    }
}
