using System;
using System.Text;

namespace VoiceController.Messages
{
	public static class Base64
	{
		public static string Encode(byte[] bytes)
		{
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes), Constants.PARAMETER_IS_NULL);
            }
			return Convert.ToBase64String(bytes);
		}
		
		public static string Encode(string plainString)
		{
            if (plainString == null)
            {
                throw new ArgumentNullException(nameof(plainString), Constants.PARAMETER_IS_NULL);
            }
            byte[] encodedBytes = Encoding.UTF8.GetBytes(plainString);
			return Convert.ToBase64String(encodedBytes);
		}

		public static string Decode(string encodedString)
		{
            if (encodedString == null)
            {
                throw new ArgumentNullException(nameof(encodedString), Constants.PARAMETER_IS_NULL);
            }
            byte[] decodedBytes = Convert.FromBase64String(encodedString);
			return Encoding.UTF8.GetString(decodedBytes);
		}

		public static byte[] DecodeToArray(string encodedString)
		{
            if (encodedString == null)
            {
                throw new ArgumentNullException(nameof(encodedString), Constants.PARAMETER_IS_NULL);
            }
			return Convert.FromBase64String(encodedString);
		}

		public static string Encode(string plainString, bool trimEqualSigns)
		{
			if (trimEqualSigns)
			{
				var result = Encode(plainString);
				return result.TrimEnd(new [] { '=' });
			}
			return Encode(plainString);
		}
	}
}
