using System.IO;

namespace WOWSModelMod
{
	internal class BinaryReader : System.IO.BinaryReader
	{
		public BinaryReader(Stream input)
			: base(input)
		{
		}

		public void Seek(int divlen)
		{
			while (BaseStream.Position % divlen != 0)
			{
				BaseStream.Position++;
			}
		}
	}
}
