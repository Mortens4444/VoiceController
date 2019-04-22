using System.Collections.Generic;
using System.Linq;

namespace VoiceController.Commands
{
	class VolumeDown : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Volume down", "Quietly" }; }
		}

		public void Execute(object o = null)
		{
			var DevEnum = new MMDeviceEnumerator();
			var device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

			var numbers = new List<float>() { 0.25f, 0.10f, 0.05f, 0.03f, 0.01f };
			foreach (var number in numbers.Where(number => device.AudioEndpointVolume.MasterVolumeLevelScalar > 0 + number))
			{
				device.AudioEndpointVolume.MasterVolumeLevelScalar -= number;
				break;
			}
		}
	}
}