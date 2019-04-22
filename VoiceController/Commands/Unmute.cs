using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Unmute : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Unmute" }; }
		}

		public void Execute(object o = null)
		{
			var DevEnum = new MMDeviceEnumerator();
			var device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
			device.AudioEndpointVolume.Mute = false;
		}
	}
}