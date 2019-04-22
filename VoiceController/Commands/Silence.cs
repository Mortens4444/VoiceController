using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Silence : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { /*"Mute",*/ "Silence", "Quiet", "Be quiet"/*, "Shut up!"*/ }; }
		}

		public void Execute(object o = null)
		{
			var DevEnum = new MMDeviceEnumerator();
			var device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
			device.AudioEndpointVolume.Mute = true;
		}
	}
}