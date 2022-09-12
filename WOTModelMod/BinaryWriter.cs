using System.IO;

namespace WOTModelMod
{
	internal class BinaryWriter : System.IO.BinaryWriter
	{
		public BinaryWriter(Stream input)
			: base(input)
		{
		}

		public void Seek(int divlen)
		{
			while (BaseStream.Position % divlen != 0)
			{
				Write((byte)0);
			}
		}
	}
}
