using System.Web;

namespace VoiceController
{
    /// <summary>
    /// Google TextToSpeech
    /// </summary>
    public class UrlMp3Reader
    {
        private readonly UrlMp3Player urlMp3Player;

        public UrlMp3Reader()
        {
            urlMp3Player = new UrlMp3Player();
        }

        public void ReadAsyncInEnglishWithGoogle(string text)
        {
            urlMp3Player.PlayMp3FromUrl("http://translate.google.com/translate_tts?tl=en&q=" + TransformText(text));
        }

        public void ReadAsyncInHungarianWithGoogle(string text)
        {
            urlMp3Player.PlayMp3FromUrl("http://translate.google.com/translate_tts?tl=hu&q=" + TransformText(text));
        }

        private static string TransformText(string text)
        {
            return text.Replace(' ', '+');
            //return HttpUtility.UrlEncode(text);
        }
    }
}
